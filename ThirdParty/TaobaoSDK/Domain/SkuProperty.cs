using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// SkuProperty Data Structure.
    /// </summary>
    [Serializable]
    public class SkuProperty : TopObject
    {
        /// <summary>
        /// sku属性id
        /// </summary>
        [XmlElement("prop_id")]
        public string PropId { get; set; }

        /// <summary>
        /// sku属性名称
        /// </summary>
        [XmlElement("prop_name")]
        public string PropName { get; set; }

        /// <summary>
        /// SKU属性值
        /// </summary>
        [XmlArray("values")]
        [XmlArrayItem("sku_property_value")]
        public List<Baichuan.Api.Domain.SkuPropertyValue> Values { get; set; }
    }
}
