using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// ItemUpdateListingResponse.
    /// </summary>
    public class ItemUpdateListingResponse : TopResponse
    {
        /// <summary>
        /// 上架后返回的商品信息：返回的结果就是:num_iid和modified
        /// </summary>
        [XmlElement("item")]
        public Baichuan.Api.Domain.Item Item { get; set; }
    }
}
