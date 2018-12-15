using System;
using System.Configuration;
using System.Xml;
using GlobalShopping.Core.Utility;

namespace GlobalShopping.Core.Utility
{
    public class EasyBuyXmlUtility
    {
        private static XmlDocument Xml
        {
            get
            {
                var fileString =
                     AppDomain.CurrentDomain.BaseDirectory +
                     ConfigurationManager.AppSettings["EasyBusy"];
                var xml = new XmlDocument();
                xml.Load(fileString);
                return xml;
            }
        }

        public static XmlNode GetSiteNode(string siteName)
        {
            var xmlList = Xml.SelectNodes("/sites/site[@name='" + siteName + "']");
            return xmlList != null && xmlList.Count > 0 ? xmlList[0] : null;
        }

        /// <summary>
        ///     获取商家名称
        /// </summary>
        /// <param name="siteName"></param>
        /// <returns></returns>
        public static string[] GetVendorName(string siteName)
        {
            var xmlNode = GetSiteNode(siteName);
            var returnValue = string.Empty;
            var selectNodes = xmlNode.SelectNodes("vendorname");
            if (selectNodes != null)
                foreach (XmlNode node in selectNodes)
                {
                    returnValue += (node.InnerText + ",./");
                }
            return returnValue.Split(new[] { ",./" }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        ///     获取运费
        /// </summary>
        /// <param name="siteName"></param>
        /// <returns></returns>
        public static string[] GetShipmentFee(string siteName)
        {
            var xmlNode = GetSiteNode(siteName);
            var returnValue = string.Empty;
            if (xmlNode != null)
            {
                foreach (XmlNode node in xmlNode.SelectNodes("shipmentfee"))
                {
                    returnValue += (node.InnerText + ",./");
                }
            }
            return returnValue.Split(new[] { ",./" }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        ///     获取商品名称
        /// </summary>
        /// <param name="siteName"></param>
        /// <returns></returns>
        public static string[] GetProductName(string siteName)
        {
            var xmlNode = GetSiteNode(siteName);
            var returnValue = string.Empty;
            foreach (XmlNode node in xmlNode.SelectNodes("productname"))
            {
                returnValue += (node.InnerText + ",./");
            }
            return returnValue.Split(new[] { ",./" }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        ///     获取商品价格
        /// </summary>
        /// <param name="siteName"></param>
        /// <returns></returns>
        public static string[] GetUnitPrice(string siteName)
        {
            var xmlNode = GetSiteNode(siteName);
            var returnValue = string.Empty;
            foreach (XmlNode node in xmlNode.SelectNodes("unitprice"))
            {
                returnValue += (node.InnerText + ",./");
            }
            return returnValue.Split(new[] { ",./" }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        ///     获取商品图片
        /// </summary>
        /// <param name="siteName"></param>
        /// <returns></returns>
        public static string[] GetProductImage(string siteName)
        {
            var xmlNode = GetSiteNode(siteName);
            var returnValue = string.Empty;
            foreach (XmlNode node in xmlNode.SelectNodes("productimage"))
            {
                returnValue += (node.InnerText + ",./");
            }
            return returnValue.Split(new[] { ",./" }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        ///     获取商品图片
        /// </summary>
        /// <param name="siteName"></param>
        /// <returns></returns>
        public static string[] GetSiteArea(string siteName)
        {
            var xmlNode = GetSiteNode(siteName);
            var returnValue = string.Empty;
            foreach (XmlNode node in xmlNode.SelectNodes("area"))
            {
                returnValue += (node.InnerText + ",./");
            }
            return returnValue.Split(new[] { ",./" }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        ///     获取淘宝商品ID
        /// </summary>
        /// <param name="siteName"></param>
        /// <returns></returns>
        public static string[] GetTaobaoProductID(string siteName)
        {
            var xmlNode = GetSiteNode(siteName);
            var returnValue = string.Empty;
            foreach (XmlNode node in xmlNode.SelectNodes("iid"))
            {
                returnValue += (node.InnerText + ",./");
            }
            return returnValue.Split(new[] { ",./" }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        ///     获取淘宝商品套餐ID
        /// </summary>
        /// <param name="siteName"></param>
        /// <returns></returns>
        public static string[] GetTaobaoMealID(string siteName)
        {
            var xmlNode = GetSiteNode(siteName);
            var returnValue = string.Empty;
            foreach (XmlNode node in xmlNode.SelectNodes("mealid"))
            {
                returnValue += (node.InnerText + ",./");
            }
            return returnValue.Split(new[] { ",./" }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        ///     获取淘宝商品套餐名称
        /// </summary>
        /// <param name="siteName"></param>
        /// <returns></returns>
        public static string[] GetTaobaoMealName(string siteName)
        {
            var xmlNode = GetSiteNode(siteName);
            var returnValue = string.Empty;
            foreach (XmlNode node in xmlNode.SelectNodes("mealname"))
            {
                returnValue += (node.InnerText + ",./");
            }
            return returnValue.Split(new[] { ",./" }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        ///     获取淘宝商品套餐价格
        /// </summary>
        /// <param name="siteName"></param>
        /// <returns></returns>
        public static string[] GetTaobaoMealPrice(string siteName)
        {
            var xmlNode = GetSiteNode(siteName);
            var returnValue = string.Empty;
            foreach (XmlNode node in xmlNode.SelectNodes("mealprice"))
            {
                returnValue += (node.InnerText + ",./");
            }
            return returnValue.Split(new[] { ",./" }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        ///     获取淘宝商品套餐图片
        /// </summary>
        /// <param name="siteName"></param>
        /// <returns></returns>
        public static string[] GetTaobaoMealImage(string siteName)
        {
            var xmlNode = GetSiteNode(siteName);
            var returnValue = string.Empty;
            foreach (XmlNode node in xmlNode.SelectNodes("mealimage"))
            {
                returnValue += (node.InnerText + ",./");
            }
            return returnValue.Split(new[] { ",./" }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        ///     获取淘宝卖家姓名
        /// </summary>
        /// <param name="siteName"></param>
        /// <returns></returns>
        public static string[] GetTaobaoSellerNick(string siteName)
        {
            var xmlNode = GetSiteNode(siteName);
            var returnValue = string.Empty;
            foreach (XmlNode node in xmlNode.SelectNodes("sellernick"))
            {
                returnValue += (node.InnerText + ",./");
            }
            return returnValue.Split(new[] { ",./" }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        ///     获取店铺信息
        /// </summary>
        /// <param name="siteName"></param>
        /// <returns></returns>
        public static string[] GetTaobaoAccountCount(string siteName)
        {
            var xmlNode = GetSiteNode(siteName);
            var returnValue = string.Empty;
            foreach (XmlNode node in xmlNode.SelectNodes("accountcount"))
            {
                returnValue += (node.InnerText + ",./");
            }
            return returnValue.Split(new[] { ",./" }, StringSplitOptions.RemoveEmptyEntries);
        }


        public static EnumData.FetchBy GetFetchBy(string siteName)
        {
            var xmlNode = GetSiteNode(siteName);
            var returnValue = string.Empty;
            foreach (XmlNode node in xmlNode.SelectNodes("fetchby"))
            {
                if (bool.Parse(node.Attributes["selected"].ToString()))
                {
                    return (EnumData.FetchBy)Enum.Parse(typeof(EnumData.FetchBy), node.Name);
                }
            }

            return EnumData.FetchBy.API;
        }
    }
}