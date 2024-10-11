using NLog;
using System.Text;

namespace SS_Rust_Win_Gui
{
    internal static class Program
    {

        public static readonly string LogFileName = AppDomain.CurrentDomain.BaseDirectory+ "output.log";

        [STAThread]
        static void Main(string[] args)
        {

            Mutex instance = new(true, "MutexName", out bool createdNew);
            if (createdNew)
            {
                NLog.LogManager.Setup().LoadConfiguration(builder => {
                    builder.ForLogger().FilterMinLevel(LogLevel.Debug).WriteToColoredConsole(layout: "${message}", encoding: Encoding.UTF8);
                    builder.ForLogger().FilterMinLevel(LogLevel.Debug).WriteToFile(fileName: LogFileName, layout: "${message}", encoding: Encoding.UTF8);
                });
                ApplicationConfiguration.Initialize();
                NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
                Logger.Info("Application Start");

                Application.Run(new FormMain(args));
                instance.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("已经启动了一个程序，请先退出！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

        }
    }
}