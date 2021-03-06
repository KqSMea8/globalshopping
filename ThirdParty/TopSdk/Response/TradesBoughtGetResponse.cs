using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// TradesBoughtGetResponse.
    /// </summary>
    public class TradesBoughtGetResponse : TopResponse
    {
        /// <summary>
        /// 搜索到的交易信息总数
        /// </summary>
        [XmlElement("total_results")]
        public long TotalResults { get; set; }

        /// <summary>
        /// 搜索到的交易信息列表，返回的Trade和Order中包含的具体信息为入参fields请求的字段信息
        /// </summary>
        [XmlArray("trades")]
        [XmlArrayItem("trade")]
        public List<Top.Api.Domain.Trade> Trades { get; set; }

    }
}
