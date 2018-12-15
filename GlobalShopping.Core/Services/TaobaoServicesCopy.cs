using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using GlobalShopping.Core.Utility;
using System.Web;
using GlobalShopping.Core.Model;
using Baichuan.Api.Response;
using System.Linq;
using Newtonsoft.Json;
using System.Dynamic;
using GlobalShopping.Core.Misc;
using System.Globalization;
using System.Configuration;
using Microsoft.CSharp;


namespace GlobalShopping.Core.Services
{
    public class TaobaoServicesCopy
    {
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
        /// <summary>
        ///     通过URL获取淘宝商品ID
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetIid(string url)
        {
            if (!string.IsNullOrEmpty(url) && url.Contains("ezbuy=ezbuy"))
            {
                try
                {
                    var myUri = new Uri(url);
                    return "ezbuy:" + HttpUtility.ParseQueryString(myUri.Query).Get("id");
                }
                catch
                {
                    return string.Empty;
                }
            }

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

        public static string FormatProductUrl(string url)
        {
            var id = GetIid(url);
            if (!string.IsNullOrEmpty(id))
            {
                if (url.IndexOf("tmall.com") != -1)
                {
                    url = "https://detail.tmall.com/item.htm?id=" + id;
                }
                else if (url.IndexOf("taobao.com") != -1)
                {
                    url = "https://item.taobao.com/item.htm?id=" + id;
                }
                else if (url.IndexOf("tmall.hk") != -1)
                {
                    url = "https://detail.tmall.hk/hk/item.htm?id=" + id;
                }
            }
            return url;
        }

        /// <summary>
        ///     根据选择的商品Url，目的地获取商品信息
        /// </summary>
        /// <param name="productUrl"></param>
        /// <param name="destination"></param>
        /// <param name="isFromMobile"></param>
        /// <returns></returns>
        public static OutlineProduct GetTaobaoProduct(string productUrl, string destination, bool isFromMobile, bool needTranslate = false)
        {
            var id = GetIid(productUrl); //获取Id
            return GetTaobaoProductByKey(id, destination, isFromMobile, productUrl, needTranslate);
        }

        /// <summary>
        ///     根据主键获取商品
        /// </summary>
        /// <param name="key"></param>
        /// <param name="destination"></param>
        /// <param name="isFromMobile">Url来源，是否来自移动端</param>
        /// <param name="proUrl"></param>
        /// <returns></returns>
        public static OutlineProduct GetTaobaoProductByKey(string key, string destination, bool isFromMobile, string proUrl, bool needTranslate = false)
        {
            try
            {
                return GetTaobaoProductByBaichuan(key, proUrl, needTranslate);

            }
            catch (Exception e)
            {
                //TaobaoManager.Instance._statsd.LogCount("Wrangler.APITaobao.GetProductListError.count");
                return new OutlineProduct { ProductUrl = proUrl, StatusCode = StatusCode.Error.ToString() };
            }
        }


        static TaobaoServicesCopy()
        {
            //cache
        }

        private static string[] stats = new string[] { "浙江", "江苏", "上海", "广东", "河北", "安徽", "福建", "江西", "山东", "河南", "湖北", "湖南", "海南", "四川", "贵州", "云南", "陕西", "甘肃", "青海", "北京", "天津", "重庆", "山西", "辽宁", "吉林", "黑龙江", "内蒙古", "广西", "宁夏", "西藏", "新疆" };
        public static string GetStateFromLocation(string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                return location;
            }
            foreach (var state in stats)
            {
                if (location.StartsWith(state))
                {
                    return state;
                }
            }

            return location;
        }

        /// <summary>
        ///     过滤到ItemImage的时间字段
        /// </summary>
        /// <param name="itemImgs">当前商品的图片信息集合</param>
        /// <returns></returns>
        private static List<ItemImg> GetItemImage(List<Top.Api.Domain.ItemImg> itemImgs)
        {
            var proItemImages = new List<ItemImg>();
            if (itemImgs.Count <= 0)
            {
                return proItemImages;
            }
            itemImgs.ForEach(p =>
            {
                //过滤掉ItemImage的Create字段
                var proImage = new ItemImg();
                proImage.Id = p.Id;
                proImage.Position = p.Position;
                proImage.Url = p.Url;
                proItemImages.Add(proImage);
            });
            return proItemImages;
        }

        /// <summary>
        ///     初始化商品
        /// </summary>
        /// <param name="item">淘宝Item</param>
        /// <returns></returns>
        private static OutlineProduct InitProduct(Top.Api.Domain.Item item, bool needTranslate = false)
        {
            return new OutlineProduct
            {
                OriginCode = "CN",
                ProductName = item.Title,
                UnitPrice = DataConvert.ToDouble(item.Price),
                ShippingFee = DataConvert.ToDouble(item.ExpressFee),
                Cid = item.Cid,
                ProductUrl = item.DetailUrl,
                VendorName = item.Nick,
                ProductImage = item.PicUrl,
                Location = item.Location.State + "_" + item.Location.City,
                ProductId = item.NumIid,
                Description = item.Desc,
                ItemImgs = GetItemImage(item.ItemImgs),
                PropertyNames = GetPropertiyName(item.PropsName, needTranslate),
                Site = "taobao",
                PriceSymbol = "￥",
                AroundwWarehouse = GetAroundWarehouse(item.Location.State), //获取省份
            };
        }

        /// <summary>
        ///     初始化淘宝店铺信息
        /// </summary>
        /// <param name="outlineProduct"></param>
        /// <param name="item"></param>
        /// <param name="taobaoManager"></param>
        private static void InitProductShop(OutlineProduct outlineProduct,Top.Api.Domain.Item item, TaobaoManagerCopy taobaoManager)
        {
            if (!string.IsNullOrEmpty(item.Nick))
            {
                //获取店铺信息
                Top.Api.Domain.Shop shop = taobaoManager.GetShopByNickName(item.Nick);
               

                if (shop != null)
                {
                    outlineProduct.ShopName = shop.Title;
                    //设置商品是否免运费。
                    //todo 
                    outlineProduct.IsShippingFee = false;
                    //outlineProduct.IsShippingFee = RecommendSellerData.IsShippingFee(shop.Title, shop.Nick);
                    //outlineProduct.ShopScore = GetScore(shop.ShopScore.DeliveryScore, shop.ShopScore.ItemScore,
                    //    shop.ShopScore.ServiceScore);
                }
            }
        }

        /// <summary>
        ///     初始化运费
        /// </summary>
        private static void InitShoppingFee(OutlineProduct outlineProduct, Top.Api.Domain.Item item)
        {
            double fee = double.Parse(item.ExpressFee);
            outlineProduct.SetShippingFee("Shanghai", fee);
            outlineProduct.SetShippingFee("Guangzhou", fee);
            outlineProduct.ShippingFee = fee;
            outlineProduct.SetIsShippingFee();
        }

        /// <summary>
        ///     根据key获取相应的备注信息。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="propertyAlias"></param>
        /// <returns></returns>
        private static string GetPropertyRemark(string key, string propertyAlias)
        {
            if (string.IsNullOrEmpty(propertyAlias)) return null;
            //根据key获取相关的属性备注
            var propAlias = propertyAlias.Split(';').ToList().FirstOrDefault(p => p.StartsWith(key));
            return string.IsNullOrEmpty(propAlias) ? null : propAlias.Split(':')[2];
        }

        /// <summary>
        ///     根据key获取相应的图片Url
        /// </summary>
        /// <param name="key"></param>
        /// <param name="propImgs"></param>
        /// <returns></returns>
        private static string GetPropertyImageUrl(string key, List<Top.Api.Domain.PropImg> propImgs)
        {
            if (propImgs.Count <= 0) return null;
            //根据key获取相关属性的图片Url
            return propImgs.FirstOrDefault(p => p.Properties.Contains(key)) == null
                ? null
                : propImgs.FirstOrDefault(p => p.Properties.Contains(key)).Url;
        }

        /// <summary>
        ///     根据淘宝Api获取某个Sku信息
        /// </summary>
        /// <param name="productSku"></param>
        /// <param name="promotion"></param>
        /// <returns></returns>
        private static SkuItem GetProductSku(Top.Api.Domain.Sku productSku)
        {
            //定义并获取商品的sku信息
            var sku = new SkuItem();
            sku.OriginalPrice = double.Parse(productSku.Price);
            sku.Price = sku.OriginalPrice;
            sku.Properties = productSku.Properties;
            sku.PropertiesName = productSku.PropertiesName;
            sku.Quantity = (int)productSku.Quantity;
            sku.SkuId = productSku.SkuId;
            //sku.SkuSpecId = productSku.SkuSpecId;
            sku.Status = productSku.Status;
            sku.WithHoldQuantity = (int)productSku.WithHoldQuantity;
            return sku;
        }

        /// <summary>
        ///     移除为库存为空的characteristic
        /// </summary>
        /// <param name="characteristics"></param>
        /// <param name="allSku"></param>
        private static void RemveZeroCharacteristics(Dictionary<string, List<CharacteristicItem>> characteristics, List<Top.Api.Domain.Sku> allSku)
        {
            foreach (var characteristic in characteristics.Values)
            {
                var zeroCharacteristics = new List<CharacteristicItem>();
                foreach (var item in characteristic)
                {
                    var totalCount = 0;
                    var quantityCount = 0;
                    foreach (var sku in allSku)
                    {
                        if (sku.Properties.Contains(item.Propkey))
                        {
                            if (sku.Quantity == 0)
                            {
                                quantityCount++;
                            }
                            totalCount++;
                        }
                    }
                    if (totalCount == quantityCount)
                    {
                        zeroCharacteristics.Add(item);
                    }
                }
                zeroCharacteristics.ForEach(p => characteristic.Remove(p));
            }
        }

        /// <summary>
        ///     获取相关特性数据，包括图片集合、Sku 等特性。
        /// </summary>
        private static void GetCharacteristic(Top.Api.Domain.Item product, OutlineProduct outlineProduct, bool needTranslate = false)
        {
            //定义特性集合
            var characteristics = new Dictionary<string, List<CharacteristicItem>>();
            var skus = new List<SkuItem>(); //skus信息集合
            if (product == null || product.Skus.Count <= 0)
            {
                outlineProduct.Characteristics = characteristics; // 商品特性集合。
                outlineProduct.Skus = skus; //商品Sku集合。
            }
            if (product != null)
            {
                product.Skus.ToList().ForEach(t =>
                {
                    //根据分隔符‘；’把每个属性进行分组
                    var properties = t.PropertiesName.Split(';');
                    skus.Add(GetProductSku(t)); //获取相关Sku
                    //分组后，根据每个属性获取属性的Propkey、Name、Remark、ImageUrl信息
                    properties.ToList().ForEach(p =>
                    {
                        var property = p.Split(':');
                        var propkey = property[0] + ":" + property[1];
                        var key = property[2];
                        //定义并获取商品的属性信息
                        var productPropertiy = new CharacteristicItem();
                        productPropertiy.Propkey = propkey;
                        productPropertiy.ActualValue = property[3];
                        productPropertiy.Remark = GetPropertyRemark(propkey, product.PropertyAlias);
                        productPropertiy.ImageUrl = GetPropertyImageUrl(propkey, product.PropImgs);
                        productPropertiy.IsSelected = false; // 是否当前项是被选中
                        //根据商品的属性名称进行分组,处于同一组信息的，整合在一起，
                        if (characteristics.ContainsKey(key))
                        {
                            var characteristic = characteristics[key];
                            if (characteristic.All(a => a.ActualValue != property[3]))
                            {
                                characteristic.Add(productPropertiy);
                            }
                        }
                        else
                        {
                            characteristics.Add(key, new List<CharacteristicItem> { productPropertiy });
                        }
                    });
                });
                RemveZeroCharacteristics(characteristics, product.Skus);
            }
            if (needTranslate)
            {
                List<string> keys = new List<string>();
                List<string> values = new List<string>();
                if (string.IsNullOrEmpty(outlineProduct.AltProductName))
                {
                    values.Add(outlineProduct.ProductName);
                }
                foreach (var character in characteristics)
                {
                    keys.Add(character.Key);
                    foreach (var vd in character.Value)
                    {
                        values.Add(vd.ActualValue);
                        values.Add(vd.Remark);
                    }
                }
                var keysEn = Translate.TranslationToEn(keys, true);
                var valuesEn = Translate.TranslationToEn(values, false);
                string altName;
                if (valuesEn.TryGetValue(outlineProduct.ProductName, out altName))
                {
                    outlineProduct.AltProductName = altName;
                }
                var newCharacteristics = new Dictionary<string, List<CharacteristicItem>>();
                foreach (var character in characteristics)
                {
                    string result;
                    var valuejson = JsonConvert.SerializeObject(character);
                    var notransValue = JsonConvert.DeserializeObject<KeyValuePair<string, List<CharacteristicItem>>>(valuejson);
                    foreach (var vd in notransValue.Value)
                    {
                        if (!string.IsNullOrEmpty(vd.ActualValue))
                            if (valuesEn.TryGetValue(vd.ActualValue, out result))
                                vd.ActualValue = result;
                        if (!string.IsNullOrEmpty(vd.Remark))
                            if (valuesEn.TryGetValue(vd.Remark, out result))
                                vd.Remark = result;
                    }

                    if (keysEn.TryGetValue(character.Key, out result))
                    {
                        newCharacteristics.Add(result, notransValue.Value);
                    }
                    else
                    {
                        newCharacteristics.Add(character.Key, notransValue.Value);
                    }
                }
                outlineProduct.Characteristics = characteristics;
                outlineProduct.AltCharacteristics = newCharacteristics;
            }
            else
            {
                outlineProduct.Characteristics = characteristics; // 商品特性集合。
                outlineProduct.AltCharacteristics = null;
            }
            outlineProduct.Skus = skus; //商品Sku集合。
        }

        private static string KeyPrefix = "getTb_";
        private static string KeyDetailPrefix = "getTbDetail_";

        public static OutlineProduct getTaobaoProductByTaobao(string numIid, string proUrl, bool needTranslate = false)
        {
            var taobaoManager = TaobaoManagerCopy.Instance;
            //todo with cache
            Top.Api.Response.ItemGetResponse result = taobaoManager.GetItem(numIid);
            if (result.SubErrMsg == "ITEM_NOT_FOUND_MESSAGE")
            {
                //SetInstock(numIid);
                //TaobaoManager.Instance._statsd.LogCount("Wrangler.APIBaiChuan.GetItemOutofStock.count");
                return new OutlineProduct { ProductUrl = proUrl, StatusCode = ((int)StatusCode.Instock).ToString() };
            }

            if (result.ErrCode == "15")
            {
                //SetInstock(numIid);
                //TaobaoManager.Instance._statsd.LogCount("Wrangler.APIBaiChuan.GetItemNotApprove.count");
                return new OutlineProduct { ProductUrl = proUrl, StatusCode = ((int)StatusCode.Instock).ToString() };
            }

            if (result.Item == null)
            {
                return new OutlineProduct { ProductUrl = proUrl, StatusCode = ((int)StatusCode.Instock).ToString() };
            }

            if (result.Item.ApproveStatus == "instock")
            {
                //SetInstock(numIid);
                //TaobaoManager.Instance._statsd.LogCount("Wrangler.APIBaiChuan.GetItemNotApprove.count");
                return new OutlineProduct { ProductUrl = proUrl, StatusCode = ((int)StatusCode.Instock).ToString() };
            }

            var outlineProduct = InitProduct(result.Item, needTranslate);
            outlineProduct.TbProductId = numIid;
            InitProductShop(outlineProduct, result.Item, taobaoManager); //初始化店铺信息
            InitShoppingFee(outlineProduct, result.Item);
            GetCharacteristic(result.Item, outlineProduct, needTranslate);

            if (outlineProduct.ProductUrl.Contains("taobao.net"))//taobao返回Url的错误,把taobao.net 变成taobao.com
            {
                outlineProduct.ProductUrl = outlineProduct.ProductUrl.Replace("taobao.net", "taobao.com");
            }

            UpdateProductDetailWithPromotion(outlineProduct);
            return outlineProduct;
        }

        //private static void SetInstock(string refid)
        //{
        //    try
        //    {
        //        var json =
        //            JsonConvert.SerializeObject(
        //                new OutOfStockModel
        //                {
        //                    refId = refid,
        //                    price = 0d,
        //                    isOnSale = false,
        //                    timestamp =
        //                        Convert.ToInt64(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds)
        //                });
        //        RedisValue value = json;
        //        Redis.GetDatabase(4).ListLeftPush("stockstatus", value);
        //    }
        //    catch
        //    {
        //    }
        //}

        private static TaeItemDetailGetResponse GetDetails(string open_iid)
        {
            //todo cache
            TaeItemDetailGetResponse result = TaobaoManagerCopy.Instance.GetProductDetail(open_iid);

            try
            {
                if (result.Data.ItemInfo.InSale == "false")
                {
                    result.ErrCode = "instock";
                }
                else if (result.Data.StockInfo.ItemQuantity == "0")
                {
                    result.ErrCode = "instock";
                }
            }
            catch { }
            return result;
        }

        //通过百川接口获取商品信息
        public static OutlineProduct GetTaobaoProductByBaichuan(string numIid, string proUrl, bool needTranslate = false)
        {
            if (TaobaoTortUtility.IsTort(numIid))
            {
                //SetInstock(numIid);
                //黑名单
                return new OutlineProduct { ProductUrl = proUrl, StatusCode = ((int)StatusCode.Instock).ToString() };
            }

            var taobaoManager = TaobaoManagerCopy.Instance;
            var OpenIidAndCid = taobaoManager.GetProductOpenIdAndCid(numIid);
            if (OpenIidAndCid == null)
            {
                //抓取不到商品的情况
                return getTaobaoProductByTaobao(numIid, proUrl, needTranslate);
            }

            var outlineProduct = new OutlineProduct();
            outlineProduct.ProductId = long.Parse(numIid);
            outlineProduct.TbProductId = numIid;

            var productDetail = GetDetails(OpenIidAndCid.Item1);
            if (productDetail.IsError)
            {
                throw new Exception("百川接口异常");
            }

            if (productDetail.ErrCode == "instock" || productDetail.ErrCode == "15")
            {
                //SetInstock(numIid);
                //抓取不到商品的情况
                return new OutlineProduct { ProductUrl = proUrl, StatusCode = ((int)StatusCode.Instock).ToString() };
            }
            outlineProduct.OriginalUnitPrice = 0;
            outlineProduct.OriginCode = "CN";
            outlineProduct.ProductName = productDetail.Data.ItemInfo.Title;
            outlineProduct.ShippingFee = TaobaoManagerCopy.GetShippingFee(productDetail.Data.DeliveryInfo.CarriageList);

            if (productDetail.Data.SellerInfo.SellerType == "tmall")
            {
                outlineProduct.ProductUrl = "https://detail.tmall.com/item.htm?id=" + numIid;
            }
            else
            {
                outlineProduct.ProductUrl = "https://item.taobao.com/item.htm?id=" + numIid;
            }

            outlineProduct.Cid = OpenIidAndCid.Item2;
            outlineProduct.ShopName = productDetail.Data.SellerInfo.ShopName;
            outlineProduct.VendorName = productDetail.Data.SellerInfo.SellerNick;
            outlineProduct.ProductImage = productDetail.Data.ItemInfo.Pics[0];
            outlineProduct.Location = GetStateFromLocation(productDetail.Data.DeliveryInfo.Location);
            outlineProduct.Site = "taobao";
            outlineProduct.PriceSymbol = "￥";
            outlineProduct.AroundwWarehouse = GetAroundWarehouse(productDetail.Data.DeliveryInfo.Location);

            int stock = 0;
            if (int.TryParse(productDetail.Data.StockInfo.ItemQuantity, out stock))
                outlineProduct.ProductStock = stock;

            outlineProduct.ProductProperties = new Dictionary<string, string>();
            try
            {
                if (productDetail.Data != null && productDetail.Data.ItemInfo != null &&
                    productDetail.Data.ItemInfo.ItemProps != null)
                {
                    outlineProduct.ProductProperties =
                        productDetail.Data.ItemInfo.ItemProps.ToDictionary(a => a.Name,
                            b => b.Value);
                }
            }
            catch { }

            StringBuilder content = new StringBuilder("<p>");
            if (productDetail != null && productDetail.Data != null && productDetail.Data.MobileDescInfo != null &&
                productDetail.Data.MobileDescInfo.DescList != null)
            {
                productDetail.Data.MobileDescInfo.DescList.ForEach(img =>
                {
                    if ("img".Equals(img.Label, StringComparison.OrdinalIgnoreCase))
                        content.Append("<img align=\"absmiddle\" src=\"").Append(img.Content).Append("\">");
                });
            }
            content.Append("</p>");
            outlineProduct.Description = content.ToString();
            outlineProduct.ItemImgs = new List<ItemImg>();
            for (int pi = 0; pi < productDetail.Data.ItemInfo.Pics.Count; pi++)//处理图片
            {
                var proImage = new ItemImg();
                proImage.Id = pi;
                proImage.Position = pi;
                proImage.Url = productDetail.Data.ItemInfo.Pics[pi];
                outlineProduct.ItemImgs.Add(proImage);
            }

            //设置商品是否免运费。
            outlineProduct.SetShippingFee("Shanghai", outlineProduct.ShippingFee.Value);
            outlineProduct.SetShippingFee("Guangzhou", taobaoManager.GetGuangZhouShippingFee(numIid, OpenIidAndCid.Item1));
            outlineProduct.SetIsShippingFee();

            ProcessProduct(productDetail, outlineProduct, 0, needTranslate);
            //taobao返回Url的错误,把taobao.net 变成taobao.com
            if (outlineProduct.ProductUrl.Contains("taobao.net"))
            {
                outlineProduct.ProductUrl = outlineProduct.ProductUrl.Replace("taobao.net", "taobao.com");
            }

            UpdateProductDetailWithPromotion(outlineProduct);

            if (outlineProduct.UnitPrice.HasValue)
            {
                //cache
            }

            return outlineProduct;
        }

        /// <summary>
        ///     根据选择的商品Url、仓库获取国内运费
        /// </summary>
        /// <param name="productUrl">商品Url</param>
        /// <param name="wareHouse">仓库</param>
        /// <returns>运费</returns>
        public static double GetShipmentByWareHouse(string productUrl, string wareHouse)
        {
            try
            {
                if (string.IsNullOrEmpty(wareHouse))
                {
                    return 0;
                }
                var taobaoManager = TaobaoManagerCopy.Instance;
                var iid = GetIid(productUrl);

                if (string.IsNullOrEmpty(iid))
                {
                    return 0;
                }

                var OpenIidAndCid = taobaoManager.GetProductOpenIdAndCid(iid);
                if (OpenIidAndCid == null)
                {
                    return 0;
                }

                if (wareHouse.ToLower() == "shanghai")
                {
                    return taobaoManager.GetShanghaiShippingFee(iid, OpenIidAndCid.Item1);
                }

                return taobaoManager.GetGuangZhouShippingFee(iid, OpenIidAndCid.Item1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return 0;
            }
        }

        /// <summary>
        ///     获取淘宝商品描述
        /// </summary>
        /// <param name="productUrl">商品url</param>
        /// <returns></returns>
        public static dynamic GetProductDescription(string productUrl)
        {
            var id = GetIid(productUrl); //获取Id

            var detail = GetTaobaoProductByBaichuan(id, productUrl, false);

            dynamic des = new ExpandoObject();
            if (detail == null)
            {
                
                des.Description = null;
                des.PropertyNames = new List<string>();
            }
            else
            {
                des.Description = detail.Description;
                des.PropertyNames = detail.PropertyNames;
            }

            return des;
        }

        public static List<KeyValuePair<string, string>> GetSkusByProps(List<List<CharacteristicItem>> totalProps, List<KeyValuePair<string, string>> result, int index)
        {
            if (index < totalProps.Count())
            {
                List<CharacteristicItem> props = totalProps[index];
                if (index == 0)
                {
                    result = props.Select(p => { string k = p.Propkey; string name = p.ActualValue; return new KeyValuePair<string, string>(k, k + ":" + name); }).ToList();
                }
                else
                {
                    result = (from a in result from b in props select new KeyValuePair<string, string>(a.Key + ";" + (string)b.Propkey, a.Value + ";" + (string)b.Propkey + ":" + (string)b.ActualValue)).ToList();
                }
                result = GetSkusByProps(totalProps, result, index + 1);
            }
            return result;
        }

        public static double GetPrice(Baichuan.Api.Domain.PriceUnit price)
        {
            if (price == null)
            {
                return 0;
            }

            double result = 0;
            string priceStr;
            var priceRange = price.Price.Split('-');
            if (priceRange.Count() == 2)
            {
                priceStr = priceRange[0];
            }
            else
            {
                priceStr = price.Price;
            }

            double.TryParse(priceStr, out result);
            return result;
        }

        public static void ProcessProduct(Baichuan.Api.Response.TaeItemDetailGetResponse productDetail, OutlineProduct outlineProduct, float PcMobileSpreadPrice, bool needTranslate = false)
        {
            //定义特性集合
            var characteristics = new Dictionary<string, List<CharacteristicItem>>();
            outlineProduct.OriginalUnitPrice = GetPrice(productDetail.Data.PriceInfo.ItemPrice.Price);
            outlineProduct.UnitPrice = outlineProduct.OriginalUnitPrice;

            var skus = new List<SkuItem>(); //skus信息集合
            List<string> spIds = new List<string>();
            if (productDetail.Data.SkuInfo != null)
            {
                productDetail.Data.SkuInfo.SkuProps.ForEach(t =>
                {
                    var list = new List<CharacteristicItem>();
                    spIds.Add(t.PropId);
                    t.Values.ForEach(pv =>
                    {
                        //定义并获取商品的属性信息
                        var productPropertiy = new CharacteristicItem();
                        productPropertiy.Propkey = t.PropId + ":" + pv.ValueId;
                        productPropertiy.ActualValue = t.PropName + ":" + pv.Name;
                        productPropertiy.Remark = pv.Name;
                        productPropertiy.ImageUrl = pv.ImgUrl;
                        productPropertiy.IsSelected = false; // 是否当前项是被选中
                        list.Add(productPropertiy);
                    });
                    characteristics.Add(t.PropName, list);
                });

                List<KeyValuePair<string, string>> totalPvs = GetSkusByProps(characteristics.Values.ToList(), null, 0);

                double minPrice = outlineProduct.UnitPrice.Value;
                if (totalPvs != null && totalPvs.Count > 0)
                {
                    productDetail.Data.SkuInfo.PvMapSkuList.ForEach(pm =>
                    {
                        string formatPv = "";
                        List<string> pvs = pm.Pv.Split(';').ToList();
                        spIds.ForEach(id =>
                        {
                            if (!string.IsNullOrEmpty(formatPv)) formatPv += ";";
                            formatPv += pvs.Single(p => p.StartsWith(id));
                        });
                        pm.Pv = formatPv;
                    });
                    totalPvs.ForEach(pv =>
                    {
                        var pvMap = productDetail.Data.SkuInfo.PvMapSkuList.SingleOrDefault(pm => pm.Pv.Equals(pv.Key));
                        if (pvMap != null)
                        {
                            var skuPrice = productDetail.Data.PriceInfo.SkuPriceList.SingleOrDefault(price => price.SkuId == pvMap.SkuId);
                            var skuStock = productDetail.Data.StockInfo.SkuQuantityList.SingleOrDefault(stock => stock.SkuId == pvMap.SkuId);

                            var sku = new SkuItem();
                            sku.Price = outlineProduct.UnitPrice.Value;
                            if (skuPrice != null)
                            {
                                if (skuStock != null)
                                {
                                    sku.Quantity = int.Parse(skuStock.Quantity);
                                    if (skuPrice != null)
                                    {
                                        if (skuPrice.PromotionPrice != null)
                                        {
                                            sku.Price = double.Parse(skuPrice.PromotionPrice.Price);
                                            if (PcMobileSpreadPrice > 0 && skuPrice.PromotionPrice.Name.IndexOf("手机") != -1)
                                                sku.Price = sku.Price + PcMobileSpreadPrice;
                                            if (minPrice > sku.Price) minPrice = sku.Price;
                                        }
                                        else
                                        {
                                            sku.Price = double.Parse(skuPrice.Price.Price);
                                            if (minPrice > sku.Price) minPrice = sku.Price;
                                        }
                                        sku.OriginalPrice = double.Parse(skuPrice.Price.Price);
                                    }
                                }
                            }
                            sku.Properties = pvMap.Pv;
                            sku.PropertiesName = pv.Value;
                            sku.SkuId = Int64.Parse(pvMap.SkuId);
                            sku.WithHoldQuantity = sku.Quantity;
                            skus.Add(sku);
                        }
                        else
                        {
                            var sku = new SkuItem();
                            sku.Price = outlineProduct.UnitPrice.Value;
                            sku.Properties = pv.Key;
                            sku.Quantity = 0;
                            sku.PropertiesName = pv.Value;
                            sku.SkuId = 0;
                            sku.WithHoldQuantity = 0;
                            skus.Add(sku);
                        }
                    });
                }

                if (minPrice < outlineProduct.UnitPrice.Value)
                {
                    outlineProduct.UnitPrice = minPrice;
                }
            }
            else
            {
                var promotionPrice = productDetail.Data.PriceInfo.ItemPrice.PromotionPrice;
                if (promotionPrice != null)
                {
                    outlineProduct.UnitPrice = GetPrice(promotionPrice);
                }
                else
                {
                    var price = productDetail.Data.PriceInfo.ItemPrice.Price;
                    outlineProduct.UnitPrice = GetPrice(price);
                }
            }
            //处理属性
            outlineProduct.PropertyNames = new List<string>();
            //处理翻译
            if (needTranslate)
            {
                List<string> keys = new List<string>();
                List<string> values = new List<string>();
                foreach (var character in characteristics)
                {
                    keys.Add(character.Key);
                    foreach (var vd in character.Value)
                    {
                        values.Add(vd.ActualValue);
                        values.Add(vd.Remark);
                    }
                }
                productDetail.Data.ItemInfo.ItemProps.ForEach(p =>
                {
                    keys.Add(p.Name);
                    values.Add(p.Value);
                });
                var keysEn = Translate.TranslationToEn(keys, true);
                var valuesEn = Translate.TranslationToEn(values, false);

                if (characteristics.Count > 0)
                {
                    var newCharacteristics = new Dictionary<string, List<CharacteristicItem>>();
                    foreach (var character in characteristics)
                    {
                        string result;
                        var valuejson = JsonConvert.SerializeObject(character);
                        KeyValuePair<string, List<CharacteristicItem>> notransValue = JsonConvert.DeserializeObject<KeyValuePair<string, List<CharacteristicItem>>>(valuejson);
                        foreach (var vd in notransValue.Value)
                        {
                            if (!string.IsNullOrEmpty(vd.ActualValue))
                                if (valuesEn.TryGetValue(vd.ActualValue, out result))
                                {
                                    vd.ActualValue = result;
                                    result = null;
                                }
                            if (!string.IsNullOrEmpty(vd.Remark))
                                if (valuesEn.TryGetValue(vd.Remark, out result))
                                    vd.Remark = result;
                        }

                        if (keysEn.TryGetValue(character.Key, out result))
                        {
                            newCharacteristics.Add(result, notransValue.Value);
                        }
                        else
                        {
                            newCharacteristics.Add(character.Key, notransValue.Value);
                        }
                    }
                    outlineProduct.AltCharacteristics = newCharacteristics;
                }
                outlineProduct.AltPropertyNames = productDetail.Data.ItemInfo.ItemProps.Select(p =>
                {
                    string key = null, value = null;
                    if (!string.IsNullOrEmpty(p.Name))
                    {
                        if (!keysEn.TryGetValue(p.Name, out key))
                            key = p.Name;
                    }
                    if (!string.IsNullOrEmpty(p.Value))
                    {
                        if (!valuesEn.TryGetValue(p.Value, out value))
                            value = p.Value;
                    }
                    return key + ":" + value;
                }).ToList();
            }
            else
            {
                outlineProduct.PropertyNames = productDetail.Data.ItemInfo.ItemProps.Select(p => p.Name + ":" + p.Value).ToList();
            }
            outlineProduct.Characteristics = characteristics;
            outlineProduct.Skus = skus; //商品Sku集合。
        }

        /// <summary>
        ///     获取商品中规格信息
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static List<string> GetPropertiyName(string propertyName, bool needTranslate = false)
        {
            var resultProperties = new List<string>();
            //获取抓取的Properties
            var properties = new Dictionary<string, string>();
            Dictionary<string, string> keyEns = null;
            Dictionary<string, string> valueEns = null;
            if (!string.IsNullOrEmpty(propertyName))
            {
                if (needTranslate)
                {
                    List<string> keys = new List<string>();
                    List<string> values = new List<string>();
                    propertyName.Split(';').ToList().ForEach(p =>
                    {
                        var splitProperties = p.Split(':');
                        keys.Add(splitProperties[2]);
                        values.Add(splitProperties[3]);
                    });
                    keyEns = Translate.TranslationToEn(keys, true);
                    valueEns = Translate.TranslationToEn(values, false);
                }
                propertyName.Split(';').ToList().ForEach(p =>
                {
                    var splitProperties = p.Split(':');
                    string key = splitProperties[2];
                    string value = splitProperties[3];
                    string media = null;
                    if (keyEns != null)
                    {
                        media = key;
                        if (!keyEns.TryGetValue(key, out key))
                        {
                            key = media;
                        }
                    }
                    if (valueEns != null)
                    {
                        media = value;
                        if (!valueEns.TryGetValue(value, out value))
                        {
                            value = media;
                        }
                    }
                    //存在相同的名称，则合并，已空格区分
                    if (properties.ContainsKey(splitProperties[0]))
                    {
                        var existedProp = properties[splitProperties[0]];
                        var leastProp = existedProp + "  " + value;
                        properties[splitProperties[0]] = leastProp;
                    }
                    else
                    {
                        properties.Add(splitProperties[0], key + ":" + value);
                    }
                });
            }
            resultProperties.AddRange(properties.Values);
            return resultProperties;
        }

        /// <summary>
        ///     获取店铺得分
        /// </summary>
        /// <param name="g1"></param>
        /// <param name="g2"></param>
        /// <param name="g3"></param>
        /// <returns></returns>
        private static string GetScore(string g1, string g2, string g3)
        {
            var score = (DataConvert.ToDouble(g1) + DataConvert.ToDouble(g2) + DataConvert.ToDouble(g3)) / 3;
            if (score >= 5)
            {
                return "5";
            }
            if (score > 4.5)
            {
                return "4.5";
            }
            if (score > 4)
            {
                return "4.0";
            }
            if (score > 3.5)
            {
                return "3.5";
            }
            if (score > 3)
            {
                return "3.0";
            }
            if (score > 2.5)
            {
                return "2.5";
            }
            if (score > 2)
            {
                return "2.0";
            }
            if (score > 1.5)
            {
                return "1.5";
            }
            if (score > 1)
            {
                return "1.0";
            }
            if (score > 0.5)
            {
                return "5";
            }
            return "0";
        }

        public static double GetJuhuasuanPrice(string productUrl)
        {
            var pageContent = DataUtility.GetPageContent(productUrl, null);
            return pageContent.GetMatchContent("<small>&yen;</small>", "</span>").TryDoubleParse();
        }

        /// <summary>
        /// 获取自助购商品
        /// </summary>
        /// <param name="productUrl">商品Url</param>
        /// <returns></returns>
        public static OutlineProduct GetSelfProduct(string productUrl)
        {
            var product = new OutlineProduct();
            //to do implementation mobile
            //if (productUrl.Contains("tb.cn"))
            //{
            //    productUrl = new TaobaoMobileSnatch().GetHtml(productUrl).Header["location"];
            //}

            var id = GetIid(productUrl); //获取Id

            return GetTaobaoProductByBaichuan(id, productUrl, false);
        }


        private static CultureInfo enUS = new CultureInfo("en-US");
        public static bool IsValidPromotionDate(Top.Api.Domain.PromotionInItem promotion)
        {
            DateTime now = DateTime.Now;
            DateTime dt;

            if (!string.IsNullOrEmpty(promotion.StartTime))
            {
                if (DateTime.TryParseExact(promotion.StartTime, "yyyy-MM-dd hh:mm:ss", enUS, DateTimeStyles.None, out dt))
                {
                    if (dt > now)
                    {
                        return false;
                    }
                }
            }

            if (!string.IsNullOrEmpty(promotion.EndTime))
            {
                if (DateTime.TryParseExact(promotion.EndTime, "yyyy-MM-dd hh:mm:ss", enUS, DateTimeStyles.None, out dt))
                {
                    if (dt < now)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static Dictionary<string, double> GetSkuPromotionPrices(Top.Api.Domain.PromotionInItem promotion)
        {
            var result = new Dictionary<string, double>();

            for (int i = 0; i < promotion.SkuIdList.Count; i++)
            {
                double price;
                if (double.TryParse(promotion.SkuPriceList[i], out price))
                {
                    result[promotion.SkuIdList[i]] = price;
                }
            }

            return result;
        }

        private static DateTime getPromotionExpire(List<Top.Api.Domain.PromotionInItem> promotions)
        {
            DateTime expire = DateTime.Today.AddDays(1);
            foreach (var promotion in promotions)
            {
                if (!string.IsNullOrEmpty(promotion.EndTime))
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(promotion.EndTime, "yyyy-MM-dd hh:mm:ss", enUS, DateTimeStyles.None, out dt))
                    {
                        if (dt < expire)
                        {
                            expire = dt;
                        }
                    }
                }
            }
            return expire;
        }

        private static bool updateSkuPriceForDouble11(OutlineProduct data, Dictionary<string, SkuPrices> skuPrices)
        {
            var priceMap = new Dictionary<string, double>();
            double minPrice = data.UnitPrice.Value;
            bool result = false;

            foreach (var sku in data.Skus)
            {
                SkuPrices price;
                if (skuPrices.TryGetValue(sku.SkuId.ToString(), out price))
                {
                    double p;
                    p = price.GetDayPrice();
                    if (p < sku.Price && p > 0)
                    {
                        result = true;
                        sku.Price = p;
                        if (p < minPrice)
                        {
                            minPrice = p;
                        }
                    }
                }
            }

            data.UnitPrice = minPrice;
            return result;
        }

        //private static void updateSkuPrice(OutlineProduct data, HashEntry[] skuPrices)
        //{
        //    var priceMap = new Dictionary<string, double>();
        //    double minPrice = data.UnitPrice.Value;

        //    foreach (var skuPrice in skuPrices)
        //    {
        //        priceMap[skuPrice.Name] = double.Parse(skuPrice.Value);
        //    }

        //    foreach (var sku in data.Skus)
        //    {
        //        double price;
        //        if (priceMap.TryGetValue(sku.SkuId.ToString(), out price))
        //        {
        //            if (price < sku.Price)
        //            {
        //                sku.Price = price;
        //                if (price < minPrice)
        //                {
        //                    minPrice = price;
        //                }
        //            }
        //        }
        //    }

        //    data.UnitPrice = minPrice;
        //}

        //private static string PromotionKeyPrefix = "tb_po_";

        private static bool UpdateProductDetailWithPromotion(OutlineProduct data)
        {
            bool result = false;
            string key = string.Empty;

            //IDatabase db = null;

            //if (RedisForDouble11 != null)
            //{
            //    db = RedisForDouble11.GetDatabase(0);
            //    var json = db.StringGet("sp:" + data.ProductId.Value.ToString());
            //    if (!string.IsNullOrEmpty(json))
            //    {
            //        var itemPrice = JsonConvert.DeserializeObject<GoSpiderData>(json);
            //        if (itemPrice != null && itemPrice.Prices != null)
            //        {
            //            if (updateSkuPriceForDouble11(data, itemPrice.Prices))
            //            {
            //                TaobaoManager.Instance._statsd.LogCount("Wrangler.GoSpider.Hit.count");
            //                return true;
            //            }
            //        }
            //        TaobaoManager.Instance._statsd.LogCount("Wrangler.GoSpider.Invalid.count");
            //    }
            //    else
            //    {
            //        TaobaoManager.Instance._statsd.LogCount("Wrangler.GoSpider.Miss.count");
            //    }
            //}

            double minPrice = data.UnitPrice.Value;
            var taobaoManager = TaobaoManagerCopy.Instance;

            Top.Api.Domain.PromotionDisplayTop promotion = taobaoManager.GetPromotions(data.ProductId.Value).Promotions;

            //var redisKey = PromotionKeyPrefix + data.ProductId.Value;
            //var redisvalue = Redis.GetDatabase(2).StringGet(redisKey);
            //if (redisvalue.HasValue)
            //    promotion = JsonConvert.DeserializeObject<Top.Api.Domain.PromotionDisplayTop>(redisvalue);
            //else
            //{
            //    promotion = taobaoManager.GetPromotions(data.ProductId.Value).Promotions;
            //    Redis.GetDatabase(2)
            //        .StringSet(redisKey, JsonConvert.SerializeObject(promotion), new TimeSpan(0, 1, 0, 0)); //cached duration 1 hours
            //}

            if (promotion != null)
            {
                var promotions = promotion.PromotionInItem;

                foreach (var p in promotions)
                {
                    if (IsValidPromotionDate(p))
                    {
                        var prices = GetSkuPromotionPrices(p);
                        foreach (var sku in data.Skus)
                        {
                            double price;
                            if (prices.TryGetValue(sku.SkuId.ToString(), out price))
                            {
                                if (price < sku.Price)
                                {
                                    sku.Price = price;
                                    result = true;
                                    if (price < minPrice)
                                    {
                                        minPrice = price;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            data.UnitPrice = minPrice;
            if (IsNeedCheckPromotion(data) || promotion == null)
            {
                //TaobaoManager.Instance._statsd.LogCount("Wrangler.APIBaiChuan.PromotionNotFull.count");
                try
                {
                    FetchData(data);
                }
                catch (Exception ex)
                {
                    //TaobaoManager.Instance._statsd.LogCount("Wrangler.APIBaiChuan.PromotionNotFullFault.count");
                }
            }

            return result;
        }

        private static void FetchData(OutlineProduct data)
        {
            if (data == null || data.ProductUrl == null)
                return;

            string url;
            if (data.ProductUrl.Contains("taobao.com"))
            {
                url = ConfigurationManager.AppSettings["gospiderTaobao"];
                if (string.IsNullOrEmpty(url))
                    return;
            }
            else if (data.ProductUrl.Contains("tmall.com"))
            {
                url = ConfigurationManager.AppSettings["gospiderTmall"];
                if (string.IsNullOrEmpty(url))
                    return;
            }
            else
            {
                return;
            }

            url = url + "?id=" + data.GetId();
            var fetchData = HttpHelper.Get<GoSpiderData>(url);
            ReSetSkuPrice(data, fetchData);
        }

        private static void ReSetSkuPrice(OutlineProduct data, GoSpiderData fetchData)
        {
            if (fetchData == null || data == null || data.Skus == null)
                return;

            var minprice = data.UnitPrice.Value;

            foreach (var sku in fetchData.Prices)
            {
                if (sku.Value == null)
                    continue;

                var item = data.Skus.FirstOrDefault(p => p.SkuId.ToString() == sku.Key);
                if (item != null)
                {
                    double promotionPrice;
                    if (double.TryParse(sku.Value.PromotionPrice, out promotionPrice))
                    {
                        if (item.Price > promotionPrice)
                        {
                            item.Price = promotionPrice;

                            if (item.Price < minprice)
                            {
                                minprice = item.Price;
                            }
                        }
                    }
                }
            }

            data.UnitPrice = minprice;
        }

        private static bool IsNeedCheckPromotion(OutlineProduct data)
        {
            bool hasSamePrice = false;
            bool hasDifferntPrice = false;

            foreach (var sku in data.Skus)
            {
                if (sku.OriginalPrice == sku.Price)
                {
                    hasSamePrice = true;
                }
                else
                {
                    hasDifferntPrice = true;
                }
            }

            if (hasSamePrice && hasDifferntPrice)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 根据商品的地点获取临近仓库(仅限用于内部调用)
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        private static string GetAroundWarehouse(string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                return null;
            }
            //warehouse rewrite
            if (location.Contains("上海") || location.Contains("江苏") || location.Contains("浙江") || location.Contains("安徽"))
            {
                return "Near shanghai";
            }
            if (location.Contains("广东") || location.Contains("广西"))
            {
                return "Near guangzhou";
            }
            return null;

        }
    }
}
