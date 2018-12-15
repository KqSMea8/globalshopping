using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// SkuPriceItem Data Structure.
    /// </summary>
    [Serializable]
    public class SkuPriceItem : TopObject
    {
        /// <summary>
        /// sku一口价
        /// </summary>
        [XmlElement("price")]
        public Baichuan.Api.Domain.PriceUnit Price { get; set; }

        /// <summary>
        /// sku促销价
        /// </summary>
        [XmlElement("promotion_price")]
        public Baichuan.Api.Domain.PriceUnit PromotionPrice { get; set; }

        /// <summary>
        /// skuId
        /// </summary>
        [XmlElement("sku_id")]
        public string SkuId { get; set; }
    }
}
