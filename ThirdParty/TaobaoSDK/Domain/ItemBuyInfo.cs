using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// ItemBuyInfo Data Structure.
    /// </summary>
    [Serializable]
    public class ItemBuyInfo : TopObject
    {
        /// <summary>
        /// 是否支持购物车
        /// </summary>
        [XmlElement("cart_support")]
        public string CartSupport { get; set; }
    }
}
