using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// SkuSelectInfo Data Structure.
    /// </summary>
    [Serializable]
    public class SkuSelectInfo : TopObject
    {
        /// <summary>
        /// 外部ID
        /// </summary>
        [XmlElement("outer_id")]
        public string OuterId { get; set; }

        /// <summary>
        /// sku id
        /// </summary>
        [XmlElement("sku_id")]
        public long SkuId { get; set; }
    }
}
