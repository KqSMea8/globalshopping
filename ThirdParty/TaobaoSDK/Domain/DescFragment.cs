using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// DescFragment Data Structure.
    /// </summary>
    [Serializable]
    public class DescFragment : TopObject
    {
        /// <summary>
        /// 内容体目前支持支持图片url
        /// </summary>
        [XmlElement("content")]
        public string Content { get; set; }

        /// <summary>
        /// 内容label，目前支持支持图片txt,img
        /// </summary>
        [XmlElement("label")]
        public string Label { get; set; }
    }
}
