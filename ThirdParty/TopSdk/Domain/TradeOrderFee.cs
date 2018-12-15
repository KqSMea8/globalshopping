using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Domain
{
    /// <summary>
    /// TradeOrderFee Data Structure.
    /// </summary>
    [Serializable]
    public class TradeOrderFee : TopObject
    {
        /// <summary>
        /// 订单支持的所有运送方式的费用。  <br/><br/>  <font color = red>  一笔订单的商品都是包邮的情况下fee_list为空  </font>  <br/><br/>  <font color = blue>  当一笔订单没有需要的计算运送方式时（比如，你把商品支持的运送方式都设置成忽略方式了）整个tradeOrderfee对象返回空  </font>
        /// </summary>
        [XmlArray("fee_list")]
        [XmlArrayItem("fee_desc")]
        public List<Top.Api.Domain.FeeDesc> FeeList { get; set; }

        /// <summary>
        /// 该笔订单是否是卖家包邮  <br/>  是：true、否：false
        /// </summary>
        [XmlElement("need_seller_pay")]
        public bool NeedSellerPay { get; set; }

        /// <summary>
        /// 订单的唯一标识。  <br/>  <font color = red>  可以是真实的订单号，也可以是用户填入的方便区分订单的标识  </font>
        /// </summary>
        [XmlElement("order_id")]
        public string OrderId { get; set; }
    }
}
