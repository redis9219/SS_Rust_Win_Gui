using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SS_Rust_Win_Gui
{
    internal class ConfigServer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ConfigServer()
        {
            serverValue = "newConfig.com";
            server_port = "8088";
            password = "password";
            method = "chacha20-ietf-poly1305";
            timeout = "10";
            remark = "";
            plugin = "";
            plugin_opt = "";
        }
        public void SetVal(ConfigServer configServer)
        {
            serverValue = configServer.server;
            server_port = configServer.server_port;
            password = configServer.password;
            method = configServer.method;
            timeout = configServer.timeout;
            remark = configServer.remark;
            plugin = configServer.plugin;
            plugin_opt = configServer.plugin_opt;
            NotifyPropertyChanged();
        }
        private string serverValue = string.Empty;
#pragma warning disable IDE1006 // 命名样式
        public string server
#pragma warning restore IDE1006 // 命名样式
        {
            get
            {
                return serverValue;
            }
            set
            {
                if (value != serverValue)
                {
                    serverValue = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string server_port;
        public string password;
        public string method;
        public string timeout;
        public string remark;
        public string plugin;
        public string plugin_opt;
    }
}
