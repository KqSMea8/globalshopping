using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// SellerInfo Data Structure.
    /// </summary>
    [Serializable]
    public class SellerInfo : TopObject
    {
        /// <summary>
        /// 卖家昵称
        /// </summary>
        [XmlElement("seller_nick")]
        public string SellerNick { get; set; }

        /// <summary>
        /// 卖家类型
        /// </summary>
        [XmlElement("seller_type")]
        public string SellerType { get; set; }

        /// <summary>
        /// 卖家店铺名称
        /// </summary>
        [XmlElement("shop_name")]
        public string ShopName { get; set; }
    }
}
