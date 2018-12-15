using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalShopping.Lib
{
    public class RuntimeSystemHelper
    {
        public static bool IsLinux()
        {
            return System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux);
        }

        public static bool IsWindow()
        {
            return System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows);
        }
    }
}
