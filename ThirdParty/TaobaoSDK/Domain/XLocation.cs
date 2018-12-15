using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// XLocation Data Structure.
    /// </summary>
    [Serializable]
    public class XLocation : TopObject
    {
        /// <summary>
        /// 商品所在市
        /// </summary>
        [XmlElement("city")]
        public string City { get; set; }

        /// <summary>
        /// 商品所在省
        /// </summary>
        [XmlElement("state")]
        public string State { get; set; }
    }
}
