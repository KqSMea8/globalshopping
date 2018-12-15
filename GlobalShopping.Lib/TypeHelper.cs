using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalShopping.Lib
{
    public class TypeHelper
    {
        public static object ChangeType(object value, Type type)
        {
            if (value == null)
            {
                if (type.IsValueType)
                {
                    return Activator.CreateInstance(type);
                }
            }

            object result;

            if (type == typeof(String))
            {
                if (!(value is String))
                {
                    if (value == null)
                    {
                        result = "";
                    }
                    else
                    {
                        result = value.ToString();
                    }
                }
                else
                {
                    result = value;
                }
            }
            else if (type == typeof(Guid))
            {
                Guid id;
                if (Guid.TryParse(value.ToString(), out id))
                {
                    return id;
                }
                return default(Guid);
            }
            else if (type == typeof(bool))
            {
                bool ok;
                if (bool.TryParse(value.ToString(), out ok))
                {
                    return ok;
                }
                return false;
            }
            else
            {
                result = Convert.ChangeType(value, type);
            }

            return result;
        }
    }
}
