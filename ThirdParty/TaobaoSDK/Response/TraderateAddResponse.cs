using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// TraderateAddResponse.
    /// </summary>
    public class TraderateAddResponse : TopResponse
    {
        /// <summary>
        /// 返回tid、oid、create
        /// </summary>
        [XmlElement("trade_rate")]
        public Baichuan.Api.Domain.TradeRate TradeRate { get; set; }
    }
}
