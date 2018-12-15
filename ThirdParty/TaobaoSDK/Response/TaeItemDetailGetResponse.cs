using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// TaeItemDetailGetResponse.
    /// </summary>
    public class TaeItemDetailGetResponse : TopResponse
    {
        /// <summary>
        /// 详情业务模块数据
        /// </summary>
        [XmlElement("data")]
        public Baichuan.Api.Domain.ItemDetailData Data { get; set; }
    }
}
