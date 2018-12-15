using System;
using System.Text.RegularExpressions;

namespace GlobalShopping.Core.Snatch
{
    /// <summary>
    ///     抓取商品信息基类。
    /// </summary>
    public abstract class BaseSnatch : ISnatch
    {
        /// <summary>
        /// 初始化抓取商品信息
        /// </summary>
        /// <param name="domain">领域</param>
        /// <param name="site">站点</param>
        protected BaseSnatch(string domain, string site)
        {
            Site = site;
            Domain = domain;
        }
        public string Domain { get; private set; }

        public string Site { get; private set; }

        public virtual string[] VendorNamePatterns
        {
            get { return EasyBuyXmlUtility.GetVendorName(Site); }
        }

        public virtual string[] ProductNamePatterns
        {
            get { return EasyBuyXmlUtility.GetProductName(Site); }
        }

        public virtual string[] UnitPricePatterns
        {
            get { return EasyBuyXmlUtility.GetUnitPrice(Site); }
        }

        public virtual string[] ShipmentFeePatterns
        {
            get { return EasyBuyXmlUtility.GetShipmentFee(Site); }
        }

        public virtual string[] ProductImagePatterns
        {
            get { return EasyBuyXmlUtility.GetProductImage(Site); }
        }

        public virtual string GetVendorName(string pageContent)
        {
            foreach (string pattern in VendorNamePatterns)
            {
                var isMatch = Regex.IsMatch(pageContent, pattern);
                if (isMatch)
                {
                    var m = Regex.Match(pageContent, pattern);
                    var vendorName = m.Groups[1].ToString();

                    if (!string.IsNullOrEmpty(vendorName))
                    {
                        vendorName = Regex.Replace(DataUtility.RemoveHtml(vendorName.Trim()), @"&(.+);", "",
                            RegexOptions.IgnoreCase);
                    }

                    return vendorName;
                }
            }
            return string.Empty;
        }

        public virtual string GetProductName(string pageContent)
        {
            foreach (string pattern in ProductNamePatterns)
            {
                var isMatch = Regex.IsMatch(pageContent, pattern);
                if (isMatch)
                {
                    var m = Regex.Match(pageContent, pattern);
                    var productName = m.Groups[1].ToString();

                    if (!string.IsNullOrEmpty(productName))
                    {
                        productName = productName.Trim();
                    }

                    return productName.RemoveHTML().HtmlDecode();
                }
            }

            return string.Empty;
        }

        public virtual double? GetUnitPrice(string pageContent)
        {
            foreach (string pattern in UnitPricePatterns)
            {
                var isMatch = Regex.IsMatch(pageContent, pattern);
                if (isMatch)
                {
                    var m = Regex.Match(pageContent, pattern);
                    var unitPrice = m.Groups[1].ToString();
                    if (!string.IsNullOrEmpty(unitPrice))
                    {
                        unitPrice = unitPrice.Trim();
                        return Convert.ToDouble(unitPrice);
                    }
                }
            }

            return 0;
        }

        public virtual double? GetShipmentFee(string pageContent)
        {
            if (ShipmentFeePatterns != null)
            {
                foreach (string pattern in ShipmentFeePatterns)
                {
                    var isMatch = Regex.IsMatch(pageContent, pattern);
                    if (isMatch)
                    {
                        var m = Regex.Match(pageContent, pattern);
                        var shipmentFee = m.Groups[2].ToString().Trim();

                        if (!string.IsNullOrEmpty(shipmentFee))
                        {
                            shipmentFee = shipmentFee.Trim();
                            return Convert.ToDouble(shipmentFee);
                        }
                    }
                }
            }
            return 0;
        }

        public virtual string GetProductImage(string pageContent)
        {
            foreach (string pattern in ProductImagePatterns)
            {
                var isMatch = Regex.IsMatch(pageContent, pattern);

                if (isMatch)
                {
                    var m = Regex.Match(pageContent, pattern);
                    var productImage = m.Groups[1].ToString();
                    if (!string.IsNullOrEmpty(productImage))
                    {
                        productImage = GetUrl(productImage.Trim());
                    }

                    return productImage;
                }
            }
            return string.Empty;
        }

        public virtual OutlineProduct Snatch(string productUrl, string destination,bool needTranslate=false)
        {
            const string originCode = "CN";
            try
            {
                if (productUrl.IndexOf(Domain, StringComparison.Ordinal) == -1)
                {
                    return new OutlineProduct(originCode, productUrl);
                }

                var pageContent = DataUtility.GetPageContent(productUrl, null);

                if (pageContent == "")
                {
                    return new OutlineProduct(originCode, productUrl);
                }
                return new OutlineProduct(originCode, productUrl)
                {
                    VendorName = GetVendorName(pageContent),
                    ProductName = GetProductName(pageContent),
                    UnitPrice = GetUnitPrice(pageContent),
                    ShippingFee = GetShipmentFee(pageContent),
                    ProductImage = GetProductImage(pageContent)
                };
            }
            catch
            {
                return new OutlineProduct(originCode, productUrl)
                {
                    ProductUrl = productUrl
                };
            }
        }

        public virtual double? GetShipmentByWareHouse(string productUrl, string wareHouse)
        {
            if (productUrl.IndexOf(Domain, StringComparison.Ordinal) == -1)
            {
                return 0;
            }

            var pageContent = DataUtility.GetPageContent(productUrl, null);

            if (pageContent == "")
            {
                return 0;
            }
            return GetShipmentFee(pageContent);
        }

        public virtual string GetOriginCodeByUrl(string productUrl)
        {
            return EasyBuyXmlUtility.GetSiteArea(Site)[0];
        }

        public virtual string GetUrl(string source)
        {
            if (source == "")
            {
                return "";
            }
            string result;

            if (source.IndexOf("http://", StringComparison.Ordinal) >= 0
                || source.IndexOf("https://", StringComparison.Ordinal) >= 0)
            {
                result = source;
            }
            else
            {
                result = "http://www." + Domain + "/" + source;
            }

            return result;
        }

        /// <summary>
        /// 获取价格符号
        /// </summary>
        /// <returns></returns>
        protected string GetPriceSymbol()
        {
            var originCode = EasyBuyXmlUtility.GetSiteArea(Site)[0];
            if (originCode == "CN")
            {
                return "￥";
            }
            if (originCode == "TW")
            {
                return "NT$";
            }
            else
            {
                return "$";
            }
        }

        public static string ProductUrl { get; set; }
        public static string OriginCode { get; set; }
    }
}