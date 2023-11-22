using CliWrap;
using CliWrap.Buffered;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SS_Rust_Win_Gui
{
    internal class SystemProxyUtils
    {

        [System.Runtime.InteropServices.DllImport("wininet.dll")]
        private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);

        private const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
        private const int INTERNET_OPTION_REFRESH = 37;
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

            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);

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
                string proxyEnable = optionKey.GetValue("ProxyEnable").ToString();

                if (proxyEnable == "1")
                {
                    string addressAndPort = optionKey.GetValue("ProxyServer").ToString();
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

