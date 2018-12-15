using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Configuration;

namespace GlobalShopping.DataAccess
{
    public interface IDatabaseConnect
    {
        void BeginTransaction();
        void Commit();
        void RollBack();
        bool ExecuteNoQuery(string sql, DynamicParameters parameters);
        void ExecuteNoQuery<T>(string sql, T model);
        List<T> ExecuteList<T>(string sql, DynamicParameters parameters);
        T ExecuteSingle<T>(string sql, DynamicParameters parameters);
        //void Insert<T>(T model);
        //void Update<T>(string filter, T model);
        //void Delete<T>(string filter, DynamicParameters parameters);
        //List<T> SelectList<T>(string filter, DynamicParameters parameters);
        //List<T> SelectList<T>(string filter, DynamicParameters parameters, string sort);
        //T SelectOne<T>(string filter, DynamicParameters parameters);
        List<T> ExecProcReturnList<T>(string processName, DynamicParameters parameters);
        void ExecProc(string processName, DynamicParameters parameters);

        T ExecProcReturnSingle<T>(string processName, DynamicParameters parameter);
    }
    public class DatabaseConnect: IDatabaseConnect
    {
        private SqlConnection sqlConnection = null;


        public static string ConnectString { get; set; } = ConfigurationManager.AppSettings["connectionstring"];

        public DatabaseConnect()
        {
            
        }

        public DatabaseConnect(string connectString)
        {
            
        }

        private bool Open()
        {
            if (sqlConnection == null)
            {
                sqlConnection = new SqlConnection(ConnectString);
                sqlConnection.Open();
            }
            else if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            else if (sqlConnection.State == ConnectionState.Broken)
            {
                sqlConnection.Close();
                sqlConnection = new SqlConnection(ConnectString);
                sqlConnection.Open();
            }
            return sqlConnection.State == ConnectionState.Open;
        }
        private void Close()
        {
            if (!isTransactionRun)
                Dispose();
        }
        

        private SqlConnection Connection
        {
            get
            {
                if (sqlConnection == null)
                {
                    sqlConnection = new SqlConnection(ConnectString);
                }
                return sqlConnection;
            }
            set { sqlConnection = null; }
        }

        private IDbTransaction transaction;
        protected IDbTransaction Transaction
        {
            get
            {
                if (null == transaction)
                    BeginTransaction();
                return transaction;
            }
        }
        
        int transactionCount = 0;
        private bool isTransactionRun;
        private bool isRollBack = false;

        public void BeginTransaction()
        {
            if (null == transaction)
            {
                try
                {
                    Open();
                    transaction = this.Connection.BeginTransaction();
                    isTransactionRun = true;
                }
                catch (Exception ex)
                {
                    Close();
                    throw ex;
                }
            }
            transactionCount++;
        }

        public void Commit()
        {
            transactionCount--;
            if (transactionCount <= 0)
            {
                try
                {
                    if (!isRollBack)
                        this.transaction.Commit();
                    else
                        this.transaction.Rollback();
                }
                finally
                {
                    DisposeTransaction();
                }
            }
        }

        public void Dispose()
        {
            SqlConnection conn = this.Connection;
            if (conn != null && !isTransactionRun)		
            {
                this.Connection = null;
                transactionCount = 0;
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
                conn.Dispose();
            }
        }

        public void RollBack()
        {
            isRollBack = true;
            transactionCount--;
            if (transactionCount <= 0)
            {
                try
                {
                    this.transaction.Rollback();
                }
                finally
                {
                    DisposeTransaction();
                }
            }
        }

        private void DisposeTransaction()
        {
            try
            {
                if (null != transaction)
                    transaction.Dispose();
                transaction = null;
                isTransactionRun = false;
                if (transactionCount != 0)
                    transactionCount = 0;
                if (isRollBack)
                    isRollBack = false;
                Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// create,update,Delete operate
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public  bool ExecuteNoQuery(string sql, DynamicParameters parameters)
        {
            int result= Connection.Execute(sql, parameters, transaction);
            return result > 0;
        }

        /// <summary>
        ///  create,update,Delete operate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="model"></param>
        public void ExecuteNoQuery<T>(string sql, T model)
        {
            Connection.Execute(sql, model, transaction);
        }
        /// <summary>
        /// return a list 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<T> ExecuteList<T>(string sql, DynamicParameters parameters)
        {
            return Connection.Query<T>(sql, parameters,transaction).AsList();
        }
        /// <summary>
        /// return single Data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public T ExecuteSingle<T>(string sql, DynamicParameters parameters)
        {
            //return (T) Connection.QuerySingleOrDefault(sql, parameters);
            return (T)Connection.QueryFirstOrDefault<T>(sql, parameters,transaction);
        }

        #region obsolete
        //public void Insert<T>(T model)
        //{
        //    string sql = SimpleSqlHelper.GetInsertSql<T>();
        //    ExecuteNoQuery<T>(sql, model);
        //}

        //public void Update<T>(string filter,T model)
        //{
        //    string sql = SimpleSqlHelper.GetUpdateSql<T>(filter);
        //    ExecuteNoQuery<T>(sql, model);
        //    //ExecuteNoQuery(sql,t)
        //}
        //public void Delete<T>(string filter, DynamicParameters parameters)
        //{
        //    string sql = SimpleSqlHelper.GetDeleteSql<T>(filter);
        //    ExecuteNoQuery(sql, parameters);
        //}

        //public List<T> SelectList<T>(string filter, DynamicParameters parameters)
        //{
        //    return SelectList<T>(filter, parameters, string.Empty);
        //}
        //public List<T> SelectList<T>(string filter, DynamicParameters parameters, string sort)
        //{
        //    string sql = SimpleSqlHelper.GetSelectSql<T>(filter, sort);
        //    return ExecuteList<T>(sql, parameters);
        //}

        //public T SelectOne<T>(string filter, DynamicParameters parameters)
        //{
        //    string sql = SimpleSqlHelper.GetSelectSql<T>(filter,string.Empty);
        //    return ExecuteSingle<T>(sql, parameters);
        //}
        #endregion
        public List<T> ExecProcReturnList<T>(string processName, DynamicParameters parameters)
        {
            return Connection.Query<T>(processName, parameters, null, false, null, CommandType.StoredProcedure).AsList();
        }

        public void ExecProc(string processName,DynamicParameters parameters)
        {
            Connection.Query(processName, parameters,  null, true, null, CommandType.StoredProcedure);
        }

        public T ExecProcReturnSingle<T>(string processName, DynamicParameters parameters)
        {
           return Connection.QueryFirst<T>(processName, parameters, null,3000, CommandType.StoredProcedure);
        }
  
    }


    
}
