using CliWrap;
using CliWrap.Buffered;
using Microsoft.Win32;

namespace SS_Rust_Win_Gui.Utils
{
    internal class SystemProxyUtils
    {

       
        public static async Task<bool> SetProxyAsync(bool enabled, string appAddressAndPort)
        {
            var regPath = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings";
            var result = await Cli.Wrap(@"reg")
                .WithArguments("add \"" + regPath + "\" /v ProxyEnable /t REG_DWORD /d " + (enabled ? "1" : "0") + " /f")
                .WithValidation(CommandResultValidation.None)
                .ExecuteBufferedAsync();
            if (result.ExitCode != 0)
            {
                return false;
            }
            result = await Cli.Wrap(@"reg")
                .WithArguments("add \"" + regPath + "\" /v ProxyServer /t REG_SZ /d \"" + appAddressAndPort + "\" /f")
                .WithValidation(CommandResultValidation.None)
                .ExecuteBufferedAsync();
            if (result.ExitCode != 0)
            {
                return false;
            }
            result = await Cli.Wrap(@"reg")
                .WithArguments("add \"" + regPath + "\" /v ProxyOverride /t REG_SZ /d \"localhost;127.*;192.168.*;<local>\" /f")
                .WithValidation(CommandResultValidation.None)
                .ExecuteBufferedAsync();
            if (result.ExitCode != 0)
            {
                return false;
            }

            NativeUtils.RefishProxy();

            return true;

        }

        public static bool GetProxyEnable(string appAddressAndPort)
        {
            var res = false;
            var regKey = Registry.CurrentUser;

            const string subKeyPath = "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";
            var optionKey = regKey.OpenSubKey(subKeyPath, true);
            if (optionKey != null)
            {
                string proxyEnable = optionKey.GetValue("ProxyEnable")?.ToString() ?? "";

                if (proxyEnable == "1")
                {
                    string addressAndPort = optionKey.GetValue("ProxyServer")?.ToString() ?? "";
                    if (appAddressAndPort == addressAndPort)
                    {
                        res = true;
                    }
                }
            }
            return res;
        }
    }
}

