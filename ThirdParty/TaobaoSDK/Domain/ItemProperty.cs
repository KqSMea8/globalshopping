using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// ItemProperty Data Structure.
    /// </summary>
    [Serializable]
    public class ItemProperty : TopObject
    {
        /// <summary>
        /// 商品属性名
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// 商值属性值
        /// </summary>
        [XmlElement("value")]
        public string Value { get; set; }
    }
}
