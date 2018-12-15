using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// RateItem Data Structure.
    /// </summary>
    [Serializable]
    public class RateItem : TopObject
    {
        /// <summary>
        /// 评价内容
        /// </summary>
        [XmlElement("feedback")]
        public string Feedback { get; set; }

        /// <summary>
        /// 评价人nick
        /// </summary>
        [XmlElement("nick")]
        public string Nick { get; set; }
    }
}
