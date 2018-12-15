using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// DetailPrice Data Structure.
    /// </summary>
    [Serializable]
    public class DetailPrice : TopObject
    {
        /// <summary>
        /// 商品价格
        /// </summary>
        [XmlElement("price")]
        public Baichuan.Api.Domain.PriceUnit Price { get; set; }

        /// <summary>
        /// 商品促销价格
        /// </summary>
        [XmlElement("promotion_price")]
        public Baichuan.Api.Domain.PriceUnit PromotionPrice { get; set; }
    }
}
