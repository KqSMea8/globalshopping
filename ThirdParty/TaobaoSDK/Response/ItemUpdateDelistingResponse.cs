using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// ItemUpdateDelistingResponse.
    /// </summary>
    public class ItemUpdateDelistingResponse : TopResponse
    {
        /// <summary>
        /// 返回商品更新信息：返回的结果是:num_iid和modified
        /// </summary>
        [XmlElement("item")]
        public Baichuan.Api.Domain.Item Item { get; set; }
    }
}
