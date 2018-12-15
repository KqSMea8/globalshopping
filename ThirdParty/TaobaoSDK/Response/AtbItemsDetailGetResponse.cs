using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// AtbItemsDetailGetResponse.
    /// </summary>
    public class AtbItemsDetailGetResponse : TopResponse
    {
        /// <summary>
        /// 爱淘宝商品详情列表
        /// </summary>
        [XmlArray("atb_item_details")]
        [XmlArrayItem("aitaobao_item_detail")]
        public List<Baichuan.Api.Domain.AitaobaoItemDetail> AtbItemDetails { get; set; }

        /// <summary>
        /// 搜索到符合条件的结果总数
        /// </summary>
        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}
