using CliWrap;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using CliWrap.Buffered;
using CliWrap.EventStream;
using System.Text;
using SS_Rust_Win_Gui.Utils;

namespace SS_Rust_Win_Gui
{
    public partial class FormMain : Form
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public FormMain()
        {
            InitializeComponent();
        }

        public FormMain(string[] args)
        {
            InitializeComponent();
            foreach (string arg in args)
            {
                if (arg == "-mini")
                {
                    ShowState = false;
                    WindowState = FormWindowState.Minimized;
                }
            }
        }
        private bool ShowState = true;
        protected override void OnShown(EventArgs e)
        {
            if (ShowState)
            {
                base.OnShown(e);
            }
            else
            {
                Hide();
            }
        }



        ConfigData configData = new();
        private static string ConfigDatafile = "config.json";

        private ConfigData LoadConfig()
        {
            ConfigData res = new();

            if (!File.Exists(ConfigDatafile))
            {
                res.servers.Add(new ConfigServer());
                SaveConfig(res);
            }
            else
            {
                using StreamReader file = File.OpenText(ConfigDatafile);
                string value = file.ReadToEnd();
                if (value != null)
                {
                    res = JsonConvert.DeserializeObject<ConfigData>(value) ?? res;
                }
            }
            return res;
        }

        private void SaveConfig(ConfigData configData)
        {
            File.WriteAllText(ConfigDatafile, JsonConvert.SerializeObject(configData, Newtonsoft.Json.Formatting.Indented));
        }



        private async void Main_Load(object sender, EventArgs e)
        {
            string[] mt = await GetMethodsAsync();
            s_server_method.Items.AddRange(mt);
            //isAutoStart = AutoStartUtils.GetAutoStart();
            开机启动ToolStripMenuItem.Checked = AutoStartUtils.GetAutoStart();
            configData = LoadConfig();

            开启系统代理ToolStripMenuItem.Checked = SystemProxyUtils.GetProxyEnable(configData.local_address + ":" + configData.local_port);
            允许局域网访问ToolStripMenuItem.Checked = configData.local_address == "0.0.0.0";

            Bitmap bmp = 开启系统代理ToolStripMenuItem.Checked ? Properties.Resources.ssw128 : Properties.Resources.ss32Outline;
            notifyIcon1.Icon = Icon.FromHandle(bmp.GetHicon());



            listBox1.DisplayMember = "server";

            if (configData.servers.Count > 0)
            {
                if (configData.active_num >= configData.servers.Count)
                {
                    configData.active_num = 0;
                }
                listBox1.SelectedIndexChanged -= new EventHandler(ListBox1_SelectedIndexChanged);
                listBox1.DataSource = configData.servers;
                listBox1.SelectedIndex = configData.active_num;
                ListBox1_SelectedIndexChanged();
                listBox1.SelectedIndexChanged += new EventHandler(ListBox1_SelectedIndexChanged);

            }
            s_local_port.Text = configData.local_port;

        }


        private async Task<string[]> GetMethodsAsync()
        {
            if (File.Exists("sslocal.exe"))
            {
                var result = await Cli.Wrap("sslocal.exe").WithArguments("-m").WithValidation(CommandResultValidation.None).ExecuteBufferedAsync();
                string line = result.StandardError;
                Regex rgx = MyRegex();//中括号[]
                line = rgx.Match(line).Value;//中括号[]
                line = line[(line.IndexOf(':') + 1)..];
                line = line.Replace(" ", "");
                var res = line.Split(',');
                for (int i = 0; i < res.Length; i++)
                {
                    res[i] = res[i].Trim();
                }
                return res;
            }
            else {
                MessageBox.Show("sslocal.exe不存在，即将退出！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return [];
            }
           
        }
        private void ListBox1_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ListBox1_SelectedIndexChanged();
        }

        int preConfigServerIndex = -1;
        private void ListBox1_SelectedIndexChanged()
        {
            if (listBox1.SelectedIndex == -1)
            {
                return;
            }
            if (preConfigServerIndex == listBox1.SelectedIndex)
            {
                return;
            }
            else {
                preConfigServerIndex = listBox1.SelectedIndex;
            }


            ConfigServer configServer = configData.servers.ElementAt(listBox1.SelectedIndex);
            if (configServer is not null)
            {
                if (!configServer.server.Equals("newConfig.com"))
                {
                    LoadServerConfig(configServer);
                    _ = StartSocks5ProxyAsync(configServer);
                    configData.active_num = listBox1.SelectedIndex;
                    SaveConfig(configData);
                    label_save_msg.Text = configServer.server + " 服务已连接！";
                }
                else
                {
                    LoadServerConfig(configServer);
                }
            }
        }

        private void LoadServerConfig(ConfigServer configServer)
        {
            s_server_name.Text = configServer.server;
            s_server_port.Text = configServer.server_port;
            s_server_method.Text = configServer.method;
            s_server_passwd.Text = configServer.password;
            s_server_timout.Text = configServer.timeout;
            s_server_plugin.Text = configServer.plugin;
            s_server_plugin_opt.Text = configServer.plugin_opt;
            s_server_remark.Text = configServer.remark;
        }
        private ConfigServer GetServerConfig()
        {
            ConfigServer configServer = new()
            {
                server = s_server_name.Text,
                server_port = s_server_port.Text,
                method = s_server_method.Text,
                password = s_server_passwd.Text,
                timeout = s_server_timout.Text,
                plugin = s_server_plugin.Text,
                plugin_opt = s_server_plugin_opt.Text,
                remark = s_server_remark.Text
            };
            return configServer;
        }

        private string GetConfigArguments(ConfigServer configServer)
        {
            string arguments = "";
            arguments += " -b \"" + configData.local_address + ":" + configData.local_port + "\"";
            arguments += " -s \"" + configServer.server + ":" + configServer.server_port + "\"";
            arguments += " -m \"" + configServer.method + "\"";
            arguments += " -k \"" + configServer.password + "\"";
            if (configServer.timeout != "")
            {
                arguments += " --timeout \"" + configServer.timeout + "\"";
            }
            if (configServer.plugin != "")
            {
                arguments += " --plugin \"" + configServer.plugin + "\"";
            }
            if (configServer.plugin_opt != "")
            {
                arguments += " --plugin-opts \"" + configServer.plugin_opt + "\"";
            }
            return arguments;
        }


        CancellationTokenSource cts = new();
        ConfigServer lastStartConfig = new();
        private async Task StartSocks5ProxyAsync(ConfigServer configServer)
        {
            cts.Cancel();
            cts = new();
            if (File.Exists("sslocal.exe"))
            {
                notifyIcon1.ShowBalloonTip(3, "sslocal 服务已启动", "SOCKS5://" + configData.local_address + ":" + configData.local_port, ToolTipIcon.Info);
                var cmd = Cli.Wrap("sslocal.exe")
                    .WithValidation(CommandResultValidation.None)
                    .WithArguments(GetConfigArguments(configServer));
                //.ExecuteAsync(cts.Token);
                await foreach (var cmdEvent in cmd.ListenAsync(Encoding.UTF8, cts.Token))
                {
                    switch (cmdEvent)
                    {
                        case StartedCommandEvent started:
                            Logger.Info($"Process started; ID: {started.ProcessId}");
                            break;
                        case ExitedCommandEvent exited:
                            Logger.Info($"Process exited; Code: {exited.ExitCode}");
                            SSLocal_Exited();
                            break;
                        case StandardErrorCommandEvent errored:
                            Logger.Error($"Error: {errored.Text}");
                            break;
                        default:
                            Logger.Info(cmdEvent);
                            break;
                    }
                }
            }
            else {
                MessageBox.Show("sslocal.exe不存在，即将退出！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void SSLocal_Exited()
        {
            notifyIcon1.ShowBalloonTip(0, "错误", "sslocal 运行停止，请检查配置文件和sslocal！", ToolTipIcon.Error);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            cts.Cancel();
        }

        protected override void WndProc(ref Message m)
        {
            var WM_SYSCOMMAND = 0X112;
            var SC_CLOSE = 0XF060;
            if (m.Msg == WM_SYSCOMMAND && m.WParam == (IntPtr)SC_CLOSE)
            {

                Hide();
                return;
            }
            base.WndProc(ref m);
        }



        private void Button_save_Click(object sender, EventArgs e)
        {
            SaveBtnFunc();
            Hide();
        }

        private void Button_apply_Click(object sender, EventArgs e)
        {
            SaveBtnFunc();
        }

        private void SaveBtnFunc()
        {

            ConfigServer configServer = GetServerConfig();
            configData.servers.ElementAt(listBox1.SelectedIndex).SetVal(configServer);
            configData.local_port = s_local_port.Text;
            SaveConfig(configData);
            label_save_msg.Text = "配置文件保存成功！";
            _ = StartSocks5ProxyAsync(configServer);
        }

        private void Button_cancel_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowMainForm();
        }

        private void 打开配置界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowMainForm();
        }
        private void ShowMainForm()
        {
            Show();
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button_add_Click(object sender, EventArgs e)
        {
            ConfigServer configServer = new();
            configData.servers.Add(configServer);

            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        private void Button_remove_Click(object sender, EventArgs e)
        {
            var idnex = listBox1.SelectedIndex;
            configData.servers.RemoveAt(idnex);
            listBox1.SelectedIndex = idnex >= 1 ? idnex - 1 : 0;
            SaveConfig(configData);
        }

        private void Button_copy_Click(object sender, EventArgs e)
        {
            ConfigServer configServer = configData.servers.ElementAt(listBox1.SelectedIndex);
            configData.servers.Add(configServer);
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        private void Button_m_up_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == 0)
            {
                return;
            }
            int index = listBox1.SelectedIndex;
            listBox1.SelectedIndexChanged -= new EventHandler(ListBox1_SelectedIndexChanged);
            ConfigServer configServer = configData.servers.ElementAt(listBox1.SelectedIndex);
            configData.servers.RemoveAt(index);
            configData.servers.Insert(index - 1, configServer);
            listBox1.SelectedIndexChanged += new EventHandler(ListBox1_SelectedIndexChanged);
            listBox1.SelectedIndex = index - 1;
            SaveConfig(configData);
        }

        private void Button_m_down_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= configData.servers.Count - 1)
            {
                return;
            }
            int index = listBox1.SelectedIndex;
            listBox1.SelectedIndexChanged -= new EventHandler(ListBox1_SelectedIndexChanged);
            ConfigServer configServer = configData.servers.ElementAt(listBox1.SelectedIndex);
            configData.servers.RemoveAt(index);
            configData.servers.Insert(index + 1, configServer);
            listBox1.SelectedIndexChanged += new EventHandler(ListBox1_SelectedIndexChanged);
            listBox1.SelectedIndex = index + 1;
            SaveConfig(configData);



        }
        bool showPW = false;
        private void Button_switch_pwd_Click(object sender, EventArgs e)
        {
            showPW = !showPW;
            if (showPW)
            {
                s_server_passwd.PasswordChar = '\0';
                button_switch_pwd.Image = Properties.Resources.eye;
            }
            else
            {
                s_server_passwd.PasswordChar = '*';
                button_switch_pwd.Image = Properties.Resources.no_eye;
            }

        }

        private void 开机启动ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var res = AutoStartUtils.SetMeStart(开机启动ToolStripMenuItem.Checked, "-mini");
            notifyIcon1.ShowBalloonTip(0, "启动项", (开机启动ToolStripMenuItem.Checked ? "添加" : "删除") + (res ? "成功" : "失败"), res ? ToolTipIcon.Info : ToolTipIcon.Error);
        }

        private void 允许局域网访问ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (允许局域网访问ToolStripMenuItem.Checked)
            {
                configData.local_address = "0.0.0.0";
            }
            else
            {
                configData.local_address = "127.0.0.1";
            }
            SaveConfig(configData);
            _ = StartSocks5ProxyAsync(lastStartConfig);
        }

        private void 开启系统代理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ = SystemProxyUtils.SetProxyAsync(开启系统代理ToolStripMenuItem.Checked, configData.local_address + ":" + configData.local_port);
            Bitmap bmp = 开启系统代理ToolStripMenuItem.Checked ? Properties.Resources.ssw128 : Properties.Resources.ss32Outline;
            notifyIcon1.Icon = Icon.FromHandle(bmp.GetHicon());
        }

        private void 查看日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLog formLog = new();
            formLog.Show();
        }
        private void NumberBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9'))
                {
                    e.Handled = true;
                }
            }
        }

        [GeneratedRegex("(?i)(?<=\\[)(.*)(?=\\])", RegexOptions.None, "zh-CN")]
        private static partial Regex MyRegex();
    }
}
