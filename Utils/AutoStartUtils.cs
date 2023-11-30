using Microsoft.Win32;
using System.Diagnostics;

namespace SS_Rust_Win_Gui.Utils
{
    internal class AutoStartUtils
    {

        /// <summary>
        /// 写入或删除注册表键值对,即设为开机启动或开机不启动
        /// </summary>
        /// <param name="isStart">是否开机启动</param>
        /// <param name="exeName">应用程序名</param>
        /// <param name="path">应用程序路径带程序名</param>
        /// <returns></returns>
        private static bool SelfRunning(bool isStart, string exeName, string path)
        {
            try
            {
                RegistryKey local = Registry.CurrentUser;
                RegistryKey? key = local.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (key == null)
                {
                    local.CreateSubKey("SOFTWARE//Microsoft//Windows//CurrentVersion//Run");
                    key = local.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                }
                if (key != null)
                {
                    if (isStart)
                    {
                        key.SetValue(exeName, path);
                        key.Close();
                    }
                    else
                    {
                        string[] keyNames = key.GetValueNames();
                        foreach (string keyName in keyNames)
                        {
                            if (keyName.Equals(exeName, StringComparison.CurrentCultureIgnoreCase))
                            {
                                key.DeleteValue(exeName);
                                key.Close();
                            }
                        }
                    }
                }


            }
            catch (Exception)
            {
                //string ss = ex.Message;
                return false;
            }

            return true;
        }


        /// <summary>
        /// 判断注册键值对是否存在，即是否处于开机启动状态
        /// </summary>
        /// <param name="keyName">键值名</param>
        /// <returns></returns>
        private static bool IsExistKey(string keyName)
        {
            try
            {
                bool _exist = false;
                RegistryKey local = Registry.CurrentUser;
                RegistryKey? runs = local.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (runs == null)
                {
                    RegistryKey key2 = local.CreateSubKey("SOFTWARE");
                    RegistryKey key3 = key2.CreateSubKey("Microsoft");
                    RegistryKey key4 = key3.CreateSubKey("Windows");
                    RegistryKey key5 = key4.CreateSubKey("CurrentVersion");
                    RegistryKey key6 = key5.CreateSubKey("Run");
                    runs = key6;
                }
                string[] runsName = runs.GetValueNames();
                foreach (string strName in runsName)
                {
                    if (strName.Equals(keyName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        _exist = true;
                        return _exist;
                    }
                }
                return _exist;

            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 将本程序设为开启自启
        /// </summary>
        /// <param name="onOff">自启开关</param>
        /// <returns></returns>
        public static bool SetMeStart(bool onOff, string args)
        {

            string appName = Process.GetCurrentProcess().MainModule?.ModuleName ?? "ss-rust-win-gui";
            string appPath = Process.GetCurrentProcess().MainModule?.FileName + " " + args;
            bool isOk = SetAutoStart(onOff, appName, appPath);
            return isOk;
        }
        public static bool GetAutoStart()
        {
            string appName = Process.GetCurrentProcess().MainModule?.ModuleName ?? "ss-rust-win-gui";
            if (appName == null)
            {
                return false;
            }
            return IsExistKey(appName);
        }

        /// <summary>
        /// 将应用程序设为或不设为开机启动
        /// </summary>
        /// <param name="onOff">自启开关</param>
        /// <param name="appName">应用程序名</param>
        /// <param name="appPath">应用程序完全路径</param>
        public static bool SetAutoStart(bool onOff, string appName, string appPath)
        {
            bool isOk = true;
            //如果从没有设为开机启动设置到要设为开机启动
            if (!IsExistKey(appName) && onOff)
            {
                isOk = SelfRunning(onOff, appName, @appPath);
            }
            //如果从设为开机启动设置到不要设为开机启动
            else if (IsExistKey(appName) && !onOff)
            {
                isOk = SelfRunning(onOff, appName, @appPath);
            }
            return isOk;
        }
    }
}
