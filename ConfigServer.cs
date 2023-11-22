using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SS_Rust_Win_Gui
{
    internal class ConfigServer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ConfigServer()
        {
            server = "newConfig.com";
            server_port = "8088";
            password = "password";
            method = "chacha20-ietf-poly1305";
            timeout = "10";
            remark = "";
            plugin = "";
            plugin_opt = "";
        }
        public void setVal(ConfigServer configServer)
        {
            server = configServer.server;
            server_port = configServer.server_port;
            password = configServer.password;
            method = configServer.method;
            timeout = configServer.timeout;
            remark = configServer.remark;
            plugin = configServer.plugin;
            plugin_opt = configServer.plugin_opt;


        }
        private string serverValue = String.Empty;
        public string server
        {
            get
            {
                return this.serverValue;
            }
            set
            {
                if (value != this.serverValue)
                {
                    this.serverValue = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string server_port { get; set; }
        public string password { get; set; }
        public string method { get; set; }
        public string timeout { get; set; }
        public string remark { get; set; }
        public string plugin { get; set; }
        public string plugin_opt { get; set; }
    }
}
