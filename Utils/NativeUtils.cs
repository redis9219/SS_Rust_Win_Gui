using System.Runtime.InteropServices;

namespace SS_Rust_Win_Gui.Utils
{
    public partial class NativeUtils
    {

        [LibraryImport("wininet.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool InternetSetOption(nint hInternet, int dwOption, nint lpBuffer, int dwBufferLength);

        public static void RefishProxy()
        {
            const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
            const int INTERNET_OPTION_REFRESH = 37;
            InternetSetOption(nint.Zero, INTERNET_OPTION_SETTINGS_CHANGED, nint.Zero, 0);
            InternetSetOption(nint.Zero, INTERNET_OPTION_REFRESH, nint.Zero, 0);

        }
    }
}
