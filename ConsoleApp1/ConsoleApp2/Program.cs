using System;
using Top;
//using Top.Api;
//using Top.Api.Request;
//using Top.Api.Response;
using Baichuan.Api;
using Baichuan.Api.Response;
using Baichuan.Api.Request;

namespace ConsoleApp2
{
    class Program
    {
        public static string DefaultFields
        {
            get
            {
                return
                    @"num_iid,props_name,skus,approve_status,item_img,property_alias,
                        prop_img,nick,score,pic_url,detail_url,
                        title,nick,desc,price,express_fee,postage_id,cid,props,sku,location";
            }
        }
        static void Main(string[] args)
        {
            //me no permission
            //var appkey = "25313721";
            //var secret = "7baeb2a26918600d6e3a2fa3209c9211";

            //coo
            //var appkey = "23346767";
            //var secret = "0e3e924ebec051da26438087571f242c";

            //d
            //var appkey = "12193480";
            //var secret = "3091a169a0106c59f0e2fb769939ca8a";

            var appkey = "23238713";
            var secret = "35bc06de312ac2999c0b3930ce8c1e56";




            var url = "http://gw.api.taobao.com/router/rest";
            ITopClient client = new DefaultTopClient(url, appkey, secret);

            TaeItemsListRequest req = new TaeItemsListRequest();
            req.Fields = "location,cid,price";
            req.NumIids = "16790041596";
            var response = client.Execute(req);
            
            if(response.Items!=null && response.Items.Count > 0)
            {
                var item = response.Items[0];
                TaeItemDetailGetRequest detailReq = new TaeItemDetailGetRequest();
                detailReq.BuyerIp = "127.0.0.1";
                detailReq.Fields = "itemInfo,priceInfo,skuInfo,stockInfo,descInfo,sellerInfo,mobileDescInfo,deliveryInfo,storeInfo,itemBuyInfo,couponInfo";
                detailReq.Id = item.OpenIid;
                var result = client.Execute(detailReq);


                var itemRequest = new ItemGetRequest
                {
                    Fields = DefaultFields, //如果需要查询的字段为空，则取默认的查询属性
                    NumIid = 16790041596 //查询主键
                };
                var itemResponse = client.Execute(itemRequest);
            }

            


            //TimeGetRequest req = new TimeGetRequest();
            //TimeGetResponse rsp = client.Execute(req);
            Console.WriteLine(response.Body);

        }
    }
}
