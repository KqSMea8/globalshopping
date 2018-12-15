using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using GlobalShopping.Core.Utility;
using System.Web;
using GlobalShopping.Core.Model;
using Baichuan.Api;
using Baichuan.Api.Request;
using Baichuan.Api.Response;

namespace GlobalShopping.Core.Services
{
    public class TaobaoService
    {
        
        private static ITopClient client = new DefaultTopClient(TaobaoConfig.Url,TaobaoConfig.AppKey,TaobaoConfig.AppSecret);
        public static string Site
        {
            get { return "taobao"; }
        }
        public static List<Regex> productIdRegexs;
        public static List<Regex> ProductIdRegexs
        {
            get
            {
                if (productIdRegexs == null)
                {
                    productIdRegexs = new List<Regex>();
                    var patterns = EasyBuyXmlUtility.GetTaobaoProductID(Site);
                    foreach (var pattern in patterns)
                    {
                        productIdRegexs.Add(new Regex(pattern));
                    }
                }
                return productIdRegexs;
            }
        }

        private static string GetId(string url)
        {
            foreach (var regex in ProductIdRegexs)
            {
                var m = regex.Match(url);
                if (m.Groups.Count > 0)
                {
                    var id = m.Groups[1].ToString();
                    if (!string.IsNullOrEmpty(id))
                        return id.Trim();
                }
            }
            string itemId = string.Empty;

            itemId = HttpUtility.ParseQueryString(new Uri(url).Query).Get("id");

            if (string.IsNullOrEmpty(itemId)) itemId = url.GetMatchContent("/item/", "\\.htm").TryStringParse();


            return itemId;
        }

        public static OutlineProduct GetTaobaoProduct(string productUrl, bool needTranslate = false)
        {
            var id = GetId(productUrl);

            try
            {
                //can be removed?
                if (TaobaoTortUtility.IsTort(id))
                {
                    return new OutlineProduct { ProductUrl = productUrl, StatusCode = ((int)StatusCode.Instock).ToString() };
                }

                var openIdNCid = GetProductOpenIdAndCid(id);
                if(openIdNCid==null)
                {
                    //todo confirm
                    return new OutlineProduct { ProductUrl = productUrl, StatusCode = StatusCode.Error.ToString() };
                }
                var openIId = openIdNCid.Item1;
                var productDetail = GetProductDetail(openIId);
                
                if(productDetail==null||productDetail.IsError)
                {
                    throw new Exception("Api is error");
                }

                //下架
                //todo change "instock" to "offshelves"
                if (productDetail.ErrCode == "instock" || productDetail.ErrCode == "15")
                {
                    return new OutlineProduct { ProductUrl = productUrl, StatusCode = ((int)StatusCode.Instock).ToString() };
                }
                var cid = openIdNCid.Item2;
                return new OutlineProduct(id, cid, productDetail, needTranslate);
            }
            catch(Exception ex)
            {
                return new OutlineProduct { ProductUrl = productUrl, StatusCode = StatusCode.Error.ToString() };
            }
        }


        private static Tuple<string,long> GetProductOpenIdAndCid(string id)
        {
            var request = new TaeItemsListRequest();
            request.Fields = "location,cid,price";
            request.NumIids = id;
            try
            {
                var response = client.Execute(request);
                if (response != null && response.Items != null && response.Items.Count > 0)
                {
                    var item = response.Items[0];
                    if (item != null)
                        return new Tuple<string, long>(item.OpenIid, item.Cid);
                }
            }
            catch(Exception ex)
            {

            }
            
            return null;
        }

        private static TaeItemDetailGetResponse GetProductDetail(string open_iid)
        {
            var request = new TaeItemDetailGetRequest();
            request.Fields = "itemInfo,priceInfo,skuInfo,stockInfo,descInfo,sellerInfo,mobileDescInfo,deliveryInfo,storeInfo,itemBuyInfo,couponInfo";
            request.Id = open_iid;
            try
            {
                var response = client.Execute(request);
                if (response.Data.ItemInfo.InSale == "false")
                {
                    response.ErrCode = "instock";
                }
                else if (response.Data.StockInfo.ItemQuantity == "0")
                {
                    response.ErrCode = "instock";
                }
                return response;
            }
            catch(Exception ex)
            {

            }
            return null;
        }

    }
}
