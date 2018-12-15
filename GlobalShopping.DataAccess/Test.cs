using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using GlobalShopping.DataAccess.Model;



namespace GlobalShopping.DataAccess
{
    public class Test
    {
        public List<TestModel> GetTests()
        {
            var parameters = new DynamicParameters();
            parameters.Add("@code", "1");
            
            var models = DataBus.DataBase.ExecProcReturnList<TestModel>("GetTestList", parameters);

            return models;
        }

        public TestModel GetTest()
        {
            var parameters = new DynamicParameters();
            parameters.Add("@code", "1");

            var model = DataBus.DataBase.ExecProcReturnSingle<TestModel>("GetTest", parameters);

            return model;
        }

        public TestModel Select(string code)
        {
            var sql = string.Format("select * from test where code=@code");
            var para = new DynamicParameters();
            para.Add("@code",code);

            var model = DataBus.DataBase.ExecuteSingle<TestModel>(sql, para);

            return model;

        }

        public void Delete(int id)
        {
            var sql = string.Format("delete from test where id=@id");
            var para = new DynamicParameters();
            para.Add("@id", id);
            DataBus.DataBase.ExecuteNoQuery(sql, para);
        }

        public void Update(TestModel model)
        {
            var sql = "";

            DataBus.DataBase.ExecuteNoQuery<TestModel>(sql, model);
        }


    }
}
