using System;
using System.Collections.Generic;
using GlobalShopping.Core.Wrapper;
using GlobalShopping.Core.Misc;
using Baichuan.Api.Response;
using Baichuan.Api.Domain;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace GlobalShopping.Core.Model
{
    public class ItemImg
    {
        public long Id { get; set; }
        public long Position { get; set; }
        public string Url { get; set; }
    }

    public class CharacteristicItem
    {
        public string Propkey { get; set; }
        public string ActualValue { get; set; }
        public string Remark { get; set; }
        public string ImageUrl { get; set; }
        public bool IsSelected { get; set; }
        public string SkuId { get; set; }
    }

    public class SkuItem
    {
        public string Properties { get; set; }
        public string PropertiesName { get; set; }
        public int Quantity { get; set; }
        public int WithHoldQuantity { get; set; }
        public double Price { get; set; }
        public long SkuId { get; set; }
        public string Status { get; set; }
        public double OriginalPrice { get; set; }
        public double UnitDiscount { get; set; }

    }

    /// <summary>
    ///     在线获取商品信息。
    /// </summary>
    public class OutlineProduct
    {
        public OutlineProduct()
        {
            Characteristics = new Dictionary<string, List<CharacteristicItem>>();
            Skus = new List<SkuItem>();
            ItemImgs = new List<ItemImg>();
            PropertyNames = new List<string>();
            StatusCode = "200";
            PriceSymbol = "￥";
            OriginCode = "CN";
            ShippingFee = 0;
            UnitPrice = 0;
            Cid = -1;
            ShippingFees = new Dictionary<string, double>();
        }

        public OutlineProduct(string originCode, string productUrl)
        {
            Characteristics = new Dictionary<string, List<CharacteristicItem>>();
            Skus = new List<SkuItem>();
            ItemImgs = new List<ItemImg>();
            PropertyNames = new List<string>();
            StatusCode = "200";
            OriginCode = originCode;
            ProductUrl = productUrl;
            ShippingFees = new Dictionary<string, double>();
            switch (originCode)
            {

                case "US":
                    PriceSymbol = "USD";
                    ShippingFee = 0;
                    break;
                case "TW":
                    PriceSymbol = "NT$";
                    ShippingFee = 0;
                    break;
                default:
                    PriceSymbol = "￥";
                    ShippingFee = 0;
                    SetOriginShippingFee(productUrl);
                    break;
            }
            UnitPrice = 0;
            Cid = -1;
        }


        public OutlineProduct(string numIid,long cid, TaeItemDetailGetResponse productDetail,bool needTranslate)
        {
            this.ProductId = long.Parse(numIid);
            this.TbProductId = numIid;
            this.OriginalUnitPrice = 0;
            this.OriginCode = "CN";
            this.ProductName = productDetail.Data.ItemInfo.Title;
            //todo confirm shippingfee
            this.ShippingFee = GetShippingFee(productDetail.Data.DeliveryInfo);
            SetIsShippingFee();
            this.ProductUrl = GetProductUrl(productDetail.Data.SellerInfo.SellerType,numIid);

            this.Cid = cid;
            this.ShopName = productDetail.Data.SellerInfo.ShopName;
            this.VendorName = productDetail.Data.SellerInfo.SellerNick;
            this.ProductImage = productDetail.Data.ItemInfo.Pics[0];
            if(productDetail.Data.DeliveryInfo!=null)
            {
                this.Location = GetStateFromLocation(productDetail.Data.DeliveryInfo.Location);
                this.AroundwWarehouse = productDetail.Data.DeliveryInfo.Location;
            }
            
            this.Site = "taobao";
            this.PriceSymbol = "￥";
            //todo change to our warehouse
            
            int stock = 0;
            if (int.TryParse(productDetail.Data.StockInfo.ItemQuantity, out stock))
                this.ProductStock = stock;

            this.ProductProperties = GetProductProperties(productDetail);
            this.Description = GetDescription(productDetail);
            this.ItemImgs = GetItemImages(productDetail);

            ProcessProduct(productDetail, needTranslate);

            if (this.ProductUrl.Contains("taobao.net"))
            {
                this.ProductUrl = this.ProductUrl.Replace("taobao.net", "taobao.com");
            }
            //todo UpdateProductDetailWithPromotion
            //for reset price

        }
        public void ProcessProduct(TaeItemDetailGetResponse productDetail, bool needTranslate = false)
        {
            this.OriginalUnitPrice = GetPrice(productDetail.Data.PriceInfo.ItemPrice.Price);
            this.UnitPrice = this.OriginalUnitPrice;
            this.Characteristics = GetCharacterisDic(productDetail);
            this.Skus = GetSkuItems(productDetail);

            UpdateUnitPrice(this.Skus, productDetail);

            this.PropertyNames = new List<string>();
            if (needTranslate)
            {
                List<string> keys = new List<string>();
                List<string> values = new List<string>();
                foreach (var character in this.Characteristics)
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

                this.AltCharacteristics = GetAltCharacteristics(productDetail,keysEn,valuesEn);
                this.AltPropertyNames = GetAltPropertyNames(productDetail, keysEn, valuesEn);
            }
            else
            {
                this.PropertyNames= productDetail.Data.ItemInfo.ItemProps.Select(p => p.Name + ":" + p.Value).ToList();
            }

        }

        
        private Dictionary<string, List<CharacteristicItem>> GetAltCharacteristics(TaeItemDetailGetResponse productDetail, Dictionary<string, string> keysEn, Dictionary<string, string> valuesEn)
        {

            var newCharacteristics = new Dictionary<string, List<CharacteristicItem>>();
            if (this.Characteristics.Count > 0)
            {
                
                foreach (var character in this.Characteristics)
                {
                    string result;
                    var valuejson = JsonConvert.SerializeObject(character);
                    
                    KeyValuePair<string, List<CharacteristicItem>> cloneCharacter = JsonConvert.DeserializeObject<KeyValuePair<string, List<CharacteristicItem>>>(valuejson);
                    foreach (var vd in cloneCharacter.Value)
                    {
                        //translate value
                        if (!string.IsNullOrEmpty(vd.ActualValue) && valuesEn.TryGetValue(vd.ActualValue, out result))
                        {
                            vd.ActualValue = result;
                            result = null;
                        }

                        if (!string.IsNullOrEmpty(vd.Remark) && valuesEn.TryGetValue(vd.Remark, out result))
                        {
                            vd.Remark = result;
                        }

                    }

                    if (keysEn.TryGetValue(character.Key, out result))
                    {
                        newCharacteristics.Add(result, cloneCharacter.Value);
                    }
                    else
                    {
                        newCharacteristics.Add(character.Key, cloneCharacter.Value);
                    }
                }
               
            }
            return newCharacteristics;
        }

        private List<string> GetAltPropertyNames(TaeItemDetailGetResponse productDetail, Dictionary<string, string> keysEn, Dictionary<string, string> valuesEn)
        {
            var properyNames = new List<string>();
            properyNames=productDetail.Data.ItemInfo.ItemProps.Select(p =>
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
            return properyNames;
        }


        private void UpdateUnitPrice(List<SkuItem> skus, TaeItemDetailGetResponse productDetail)
        {
            if(productDetail.Data.SkuInfo != null)
            {
                if (skus.Count > 0)
                {
                    var sortSkus = skus.OrderBy(s => s.Price).ToList();
                    var minPrice = sortSkus[0].Price;
                    if (minPrice < this.UnitPrice.Value)
                    {
                        this.UnitPrice = minPrice;
                    }
                }
            }
            else
            {
                var promotionPrice = productDetail.Data.PriceInfo.ItemPrice.PromotionPrice;
                if (promotionPrice != null)
                {
                    this.UnitPrice = GetPrice(promotionPrice);
                }
                else
                {
                    var price = productDetail.Data.PriceInfo.ItemPrice.Price;
                    this.UnitPrice = GetPrice(price);
                }
            }
        }
        private Dictionary<string, List<CharacteristicItem>> GetCharacterisDic(TaeItemDetailGetResponse productDetail)
        {
            var characteristics = new Dictionary<string, List<CharacteristicItem>>();
            if(productDetail.Data.SkuInfo != null)
            {
                productDetail.Data.SkuInfo.SkuProps.ForEach(s =>
                {
                    var list = new List<CharacteristicItem>();
                    s.Values.ForEach(pv =>
                    {
                        //定义并获取商品的属性信息
                        var productPropertiy = new CharacteristicItem();
                        productPropertiy.Propkey = s.PropId + ":" + pv.ValueId;
                        productPropertiy.ActualValue = s.PropName + ":" + pv.Name;
                        productPropertiy.Remark = pv.Name;
                        productPropertiy.ImageUrl = pv.ImgUrl;
                        productPropertiy.IsSelected = false; // 是否当前项是被选中
                        list.Add(productPropertiy);
                    });
                    characteristics.Add(s.PropName, list);
                });
            }
            return characteristics;
        }
        private List<SkuItem> GetSkuItems(TaeItemDetailGetResponse productDetail)
        {
            var skus = new List<SkuItem>();

            var characteristics =this.Characteristics;

            if (productDetail.Data.SkuInfo != null)
            {
                #region get skus
                List<string> spIds = productDetail.Data.SkuInfo.SkuProps.Select(sp => sp.PropId).ToList();

                //all combination
                List<KeyValuePair<string, string>> allPvs= GetSkusByProps(characteristics.Values.ToList(), null, 0);
                
                if (allPvs!=null && allPvs.Count>0)
                {
                    //format pv
                    var pvMapSkuList=new List<PvMapSku>();
                    productDetail.Data.SkuInfo.PvMapSkuList.ForEach(pm =>
                    {
                        string formatPv = "";
                        List<string> pvs = pm.Pv.Split(';').ToList();
                        spIds.ForEach(id =>
                        {
                            if (!string.IsNullOrEmpty(formatPv)) formatPv += ";";
                            formatPv += pvs.Single(p => p.StartsWith(id));
                        });
                        var pvMapSku = new PvMapSku()
                        {
                            Pv=formatPv,
                            SkuId=pm.SkuId
                        };
                        pvMapSkuList.Add(pvMapSku);
                    });

                    allPvs.ForEach(pv =>
                    {
                        var pvMap = pvMapSkuList.SingleOrDefault(pm => pm.Pv.Equals(pv.Key));
                        if (pvMap != null)
                        {
                            var skuPrice = productDetail.Data.PriceInfo.SkuPriceList.SingleOrDefault(price => price.SkuId == pvMap.SkuId);
                            var skuStock = productDetail.Data.StockInfo.SkuQuantityList.SingleOrDefault(stock => stock.SkuId == pvMap.SkuId);
                            var sku = new SkuItem();
                            sku.Price = this.UnitPrice.Value;
                            
                            if(skuPrice!=null && skuStock!=null)
                            {
                                if (skuPrice.PromotionPrice != null)
                                {
                                    sku.Price = double.Parse(skuPrice.PromotionPrice.Price);
                                }
                                else
                                {
                                    sku.Price = double.Parse(skuPrice.Price.Price);
                                }
                                sku.OriginalPrice = double.Parse(skuPrice.Price.Price);
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
                            sku.Price = this.UnitPrice.Value;
                            sku.Properties = pv.Key;
                            sku.Quantity = 0;
                            sku.PropertiesName = pv.Value;
                            sku.SkuId = 0;
                            sku.WithHoldQuantity = 0;
                            skus.Add(sku);
                        }
                    });

                }

                #endregion 
            }
            return skus;
        }
        /// <summary>
        /// get all property combination
        /// </summary>
        /// <param name="totalProps"></param>
        /// <param name="result"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static List<KeyValuePair<string, string>> GetSkusByProps(List<List<CharacteristicItem>> totalProps, List<KeyValuePair<string, string>> result, int index)
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
        private double GetPrice(PriceUnit price)
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

        private List<ItemImg> GetItemImages(TaeItemDetailGetResponse productDetail)
        {
            var itemImgs = new List<ItemImg>();
            for (int pi = 0; pi < productDetail.Data.ItemInfo.Pics.Count; pi++)//处理图片
            {
                var proImage = new ItemImg();
                proImage.Id = pi;
                proImage.Position = pi;
                proImage.Url = productDetail.Data.ItemInfo.Pics[pi];
                itemImgs.Add(proImage);
            }
            return itemImgs;
        }
        private string GetDescription(TaeItemDetailGetResponse productDetail)
        {
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

            return content.ToString();
        }
        private Dictionary<string,string> GetProductProperties(TaeItemDetailGetResponse productDetail)
        {
            var dic = new Dictionary<string, string>();
            try
            {
                if (productDetail.Data != null && productDetail.Data.ItemInfo != null &&
                    productDetail.Data.ItemInfo.ItemProps != null)
                {
                    dic=productDetail.Data.ItemInfo.ItemProps.ToDictionary(a => a.Name,
                            b => b.Value);
                }
            }
            catch { }
            return dic;
        }
        private string GetProductUrl(string sellerType,string numIid)
        {
            return sellerType == "tmall"
                ? "https://detail.tmall.com/item.htm?id=" + numIid
                : "https://item.taobao.com/item.htm?id=" + numIid;
 
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
        private double GetShippingFee(DeliveryInfo deliveryInfo)
        {
            //DeliveryInfo
            if (deliveryInfo == null) return 0;
            var CarriageList = deliveryInfo.CarriageList;
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
        /// <summary>
        ///     商品类型主键
        /// </summary>
        public long Cid { get; set; }

        /// <summary>
        ///     店铺拥有者。
        /// </summary>
        public string VendorName { get; set; }

        /// <summary>
        ///     商品名称。
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        ///     价格
        /// </summary>
        public double? UnitPrice { get; set; }

        /// <summary>
        ///     运费
        /// </summary>
        public double? ShippingFee { get; set; }

        /// <summary>
        ///     商品Url
        /// </summary>
        public string ProductUrl { get; set; }

        /// <summary>
        ///     商品图片
        /// </summary>
        public string ProductImage { get; set; }

        /// <summary>
        ///     地区，tw:代购台湾，cn代购中国大陆。
        /// </summary>
        public string OriginCode { get; set; }

        /// <summary>
        ///     品牌。
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// 店铺信息
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        ///     获取相关特性数据，包括图片集合、Sku 等特性
        /// </summary>
        public Dictionary<string, List<CharacteristicItem>> Characteristics { get; set; }
        /// <summary>
        ///     获取相关特性数据，包括图片集合、Sku 等特性[翻译后的信息]
        /// </summary>
        public Dictionary<string, List<CharacteristicItem>> AltCharacteristics { get; set; }
        /// <summary>
        /// 商品相关Skus
        /// </summary>
        public List<SkuItem> Skus { get; set; }

        /// <summary>
        /// 商品小图像
        /// </summary>
        public List<ItemImg> ItemImgs { get; set; }

        /// <summary>
        /// 商品位置信息
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        public long? ProductId { get; set; }

        /// <summary>
        ///商品描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 商品描述中，规格信息
        /// </summary>
        public List<string> PropertyNames { get; set; }
        public List<string> AltPropertyNames { get; set; }

        /// <summary>
        /// 商品来源网站
        /// </summary>
        public string Site { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string AltProductName { get; set; }
        /// <summary>
        /// 处理后的图片列表
        /// </summary>
        public string ProductImages { get; set; }
        /// <summary>
        /// 处理后的商品描述
        /// </summary>
        public string ProductDescription { get; set; }
        /// <summary>
        /// 商品状态，默认为200，30001表示商品已下架。
        /// </summary>
        public string StatusCode { get; set; }

        /// <summary>
        /// 商品价格符号。
        /// </summary>
        public string PriceSymbol { get; set; }

        /// <summary>
        /// 靠近仓库（目前仓库有上海、广州）。
        /// </summary>
        public string AroundwWarehouse { get; set; }

        /// <summary>
        /// 是否处于黑名单中。
        /// </summary>
        public bool IsBlack { get; set; }

        /// <summary>
        /// 是否免运费。
        /// </summary>
        public bool IsShippingFee { get; set; }
        /// <summary>
        /// 店铺评分
        /// </summary>
        public string ShopScore { get; set; }

        /// <summary>
        /// 运费集合
        /// </summary>
        public Dictionary<string, double> ShippingFees { get; set; }

        /// <summary>
        /// 设置初始化运费值，淘宝的商品是通过API获取到的，而其他的网站到上海或广州都默认为10元
        /// </summary>
        /// <param name="productUrl">商品Url</param>
        private void SetOriginShippingFee(string productUrl)
        {
            if (ShippingFees == null)
                ShippingFees = new Dictionary<string, double>();

            if (string.IsNullOrEmpty(productUrl))
                return;

            if (productUrl.IndexOf("taobao.com") >= 0 || productUrl.IndexOf("tmall.com") >= 0)
                return;

            if (productUrl.Contains(SpikeProductManager.identity))
                return;

            ShippingFees.Add("Shanghai", 10);
            ShippingFees.Add("Guangzhou", 10);
        }

        /// <summary>
        /// 设置运费
        /// </summary>
        /// <param name="destination">目的地</param>
        /// <param name="fee">费用</param>
        public void SetShippingFee(string destination, double fee)
        {
            ShippingFees.Add(destination, fee);
        }

        public void SetIsShippingFee()
        {
            
            IsShippingFee = true;
            if (ShippingFees != null)
            {
                foreach (var fee in ShippingFees.Values)
                {
                    if (fee > 0)
                    {
                        IsShippingFee = false;
                        return;
                    }
                }
            }
               
        }

        /// <summary>
        /// 内容获取的途径，有API,Taobo Website获取其他第三方网站
        /// </summary>
        public string FetchBy { get; set; }

        /// <summary>
        /// 是否是自定义的店铺（我们有时会把5，6个店铺合并成一个店铺）
        /// </summary>        
        public bool IsCustomShop { get; set; }

        /// <summary>
        /// 淘宝Id
        /// </summary>
        public string TbProductId { get; set; }

        /// <summary>
        /// 是否是一次付款
        /// </summary>
        public bool IsEzShipping { get; set; }

        /// <summary>
        /// 建模得到的重量
        /// </summary>
        public decimal? EstWeight { get; set; }

        /// <summary>
        /// 建模得到的体积重
        /// </summary>
        public decimal? EstVolumeWeight { get; set; }

        /// <summary>
        /// 是否是真实的种类
        /// </summary>
        public bool IsExactWeight { get; set; }

        /// <summary>
        /// 淘宝分类名称
        /// </summary>
        public string TbCategoryName { get; set; }

        /// <summary>
        /// 可选的运输方式
        /// </summary>
        public string AvailableShipmentTypeIds { get; set; }

        /// <summary>
        /// 是否是支持Prime模式的商品
        /// </summary>
        public bool IsPrime { get; set; }

        /// <summary>
        /// 是否是支持Prime模式的商品
        /// </summary>
        public PrimeShippingTypes PrimeShippingType { get; set; }

        /// <summary>
        /// 估计运费的eta
        /// </summary>
        public string DomesticShippingEta { get; set; }

        /// <summary>
        /// 闪购商品价格
        /// </summary>
        public double FlashSalesPrice { get; set; }

        /// <summary>
        /// 闪购开始时间
        /// </summary>
        public DateTime? FlashSalesStartTime { get; set; }

        /// <summary>
        /// 闪购结束时间
        /// </summary>
        public DateTime? FlashSalesEndTime { get; set; }

        /// <summary>
        /// 单个用户可以购买商品数量
        /// </summary>
        public int FlashSalesLimitation { get; set; }

        /// <summary>
        /// 闪购商品的库存值
        /// </summary>
        public int FlashSalesStock { get; set; }

        /// <summary>
        /// 内部商品Url：比如返利商品短连接
        /// </summary>
        public string InternalProductUrl { get; set; }

        /// <summary>
        /// 商品原始价格
        /// </summary>
        public double OriginalUnitPrice { get; set; }

        /// <summary>
        /// 是否是满减商品
        /// </summary>
        public bool IsCashOff { get; set; }

        /// <summary>
        /// 商品分类Id
        /// </summary>
        public List<int> ProductCategoryIds { get; set; }

        /// <summary>
        /// 商品对应的满减专区Key
        /// </summary>
        public string CashOffKey { get; set; }

        public Dictionary<string, string> ProductProperties { get; set; }

        public Dictionary<string,string> ProductNames { get; set; }
     
        public DateTime? CashoffEnd { get; set; }
      
        public DateTime? CashoffStart { get; set; }

        public int ProductStock { get; set; }

        public long Gpid { get; set; }

        public string GetId()
        {
            return Utility.DataUtility.GetRefId(ProductUrl);
        }
    }
}