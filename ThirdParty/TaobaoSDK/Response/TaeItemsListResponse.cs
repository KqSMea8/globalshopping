using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// TaeItemsListResponse.
    /// </summary>
    public class TaeItemsListResponse : TopResponse
    {
        /// <summary>
        /// 商品数据
        /// </summary>
        [XmlArray("items")]
        [XmlArrayItem("x_item")]
        public List<Baichuan.Api.Domain.XItem> Items { get; set; }
    }
}
