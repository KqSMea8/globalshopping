using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// Carriage Data Structure.
    /// </summary>
    [Serializable]
    public class Carriage : TopObject
    {
        /// <summary>
        /// 运费
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// 快递公司
        /// </summary>
        [XmlElement("price")]
        public string Price { get; set; }
    }
}
