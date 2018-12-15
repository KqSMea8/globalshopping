using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// TaeItemsSelectResponse.
    /// </summary>
    public class TaeItemsSelectResponse : TopResponse
    {
        /// <summary>
        /// 是否存在下一页
        /// </summary>
        [XmlElement("has_next")]
        public bool HasNext { get; set; }

        /// <summary>
        /// 商品选品服务
        /// </summary>
        [XmlArray("items")]
        [XmlArrayItem("item_select")]
        public List<Baichuan.Api.Domain.ItemSelect> Items { get; set; }

        /// <summary>
        /// 当前返回的是第几页，可能>此次请求的pageNo  每次调用请求的pageNo为前一次调用返回的pageNo+1
        /// </summary>
        [XmlElement("page_no")]
        public long PageNo { get; set; }
    }
}
