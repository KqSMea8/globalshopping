using System;
using System.Reflection;
using System.Collections.Generic;

namespace GlobalShopping.Lib
{
    public static class AssemblyLoader
    {
        public static List<Assembly> Assemblies;
        static AssemblyLoader()
        {
            Assemblies = new List<Assembly>();
            var all = AppDomain.CurrentDomain.GetAssemblies();
            foreach(var ass in all)
            {
                if(!ass.GlobalAssemblyCache)
                {
                    if (!IsIgnoredName(ass.FullName))
                    {
                        Assemblies.Add(ass);
                    }
                }
            }
        }

        public static List<Type> LoadByInterface(Type interfaceType)
        {
            List<Type> typelist = new List<Type>();

            foreach (var item in Assemblies)
            {
                foreach (var type in item.GetTypes())
                {
                    if (!type.IsAbstract && !type.IsInterface && !type.IsGenericType && interfaceType.IsAssignableFrom(type))
                    {
                        typelist.Add(type);
                    }
                }
            }

            return typelist;
        }


        private static bool IsIgnoredName(string FullName)
        {
            if (string.IsNullOrEmpty(FullName))
            {
                return true;
            }

            string lower = FullName.ToLower();
            if (lower.StartsWith("mscorlib") || lower.StartsWith("microsoft") 
                || lower.StartsWith("newtonsoft") || lower.StartsWith("vshost"))
            {
                return true;
            }

            return false;

        }
    }
}
