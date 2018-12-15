using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalShopping.DataAccess
{
    /// <summary>
    /// Different thread get different DataBaseConnect
    /// </summary>
    public class DataBus
    {
        [ThreadStatic]
        private static IDatabaseConnect ThreadDatabaseConnect;


        public static IDatabaseConnect DataBase
        {
            get
            {
                if (ThreadDatabaseConnect == null)
                {
                    ThreadDatabaseConnect = new DatabaseConnect();
                }
                return ThreadDatabaseConnect;
            }
            
        }
    }
}
