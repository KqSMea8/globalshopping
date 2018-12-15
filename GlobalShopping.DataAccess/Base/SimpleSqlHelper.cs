using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GlobalShopping.DataAccess
{
    public class SimpleSqlHelper
    {
        public static string FilterFormat = "{0}='{1}'";
        public static string GetSelectSql<T>(string filter, string sort)
        {
            string selectSql = string.Format("select * from [{0}]", typeof(T).Name);
            if (!string.IsNullOrEmpty(filter))
            {
                selectSql = string.Format("{0} where {1}", selectSql, filter);
            }
            if (!string.IsNullOrEmpty(sort))
            {
                selectSql = string.Format("{0} order by {1}", selectSql, sort);
            }
            return selectSql;
        }
        public static string GetDeleteSql<T>(string filter)
        {
            string deleteSql = string.Format("delete from [{0}]", typeof(T).Name);
            if (!string.IsNullOrEmpty(filter))
            {
                deleteSql = string.Format("{0} where {1}", deleteSql, filter);
            }
            return deleteSql;
        }

        public static string GetInsertSql<T>()
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("insert into [{0}]", typeof(T).Name);

            StringBuilder fieldBuilder = new StringBuilder();
            StringBuilder valueBuilder = new StringBuilder();
            foreach (var item in properties)
            {
                SqlIngoreAttribute[] attrs = item.GetCustomAttributes<SqlIngoreAttribute>() as SqlIngoreAttribute[];
                if (attrs != null && attrs.Length > 0) continue;
                //if(item.Attributes)
                if (fieldBuilder.Length > 0)
                    fieldBuilder.Append(",");
                if (valueBuilder.Length > 0)
                    valueBuilder.Append(",");
                fieldBuilder.AppendFormat("[{0}]", item.Name);
                valueBuilder.AppendFormat("@{0}", item.Name);
            }
            builder.AppendFormat("({0})", fieldBuilder);
            builder.AppendFormat("Values({0})", valueBuilder);

            return builder.ToString();
        }

        public static string GetUpdateSql<T>(string filter)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("update [{0}] set ", typeof(T).Name);

            StringBuilder updateFieldBuilder = new StringBuilder();

            foreach (var item in properties)
            {
                SqlIngoreAttribute[] attrs = item.GetCustomAttributes<SqlIngoreAttribute>() as SqlIngoreAttribute[];
                if (attrs != null && attrs.Length > 0) continue;
                //if(item.Attributes)
                if (updateFieldBuilder.Length > 0)
                    updateFieldBuilder.Append(",");

                updateFieldBuilder.AppendFormat("[{0}]=@{0}", item.Name);
            }
            builder.Append(updateFieldBuilder.ToString());

            builder.AppendFormat(" where {0}", filter);

            return builder.ToString();
        }

        /// <summary>
        /// ToDo Rewrite
        /// </summary>
        /// <param name="field"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static string GetGuidInFilters(string field, IEnumerable<Guid> keys)
        {
            StringBuilder builder = new StringBuilder();
            //if(keys.Count())
            builder.AppendFormat("{0} in ( ", field);

            StringBuilder keyBuilder = new StringBuilder();
            foreach (var key in keys)
            {
                if (keyBuilder.Length > 0)
                    keyBuilder.Append(",");
                keyBuilder.AppendFormat("'{0}'", key.ToString());
            }
            builder.Append(keyBuilder);
            builder.Append(" )");
            return keyBuilder.Length == 0 ? "1!=1" : builder.ToString();
        }


        public static string GetEqFilter(string field)
        {
            return string.Format("{0}=@{0}", field);
        }

    }
}
