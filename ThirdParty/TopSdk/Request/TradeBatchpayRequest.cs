using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.trade.batchpay
    /// </summary>
    public class TradeBatchpayRequest : BaseTopRequest<Top.Api.Response.TradeBatchpayResponse>
    {
        /// <summary>
        /// 合并付款的订单号序列，订单状态为等待买家付款状态，订单号以'θ'号进行间隔，最多支持15笔订单同时付款。如果是单笔订单，则只是一个订单号
        /// </summary>
        public string Tids { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.trade.batchpay";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("tids", this.Tids);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("tids", this.Tids);
        }

        #endregion
    }
}
