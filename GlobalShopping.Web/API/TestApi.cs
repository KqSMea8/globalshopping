using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GlobalShopping.Core.Model;
using System.Linq;


namespace GlobalShopping.Web.API
{
    public class TestApi: ApiBase
    {
        public override string Name
        {
            get
            {
                return "test";
            }
        }

        public List<TestModel> GetList()
        {
            var list = new List<TestModel>();
            var model1 = new TestModel()
            {
                num=1,
                name="hello"
            };

            var model2 = new TestModel()
            {
                num = 2,
                name = "world"
            };

            list.Add(model1);
            list.Add(model2);

            return list;

        }

        public TestModel Get(int num)
        {
            var list = GetList();
            var model = list.Find(t => t.num == num);
            return model;
        }



        public JObject Test(TestModel model,string desc)
        {
            var test = Request.GetValue("aa");
            var obj = new JObject();
            obj.Add("result", false);
            return obj;
        }

        public JObject TestParam(int num,string message)
        {
            var obj = new JObject();
            obj.Add("num", num);
            obj.Add("message", message);
            return obj;
        }
        public void UploadFile(string content)
        {
            try
            {
                //var content = Request.GetValue("content");
                //var content11 = Request.GetValue<byte[]>("file");
                var files = Request.Files;
                var file = files[0];

                var bytes = Request.GetBytes(file);
                
                var str = System.Text.Encoding.UTF8.GetString(bytes);
            }
            catch(Exception ex)
            {

            }
        }

        public OutlineProduct GetTaobaoDetail()
        {
            //var url = "https://item.taobao.com/item.htm?spm=a217h.9580640.831217.3.48e725aaGtNENr&id=560301470025&scm=1007.12144.81309.70043_0_0&pvid=1644cc49-3d06-46a3-9031-1b8e06843e95&utparam=%7B%22x_hestia_source%22%3A%2270043%22%2C%22x_object_type%22%3A%22item%22%2C%22x_mt%22%3A8%2C%22x_src%22%3A%2270043%22%2C%22x_pos%22%3A3%2C%22x_pvid%22%3A%221644cc49-3d06-46a3-9031-1b8e06843e95%22%2C%22x_object_id%22%3A560301470025%7D";
            //var resposne =GlobalShopping.Core.Services.TaobaoServices.GetTaobaoProduct(url, "", false, false);

            var url = "https://item.taobao.com/item.htm?spm=a217h.9580640.831217.1.7dd925aaBWvHaU&id=16790041596&scm=1007.12144.81309.70043_0_0&pvid=9de295ac-f260-40c3-bea7-d8dd732565e0&utparam=%7B%22x_hestia_source%22%3A%2270043%22%2C%22x_object_type%22%3A%22item%22%2C%22x_mt%22%3A8%2C%22x_src%22%3A%2270043%22%2C%22x_pos%22%3A1%2C%22x_pvid%22%3A%229de295ac-f260-40c3-bea7-d8dd732565e0%22%2C%22x_object_id%22%3A16790041596%7D";

            url = "https://detail.tmall.com/item.htm?id=522063398031&ali_refid=a3_430583_1006:1102388362:N:%E4%B8%83%E5%8C%B9%E7%8B%BC%20%E7%94%B7%E8%A3%85:5f3efe5cd1f3495e71ecbf649b016f20&ali_trackid=1_5f3efe5cd1f3495e71ecbf649b016f20&spm=a230r.1.14.3";
            var product= GlobalShopping.Core.Services.TaobaoService.GetTaobaoProduct(url);

            return product;
        }
    }

    public class TestModel
    {
        public string name { get; set; }

        public int num { get; set; }
    }
}
