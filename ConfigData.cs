using System.ComponentModel;

namespace SS_Rust_Win_Gui
{
    internal class ConfigData
    {
        public ConfigData() {
            active_num = 0;
            local_port = "1080";
            local_address = "127.0.0.1";
            servers = [];
        }
        public int active_num;
        public string local_port;
        public string local_address;
        public BindingList<ConfigServer> servers;
    }
}
