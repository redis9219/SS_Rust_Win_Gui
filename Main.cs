using CliWrap;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.RegularExpressions;
using CliWrap.Buffered;

namespace SS_Rust_Win_Gui
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        ConfigData configData = new ConfigData();
        private static string ConfigDatafile = "config.json";

        private ConfigData loadConfig()
        {
            ConfigData res = new ConfigData();
            using (System.IO.StreamReader file = System.IO.File.OpenText(ConfigDatafile))
            {
                string value = file.ReadToEnd();
                if (value != null)
                {
                    res = JsonConvert.DeserializeObject<ConfigData>(value);
                }
            }
            return res;
        }

        private void saveConfig(ConfigData configData)
        {
            System.IO.File.WriteAllText(ConfigDatafile, JsonConvert.SerializeObject(configData, Newtonsoft.Json.Formatting.Indented));
        }



        bool isAutoStart = false;
        private async void Main_Load(object sender, EventArgs e)
        {
            string[] mt = await getMethodsAsync();
            s_server_method.Items.AddRange(mt);
            //isAutoStart = AutoStartUtils.GetAutoStart();
            开机启动ToolStripMenuItem.Checked = AutoStartUtils.GetAutoStart();
            configData = loadConfig();

            开启系统代理ToolStripMenuItem.Checked = SystemProxyUtils.GetProxyEnable(configData.local_address + ":" + configData.local_port);
            允许局域网访问ToolStripMenuItem.Checked = configData.local_address == "0.0.0.0";

            listBox1.DisplayMember = "server";

            if (configData.servers.Count > 0)
            {
                if (configData.active_num > configData.servers.Count)
                {
                    configData.active_num = 0;
                }
                listBox1.SelectedIndexChanged -= new EventHandler(listBox1_SelectedIndexChanged);
                listBox1.DataSource = configData.servers;
                listBox1.SelectedIndex = configData.active_num;
                listBox1_SelectedIndexChanged(null, null);
                listBox1.SelectedIndexChanged += new EventHandler(listBox1_SelectedIndexChanged);
            }
            s_local_port.Text = configData.local_port;

        }


        private async Task<string[]> getMethodsAsync()
        {

            var result = await Cli.Wrap("sslocal.exe").WithArguments("-m").WithValidation(CommandResultValidation.None).ExecuteBufferedAsync();
            string line = result.StandardError;
           
            Regex rgx = new Regex(@"(?i)(?<=\[)(.*)(?=\])");//中括号[]
            line = rgx.Match(line).Value;//中括号[]
            line = line.Substring(line.IndexOf(":") + 1);
            return line.Split(',');
        }



        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigServer configServer = (ConfigServer)listBox1.SelectedItem;
            if (configServer is not null)
            {
                loadServerConfig(configServer);
                startSocks5Proxy(configServer);
                configData.active_num = listBox1.SelectedIndex;
                saveConfig(configData);
            }

        }

        private void loadServerConfig(ConfigServer configServer)
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
        private ConfigServer getServerConfig()
        {
            ConfigServer configServer = new ConfigServer();

            configServer.server = s_server_name.Text;
            configServer.server_port = s_server_port.Text;
            configServer.method = s_server_method.Text;
            configServer.password = s_server_passwd.Text;
            configServer.timeout = s_server_timout.Text;
            configServer.plugin = s_server_plugin.Text;
            configServer.plugin_opt = s_server_plugin_opt.Text;
            configServer.remark = s_server_remark.Text;
            return configServer;
        }


        private string getConfigArguments(ConfigServer configServer)
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




        Process exep = new Process();

        ConfigServer lastStartConfig= new ConfigServer();
        private void startSocks5Proxy(ConfigServer configServer)
        {
            closeSSLocal();
            lastStartConfig = configServer;
            exep = new Process();
            exep.StartInfo.UseShellExecute = false; 
            exep.StartInfo.CreateNoWindow = true; 
            exep.StartInfo.ErrorDialog = false;
            exep.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            exep.StartInfo.FileName = @"sslocal.exe";
            exep.StartInfo.Arguments = getConfigArguments(configServer);
            exep.EnableRaisingEvents = true;
            exep.Exited += new EventHandler(SSLocal_Exited);
            exep.Start();
            restartCnt = 0;
        }

        int restartCnt = 0;
        private void SSLocal_Exited(object sender, EventArgs e)
        {
           
            notifyIcon1.ShowBalloonTip(0, "错误", "sslocal 运行停止，请检查配置文件和sslocal！", ToolTipIcon.Error);
           
        }

        private void closeSSLocal()
        {
            exep.EnableRaisingEvents = false;

            int exepId = 0;
            try
            {
                exepId = exep.Id;
            }
            catch (Exception)
            { }


            if (exepId != 0)
            {
                if (!exep.CloseMainWindow())
                {
                    // 如果进程无响应，则使用 Kill 方法强制关闭进程
                    exep.Kill();
                }
            }
            Process[] p = Process.GetProcessesByName("sslocal.exe");
            foreach (Process p2 in p)
            {
                p2.Kill();
            }


        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            closeSSLocal();
        }
     
        protected override void WndProc(ref Message m)
        {
            var WM_SYSCOMMAND = 0X112;
            var SC_CLOSE = 0XF060;
            if (m.Msg == WM_SYSCOMMAND && m.WParam == (IntPtr)SC_CLOSE)
            {
              
                this.Hide();
                return;
            }
            base.WndProc(ref m);
        }



        private void button_save_Click(object sender, EventArgs e)
        {
            button_apply_Click(null, null);
            this.Hide();
        }

        private void button_apply_Click(object sender, EventArgs e)
        {
            ConfigServer configServer = getServerConfig();
            ((ConfigServer)listBox1.SelectedItem).setVal(configServer);

            configData.servers.ElementAt(listBox1.SelectedIndex).setVal(configServer);
            saveConfig(configData);

            label_save_msg.Text = "配置文件保存成功！";
        }
        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void 打开配置界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            ConfigServer configServer = new ConfigServer();
            configData.servers.Add(configServer);

            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        private void button_remove_Click(object sender, EventArgs e)
        {
            var idnex = listBox1.SelectedIndex;
            configData.servers.RemoveAt(idnex);
            listBox1.SelectedIndex = idnex >= 1 ? idnex - 1 : 0;
            saveConfig(configData);
        }

        private void button_copy_Click(object sender, EventArgs e)
        {
            ConfigServer configServer = (ConfigServer)listBox1.SelectedItem;
            configData.servers.Add(configServer);
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        private void button_m_up_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == 0)
            {
                return;
            }
            int index = listBox1.SelectedIndex;
            listBox1.SelectedIndexChanged -= new EventHandler(listBox1_SelectedIndexChanged);
            ConfigServer configServer = (ConfigServer)listBox1.SelectedItem;
            configData.servers.RemoveAt(index);
            configData.servers.Insert(index - 1, configServer);
            listBox1.SelectedIndexChanged += new EventHandler(listBox1_SelectedIndexChanged);
            listBox1.SelectedIndex = index - 1;
            saveConfig(configData);
        }

        private void button_m_down_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= configData.servers.Count - 1)
            {
                return;
            }
            int index = listBox1.SelectedIndex;
            listBox1.SelectedIndexChanged -= new EventHandler(listBox1_SelectedIndexChanged);
            ConfigServer configServer = (ConfigServer)listBox1.SelectedItem;
            configData.servers.RemoveAt(index);
            configData.servers.Insert(index + 1, configServer);
            listBox1.SelectedIndexChanged += new EventHandler(listBox1_SelectedIndexChanged);
            listBox1.SelectedIndex = index + 1;
            saveConfig(configData);



        }
        bool showPW = false;
        private void button_switch_pwd_Click(object sender, EventArgs e)
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

            var res = AutoStartUtils.SetMeStart(开机启动ToolStripMenuItem.Checked);
            notifyIcon1.ShowBalloonTip(0, "设置启动项", (开机启动ToolStripMenuItem.Checked ? "添加" : "删除") + (res ? "成功" : "失败"), res ? ToolTipIcon.Info : ToolTipIcon.Error);
        }

        bool allowLan = false;
        private void 允许局域网访问ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (允许局域网访问ToolStripMenuItem.Checked)
            {
                configData.local_address = "0.0.0.0";
            }
            else {
                configData.local_address = "127.0.0.1";
            }
            saveConfig(configData);
            startSocks5Proxy(lastStartConfig);
        }

        private void 开启系统代理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemProxyUtils.SetProxyAsync(开启系统代理ToolStripMenuItem.Checked, configData.local_address + ":" + configData.local_port);
        }
    }
}
