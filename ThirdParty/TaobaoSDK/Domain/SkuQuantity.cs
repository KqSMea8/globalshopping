using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// SkuQuantity Data Structure.
    /// </summary>
    [Serializable]
    public class SkuQuantity : TopObject
    {
        /// <summary>
        /// 库存数
        /// </summary>
        [XmlElement("quantity")]
        public string Quantity { get; set; }

        /// <summary>
        /// sku id
        /// </summary>
        [XmlElement("sku_id")]
        public string SkuId { get; set; }
    }
}
