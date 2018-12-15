using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// PriceUnit Data Structure.
    /// </summary>
    [Serializable]
    public class PriceUnit : TopObject
    {
        /// <summary>
        /// 价格Label
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// 售卖价格,单位元
        /// </summary>
        [XmlElement("price")]
        public string Price { get; set; }

        /// <summary>
        /// price后面的tips
        /// </summary>
        [XmlElement("tips")]
        public string Tips { get; set; }
    }
}
