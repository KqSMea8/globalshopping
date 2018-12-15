using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// ItemUpdateResponse.
    /// </summary>
    public class ItemUpdateResponse : TopResponse
    {
        /// <summary>
        /// 商品结构里的num_iid，modified
        /// </summary>
        [XmlElement("item")]
        public Baichuan.Api.Domain.Item Item { get; set; }
    }
}
