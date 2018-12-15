using System;
using System.Collections.Generic;
using System.Text;
using Baichuan.Api;
using System.Configuration;
using Top.Api.Request;
//using Baichuan.Api.Domain;
using Top.Api.Domain;
using Top.Api.Response;
using GlobalShopping.Core.Misc;

namespace GlobalShopping.Core.Services
{
    public class TaobaoManagerCopy
    {
        private readonly Top.Api.ITopClient _client = new Top.Api.DefaultTopClient(TaobaoUrl, AppKey, AppSecret);
        private string _nickName;

        //public readonly Statsd _statsd = new Statsd(StatsdHost, StatsdPort, StatsdClient.ConnectionType.Udp);

        private TaobaoManagerCopy(string newNickName)
        {
            NickName = newNickName;
            var RedisForBaiChuan = ConfigurationManager.AppSettings["BaichuanRedis"];
            
        }


        private static TaobaoManagerCopy _instance = new TaobaoManagerCopy(null);

        public static TaobaoManagerCopy Instance
        {
            get { return _instance; }
        }

        private static string TaobaoUrl
        {
            get { return "http://gw.api.taobao.com/router/rest"; }
        }

        private static string AppKey
        {
            get { return ConfigurationManager.AppSettings["appKey"]; }
        }

        private static string AppSecret
        {
            get { return ConfigurationManager.AppSettings["appSecret"]; }
        }
        private static string BaichuanAppKey
        {
            get { return ConfigurationManager.AppSettings["tabaobaichuanappKey"]; }
        }

        private static string BaichuanAppSecret
        {
            get { return ConfigurationManager.AppSettings["tabaobaichuanappSecret"]; }
        }
        /// <summary>
        ///     获取授权的Session
        /// </summary>
        /// <returns></returns>
        public string Session { get; set; }

        /// <summary>
        ///     自定义默认查询字段,取所有查询字段
        /// </summary>
        public string DefaultFields
        {
            get
            {
                return
                    @"num_iid,props_name,skus,approve_status,item_img,property_alias,
                        prop_img,nick,score,pic_url,detail_url,
                        title,nick,desc,price,express_fee,postage_id,cid,props,sku,location";
            }
        }

        private string NickName
        {
            get
            {
                if (string.IsNullOrEmpty(_nickName))
                    return ConfigurationManager.AppSettings["NickName"];
                return _nickName;
            }
            set { _nickName = value; }
        }

        /// <summary>
        ///     根据淘宝订单ID获取物流信息
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="vendorName"></param>
        /// <returns></returns>
        public string LogisticOrderGet(long tid, string vendorName)
        {
            try
            {
                var response = TraceSearch(tid, vendorName);
                if (response == null || string.IsNullOrEmpty(response.OutSid))
                {
                    return LogisticOrderGetTryAll(tid, vendorName);
                }
                return response.OutSid;
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("isv.invalid-parameter:seller_nick:P17", StringComparison.Ordinal) != -1)
                {
                    return LogisticOrderGetTryAll(tid, vendorName);
                }
            }
            return null;
        }

        /// <summary>
        ///     重复所有的淘宝账号获取运单号
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="vendorName"></param>
        /// <returns></returns>
        private string LogisticOrderGetTryAll(long tid, string vendorName)
        {
            //TaobaoAccountData
            //var taobaoAccounts = TaobaoAccountData.FindByCatalog("");
            //if (taobaoAccounts.TaobaoAccount != null)
            //{
            //    foreach (var taobaoAccount in taobaoAccounts.TaobaoAccount)
            //    {
            //        var appSession = taobaoAccount.AppSession.ToString();
            //        Session = appSession;
            //        var response = TraceSearch(tid, vendorName);
            //        if (response != null && !string.IsNullOrEmpty(response.OutSid)) return response.OutSid;
            //    }
            //}
            return null;
        }

        /// <summary>
        ///     批量查询物流订单
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        /// 对应API：taobao.logistics.orders.get
        public Shipping LogisticOrderGetNotTry(long tid, string key, string secret)
        {
            var request = new LogisticsOrdersGetRequest();
            request.Tid = tid;
            request.BuyerNick = NickName;
            request.Fields =
                "tid,seller_nick,buyer_nick,delivery_start,delivery_end,out_sid,item_title,receiver_name,receiver_mobile,location,status,type,freight_payer,seller_confirm,company_name,is_success,created,modified";
            var response = _client.Execute(request, Session);
            var shippings = response.Shippings;
            if (shippings != null && shippings.Count > 0) return shippings[0];
            return null;
        }

        /// <summary>
        ///     根据淘宝订单号获取运单号
        /// </summary>
        /// <param name="poNumber"></param>
        /// <param name="vendorName"></param>
        /// <returns></returns>
        public string GetWaybill(string poNumber, string vendorName)
        {
            if (!DataConvert.IsLong(poNumber)) return string.Empty;
            var wayBill = LogisticOrderGet(DataConvert.ToInt64(poNumber), vendorName);
            if (!string.IsNullOrEmpty(wayBill))
            {
                return wayBill;
            }
            return string.Empty;
        }

        /// <summary>
        ///     获取单笔交易的详细信息
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        /// 对应API：taobao.trade.fullinfo.get
        public Top.Api.Domain.Trade GetFullTradeInfo(long tid)
        {
            var request = new TradeFullinfoGetRequest();
            request.Tid = tid;
            request.Fields =
                "seller_nick,tid,title,status,buyer_memo,buyer_flag,buyer_message,alipay_no,payment,pay_time,total_fee,post_fee,buyer_alipay_no,commission_fee,created,price,num,snapshot_url,received_payment,orders";
            var response = _client.Execute(request, Session);
            return response.Trade;
        }

        /// <summary>
        ///     获取物流流转信息
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        /// 对应API：taobao.logistics.trace.search
        public LogisticsTraceSearchResponse TraceSearch(long tid, string sellerNick)
        {
            var request = new LogisticsTraceSearchRequest();
            request.Tid = tid;
            request.SellerNick = sellerNick;
            return _client.Execute(request, Session);
        }

        /// <summary>
        ///     根据卖家昵称获取店铺信息
        /// </summary>
        /// <param name="nickName"></param>
        /// <returns></returns>
        public Shop GetShopByNickName(string nickName)
        {
            var req = new ShopGetRequest();
            req.Fields = "sid,cid,title,nick,desc,bulletin,pic_path,created,modified,shop_score";
            req.Nick = nickName;
            var response = _client.Execute(req);
            return response.Shop;
        }

        Baichuan.Api.DefaultTopClient baichuanClient = new Baichuan.Api.DefaultTopClient(TaobaoUrl, BaichuanAppKey, BaichuanAppSecret);

        public Baichuan.Api.Domain.XItem GetProductList(string iid)
        {
            //_statsd.LogCount("Wrangler.APIBaiChuan.GetProductList.count");
            //using (_statsd.LogTiming("Wrangler.APIBaiChuan.GetProductList.timing"))
            //{

            //}

            Baichuan.Api.Request.TaeItemsListRequest reqone = new Baichuan.Api.Request.TaeItemsListRequest();
            reqone.Fields = "location,cid,price";
            reqone.NumIids = iid;
            var baichuanProductList = baichuanClient.Execute(reqone);

            if (baichuanProductList.IsError)
            {
                //TaobaoManager.Instance._statsd.LogCount("Wrangler.APIBaiChuan.GetProductListError.count"); //调用百川GetProductList失败的次数统计
                throw new Exception("百川接口异常");
            }

            if (baichuanProductList.Items.Count == 0)
            {
                //TaobaoManager.Instance._statsd.LogCount("Wrangler.APIBaiChuan.GetProductListOutofStock.count"); //调用百川GetProductList失败的次数统计
                return null;
            }
            return baichuanProductList.Items[0];
        }

        public ItemGetResponse GetItem(string iid)
        {
            ItemGetResponse result;
            //_statsd.LogCount("Wrangler.APIBaiChuan.GetItem.count");
            //using (_statsd.LogTiming("Wrangler.APIBaiChuan.GetItem.timing"))
            //{

            //}
            var request = new ItemGetRequest
            {
                Fields = DefaultFields, //如果需要查询的字段为空，则取默认的查询属性
                NumIid = DataConvert.ToInt64(iid) //查询主键
            };

            result = _client.Execute(request, Session);
            return result;
        }

        public static double GetShippingFee(List<Baichuan.Api.Domain.Carriage> CarriageList)
        {
            if (CarriageList == null || CarriageList.Count == 0)
            {
                return 0;
            }

            if (CarriageList[0].Price == "免运费")
            {
                return 0;
            }

            return DataConvert.ToDouble(CarriageList[0].Price);
        }

        private Tuple<string, long> doGetProductOpenIdAndCid(string iid)
        {
            var item = GetProductList(iid);
            if (item == null)
            {
                return null;
            }
            return new Tuple<string, long>(item.OpenIid, item.Cid);
        }

        //private static RedisValue[] redisFields = new RedisValue[] {
        //    "openid",
        //    "cid"
        //};

        public Tuple<string, long> GetProductOpenIdAndCid(string iid)
        {
            return doGetProductOpenIdAndCid(iid);
            //if (redis == null)
            //{
            //    return doGetProductOpenIdAndCid(iid);
            //}

            //var db = redis.GetDatabase(3);
            //var redisKey = BaichuanAppKey + "-" + iid;

            //var cache = db.HashGet(redisKey, redisFields);

            //if (cache.Length == 2 && cache[0].HasValue && cache[1].HasValue)
            //{
            //    return new Tuple<string, long>(cache[0].ToString(), long.Parse(cache[1].ToString()));
            //}

            //var result = doGetProductOpenIdAndCid(iid);
            //if (result == null)
            //{
            //    return null;
            //}

            //var data = new HashEntry[] {
            //    new HashEntry("openid", result.Item1),
            //    new HashEntry("cid", result.Item2.ToString())
            //};

            //db.HashSet(redisKey, data);
            //return result;
        }

        public UmpPromotionGetResponse GetPromotions(long itemId)
        {
            //_statsd.LogCount("Wrangler.APIBaiChuan.GetPromotions.count");
            //using (_statsd.LogTiming("Wrangler.APIBaiChuan.GetPromotions.timing"))
            //{
               
            //}

            var req = new UmpPromotionGetRequest();
            req.ItemId = itemId;
            return _client.Execute(req);
        }


        public Baichuan.Api.Response.TaeItemDetailGetResponse GetProductDetail(string open_iid)
        {
            //_statsd.LogCount("Wrangler.APIBaiChuan.GetProductDetail.count");
            //using (_statsd.LogTiming("Wrangler.APIBaiChuan.GetProductDetail.timing"))
            //{
                
            //}

            Baichuan.Api.Request.TaeItemDetailGetRequest req = new Baichuan.Api.Request.TaeItemDetailGetRequest();
            req.BuyerIp = "116.228.30.1";
            req.Fields = "itemInfo,priceInfo,skuInfo,stockInfo,descInfo,sellerInfo,mobileDescInfo,deliveryInfo,storeInfo,itemBuyInfo,couponInfo";
            req.Id = open_iid;
            var result = baichuanClient.Execute(req);

            return result;
        }

        public double GetGuangZhouShippingFee(string iid, string open_iid)
        {
            return GetShippingFee(doGetGuangZhouShippingFee(open_iid));
            //string key = "gzfee";
            //if (redis == null)
            //{
            //    return GetShippingFee(doGetGuangZhouShippingFee(open_iid));
            //}

            //var db = redis.GetDatabase(3);
            //var redisKey = BaichuanAppKey + "-" + iid;

            //var cache = db.HashGet(redisKey, new RedisValue[] {
            //        key
            //});

            //if (cache.Length == 1 && cache[0].HasValue)
            //{
            //    return double.Parse(cache[0].ToString());
            //}

            //var result = GetShippingFee(doGetGuangZhouShippingFee(open_iid));
            //var data = new HashEntry[] {
            //    new HashEntry(key, result.ToString())
            //};

            //db.HashSet(redisKey, data);
            //return result;
        }

        public double GetShanghaiShippingFee(string iid, string open_iid)
        {
            return GetShippingFee(doGetShanghaiShippingFee(open_iid));

            //string key = "shfee";
            //if (redis == null)
            //{
            //    return GetShippingFee(doGetShanghaiShippingFee(open_iid));
            //}

            //var db = redis.GetDatabase(3);
            //var redisKey = BaichuanAppKey + "-" + iid;

            //var cache = db.HashGet(redisKey, new RedisValue[] {
            //        key
            //});

            //if (cache.Length == 1 && cache[0].HasValue)
            //{
            //    return double.Parse(cache[0].ToString());
            //}

            //var result = GetShippingFee(doGetShanghaiShippingFee(open_iid));
            //var data = new HashEntry[] {
            //    new HashEntry(key, result.ToString())
            //};

            //db.HashSet(redisKey, data);
            //return result;
        }

        private List<Baichuan.Api.Domain.Carriage> doGetGuangZhouShippingFee(string open_iid)
        {
            //_statsd.LogCount("Wrangler.APIBaiChuan.GetProductDetail.count");
            //using (_statsd.LogTiming("Wrangler.APIBaiChuan.GetProductDetail.timing"))
            //{
                
            //}

            Baichuan.Api.Request.TaeItemDetailGetRequest req = new Baichuan.Api.Request.TaeItemDetailGetRequest();
            req.BuyerIp = "121.201.69.178";
            req.Fields = "deliveryInfo";
            req.Id = open_iid;
            return baichuanClient.Execute(req).Data.DeliveryInfo.CarriageList;
        }

        private List<Baichuan.Api.Domain.Carriage> doGetShanghaiShippingFee(string open_iid)
        {
            //_statsd.LogCount("Wrangler.APIBaiChuan.GetProductDetail.count");
            //using (_statsd.LogTiming("Wrangler.APIBaiChuan.GetProductDetail.timing"))
            //{
                
            //}

            Baichuan.Api.Request.TaeItemDetailGetRequest req = new Baichuan.Api.Request.TaeItemDetailGetRequest();
            req.BuyerIp = "116.228.30.1";
            req.Fields = "deliveryInfo";
            req.Id = open_iid;
            return baichuanClient.Execute(req).Data.DeliveryInfo.CarriageList;
        }
    }
}
