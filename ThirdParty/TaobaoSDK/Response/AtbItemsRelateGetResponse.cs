using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// AtbItemsRelateGetResponse.
    /// </summary>
    public class AtbItemsRelateGetResponse : TopResponse
    {
        /// <summary>
        /// 爱淘宝商品列表
        /// </summary>
        [XmlArray("items")]
        [XmlArrayItem("aitaobao_item")]
        public List<Baichuan.Api.Domain.AitaobaoItem> Items { get; set; }

        /// <summary>
        /// 搜索到符合条件的结果总数
        /// </summary>
        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}
