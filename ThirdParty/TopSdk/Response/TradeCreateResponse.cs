using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// TradeCreateResponse.
    /// </summary>
    public class TradeCreateResponse : TopResponse
    {
        /// <summary>
        /// 创建交易返回值，tid订单编号，alipay_no支付订单号，alipay_url支付连接
        /// </summary>
        [XmlElement("order_result")]
        public Top.Api.Domain.OrderCreateResult OrderResult { get; set; }

    }
}
