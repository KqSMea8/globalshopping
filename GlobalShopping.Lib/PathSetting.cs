using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalShopping.Lib
{
    public class PathSetting
    {

        public static string RootPath
        {
            get
            {
                return System.IO.Path.GetFullPath(".");
#if DEBUG
                return System.IO.Path.GetFullPath("../../../");
#else
                return System.IO.Path.GetFullPath(".");
#endif
                
            }
        }
    }
}
