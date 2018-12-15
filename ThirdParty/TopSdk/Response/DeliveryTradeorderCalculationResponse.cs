using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// DeliveryTradeorderCalculationResponse.
    /// </summary>
    public class DeliveryTradeorderCalculationResponse : TopResponse
    {
        /// <summary>
        /// 订单费用信息，主要是每笔订单左右支持的运送方式的费用  <br/>  <font color = red>  当返回多个订单费用时，可以根据传入的订单唯一标识区分每笔订单额物流费用  </font>
        /// </summary>
        [XmlArray("trade_order_fees")]
        [XmlArrayItem("trade_order_fee")]
        public List<Top.Api.Domain.TradeOrderFee> TradeOrderFees { get; set; }

    }
}
