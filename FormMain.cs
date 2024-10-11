using CliWrap;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using CliWrap.Buffered;
using CliWrap.EventStream;
using System.Text;
using SS_Rust_Win_Gui.Utils;
using System.Diagnostics;
using static System.Windows.Forms.AxHost;
using System.IO;
using System.Windows.Forms;
using System;

namespace SS_Rust_Win_Gui
{
    public partial class FormMain : Form
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly string ApplicationPath = AppDomain.CurrentDomain.BaseDirectory;

        private static readonly string RustAppName = "sslocal";
        private static readonly string RustAppNameWithExt = RustAppName + ".exe";
        private static readonly string RustAppPath = ApplicationPath + RustAppNameWithExt;
        private static string ConfigDatafile = ApplicationPath + "config.json";
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
            SyncContextMenu(res);
            return res;
        }

        private void SaveConfig(ConfigData configData)
        {
            File.WriteAllText(ConfigDatafile, JsonConvert.SerializeObject(configData, Newtonsoft.Json.Formatting.Indented));
            SyncContextMenu(configData);
        }
        private int GetDataSelectNum()
        {
            int selectNum = -1;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                selectNum = dataGridView1.SelectedRows[0].Index;
            }
            return selectNum;
        }


        private void SyncContextMenu(ConfigData configData)
        {
            服务器ToolStripMenuItem.DropDown.Items.Clear();

            var i = 0;
            foreach (ConfigServer configServer in configData.servers)
            {
                string labelTest = configServer.server;
                if (!string.IsNullOrEmpty(configServer.remark))
                {
                    labelTest += " (" + configServer.remark + ")";
                }
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(labelTest);
                toolStripMenuItem.CheckOnClick = true;
                toolStripMenuItem.Tag = i;
                toolStripMenuItem.Click += SeverToolStripMenuItem_Click;
                if (configData.active_num == i)
                {
                    toolStripMenuItem.Checked = true;
                }
                服务器ToolStripMenuItem.DropDown.Items.Add(toolStripMenuItem);
                i++;
            }
        }

        private void SeverToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            if (sender == null)
            {
                return;
            }
            if ((sender as ToolStripMenuItem).Tag is int select_num)
            {
                var i = 0;
                foreach (ConfigServer configServer in configData.servers)
                {
                    if (select_num == i)
                    {
                        configData.active_num = select_num;
                        SaveConfig(configData);
                        _ = StartSocks5ProxyAsync(configServer);
                    }
                    i++;
                }
            }

        }

        private async void Main_Load(object sender, EventArgs e)
        {

            //MessageBox.Show(ApplicationPath);
            string[] mt = await GetMethodsAsync();
            s_server_method.Items.AddRange(mt);
            //isAutoStart = AutoStartUtils.GetAutoStart();
            开机启动ToolStripMenuItem.Checked = AutoStartUtils.GetAutoStart();
            configData = LoadConfig();

            开启系统代理ToolStripMenuItem.Checked = SystemProxyUtils.GetProxyEnable(configData.local_address + ":" + configData.local_port);
            允许局域网访问ToolStripMenuItem.Checked = configData.local_address == "0.0.0.0";

            Bitmap bmp = 开启系统代理ToolStripMenuItem.Checked ? Properties.Resources.ssw128 : Properties.Resources.ss32Outline;
            notifyIcon1.Icon = Icon.FromHandle(bmp.GetHicon());



            // listBox1.DisplayMember = "server";

            if (configData.servers.Count > 0)
            {
                if (configData.active_num >= configData.servers.Count)
                {
                    configData.active_num = 0;
                }
                dataGridView1.SelectionChanged -= new EventHandler(DataList_SelectedIndexChanged);


                dataGridView1.DataSource = configData.servers;

                dataGridView1.Rows[configData.active_num].Selected = true;
                DataList_SelectedIndexChanged();
                dataGridView1.SelectionChanged += new EventHandler(DataList_SelectedIndexChanged);
                dataGridView1.CellClick += DataGridView1_CellClick;

            }
            s_local_port.Text = configData.local_port;


            if (configData.active_num > -1) {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[0].Value = "";
                    if (row.Index == configData.active_num)
                    {
                        row.Cells[0].Value = "已连接";
                    }
                }
                ConnectServer(configData.servers[configData.active_num]);
            }
            
        }
        private void DataGridView1_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "操作")
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[0].Value = "";
                    if (row.Index == e.RowIndex)
                    {
                        row.Cells[0].Value = "已连接";
                    }
                }

                configData.active_num = e.RowIndex;
                SaveConfig(configData);
                ConnectServer(configData.servers[e.RowIndex]);
            }
            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "延迟")
            {
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "...";
                ConfigServer configServer = configData.servers[e.RowIndex];

                new Thread(() =>
                {
                    try
                    {
                        string delay = TcpUtils.TestDelay(configServer.server, int.Parse(configServer.server_port));
                        dataGridView1.BeginInvoke(() =>
                        {
                            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = delay;
                        });
                    }
                    catch (Exception ex)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "Error";
                    }

                }).Start();


            }
        }

        private async Task<string[]> GetMethodsAsync()
        {
            if (File.Exists(RustAppPath))
            {
                var result = await Cli.Wrap(RustAppPath).WithArguments("-m").WithValidation(CommandResultValidation.None).ExecuteBufferedAsync();
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
            else
            {
                MessageBox.Show("sslocal.exe不存在，即将退出！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return [];
            }

        }
        private void ConnectServer(ConfigServer configServer)
        {
            _ = StartSocks5ProxyAsync(configServer);
            label_save_msg.Text = configServer.server + " 服务已连接！";
        }

        private void DataList_SelectedIndexChanged(object? sender, EventArgs e)
        {
            DataList_SelectedIndexChanged();
        }

        int preConfigServerIndex = -1;
        private void DataList_SelectedIndexChanged()
        {
            int selectNum = GetDataSelectNum();
            if (selectNum == -1)
            {
                return;
            }
            if (preConfigServerIndex == selectNum)
            {
                return;
            }
            else
            {
                preConfigServerIndex = selectNum;
            }


            ConfigServer configServer = configData.servers.ElementAt(selectNum);
            if (configServer is not null)
            {
                LoadServerConfig(configServer);

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
            if (File.Exists(RustAppPath))
            {
                notifyIcon1.ShowBalloonTip(3, "sslocal 服务已启动", configServer.remark + "\r\nSOCKS5://" + configData.local_address + ":" + configData.local_port, ToolTipIcon.Info);
                var cmd = Cli.Wrap(RustAppPath)
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
            else
            {
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
            int selectNum = GetDataSelectNum();
            ConfigServer configServer = GetServerConfig();
            configData.servers.ElementAt(selectNum).SetVal(configServer);
            configData.local_port = s_local_port.Text;
            configData.active_num = selectNum;
            SaveConfig(configData);
            label_save_msg.Text = "配置文件保存成功！";
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

        private async void 退出ToolStripMenuItem_ClickAsync(object sender, EventArgs e)
        {
            await Cli.Wrap("taskkill").WithArguments("/f /t /im sslocal.exe").WithValidation(CommandResultValidation.None).ExecuteAsync();
            this.Close();
            Application.Exit();
        }

        private void Button_add_Click(object sender, EventArgs e)
        {
            ConfigServer configServer = new();
            configData.servers.Add(configServer);


            dataGridView1.Rows[dataGridView1.RowCount - 1].Selected = true;

            //listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        private void Button_remove_Click(object sender, EventArgs e)
        {

            int selectNum = GetDataSelectNum();
            if (selectNum > -1)
            {
                configData.servers.RemoveAt(selectNum);
                dataGridView1.Rows[selectNum >= 1 ? selectNum - 1 : 0].Selected = true;
                SaveConfig(configData);
            }



        }

        private void Button_copy_Click(object sender, EventArgs e)
        {
            int selectNum = GetDataSelectNum();

            ConfigServer configServer = configData.servers.ElementAt(selectNum).Clone();
            configData.servers.Add(configServer);
            //listBox1.SelectedIndex = listBox1.Items.Count - 1;
            dataGridView1.Rows[dataGridView1.RowCount - 1].Selected = true;
        }

        private void Button_m_up_Click(object sender, EventArgs e)
        {
            int selectNum = GetDataSelectNum();

            if (selectNum < 1)
            {
                return;
            }

            dataGridView1.SelectionChanged -= new EventHandler(DataList_SelectedIndexChanged);
            ConfigServer configServer = configData.servers.ElementAt(selectNum);
            configData.servers.RemoveAt(selectNum);
            configData.servers.Insert(selectNum - 1, configServer);
            dataGridView1.SelectionChanged += new EventHandler(DataList_SelectedIndexChanged);
            dataGridView1.Rows[selectNum - 1].Selected = true;
            SaveConfig(configData);
        }

        private void Button_m_down_Click(object sender, EventArgs e)
        {
            int selectNum = GetDataSelectNum();
            if (selectNum >= configData.servers.Count - 1)
            {
                return;
            }

            dataGridView1.SelectionChanged -= new EventHandler(DataList_SelectedIndexChanged);
            ConfigServer configServer = configData.servers.ElementAt(selectNum);
            configData.servers.RemoveAt(selectNum);
            configData.servers.Insert(selectNum + 1, configServer);
            dataGridView1.SelectionChanged += new EventHandler(DataList_SelectedIndexChanged);
            dataGridView1.Rows[selectNum + 1].Selected = true;
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



        private void 剪贴板导入ss链接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IDataObject? iData = Clipboard.GetDataObject();
            if (iData != null && iData.GetDataPresent(DataFormats.Text))
            {
                string? str = iData.GetData(DataFormats.Text) as string;
                if (str != null)
                {
                    try
                    {
                        var res = SIP002UrlUtils.GetConfigServers(str);
                        foreach (ConfigServer configServer in res)
                        {
                            configData.servers.Add(configServer);
                        }
                        SaveConfig(configData);
                        notifyIcon1.ShowBalloonTip(0, "导入完成", "本次导入" + res.Count + "个服务器！", ToolTipIcon.Info);
                        return;
                    }
                    catch (Exception)
                    {

                    }

                }
            }
            notifyIcon1.ShowBalloonTip(0, "导入失败", "请检查剪贴板内容！", ToolTipIcon.Error);
        }
    }
}
