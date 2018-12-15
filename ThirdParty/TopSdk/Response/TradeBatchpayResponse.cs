using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// TradeBatchpayResponse.
    /// </summary>
    public class TradeBatchpayResponse : TopResponse
    {
        /// <summary>
        /// 返回支付宝订单列表alipayNos  合并付款总金额totalFee : 80.00  支付连接：alipayUrl : http://cashier.alipay.net/standard/payment/cashier.htm?orderId=539f230b2a958703310f001dfea2bc98&outBizNo=P2011083100055102&bizIdentity=batch10001(连接页面为淘宝主站上下单确认购买跳转的支付页面，第三方店铺需要支付宝登陆情况下才可以直接跳转）
        /// </summary>
        [XmlElement("order_result")]
        public Top.Api.Domain.OrderCreateResult OrderResult { get; set; }

    }
}
