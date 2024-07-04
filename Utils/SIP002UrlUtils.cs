using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.Web;

namespace SS_Rust_Win_Gui.Utils
{
    internal class SIP002UrlUtils
    {

        public static BindingList<ConfigServer> GetConfigServers(string urlstr)
        {
            string[] urls = urlstr.Split("\r");
            BindingList<ConfigServer> configServers = new BindingList<ConfigServer>();
            foreach (string url in urls)
            {
                Uri uri = new Uri(url);
                if (uri.Scheme == "ss") {
                    string userInfo =uri.UserInfo;
                    if (!userInfo.Contains(":"))
                    {
                        userInfo = Base64UrlEncoder.Decode(uri.UserInfo);
                    }
                    string[] userInfoArr = userInfo.Split(":");
                    var collection = HttpUtility.ParseQueryString(uri.Query);
                  
                    ConfigServer configServer = new ConfigServer();
                    configServer.server = uri.DnsSafeHost;
                    configServer.server_port = uri.Port.ToString();
                    configServer.method = userInfoArr[0];
                    configServer.password = System.Web.HttpUtility.UrlDecode(userInfoArr[1]) ;

                    var pluginStr = collection["plugin"];
                    if (!string.IsNullOrEmpty(pluginStr))
                    {
                        var pluginParams = pluginStr.Split(';');
                        configServer.plugin = pluginParams[0];
                        configServer.plugin_opt = pluginParams.Length > 1 ? pluginParams[1] : "";
                    }

                    configServers.Add(configServer);
                }
            }
            return configServers;
        }




    }
}
