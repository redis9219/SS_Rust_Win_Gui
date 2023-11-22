using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_Rust_Win_Gui
{
    internal class ConfigData
    {
        public ConfigData() {
            active_num = 0;
            local_port = "127.0.0.1";
            local_address = "1080";
            servers = new BindingList<ConfigServer>();
        }
        public int active_num;
        public string local_port;
        public string local_address;
        public BindingList<ConfigServer> servers { get; set; }
    }
}
