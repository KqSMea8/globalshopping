using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// ItemAddResponse.
    /// </summary>
    public class ItemAddResponse : TopResponse
    {
        /// <summary>
        /// 商品结构,仅有numIid和created返回
        /// </summary>
        [XmlElement("item")]
        public Baichuan.Api.Domain.Item Item { get; set; }
    }
}
