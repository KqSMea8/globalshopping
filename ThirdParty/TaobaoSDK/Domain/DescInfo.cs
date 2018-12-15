using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// DescInfo Data Structure.
    /// </summary>
    [Serializable]
    public class DescInfo : TopObject
    {
        /// <summary>
        /// 卖家发布的商品图文详情信息内容
        /// </summary>
        [XmlElement("content")]
        public string Content { get; set; }

        /// <summary>
        /// 卖家发布的商品图文详情来源平台
        /// </summary>
        [XmlElement("type")]
        public string Type { get; set; }
    }
}
