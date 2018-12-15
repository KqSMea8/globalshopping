using System;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.tae.item.detail.get
    /// </summary>
    public class TaeItemDetailGetRequest : BaseTopRequest<TaeItemDetailGetResponse>
    {
        /// <summary>
        /// 用户所在位置ip
        /// </summary>
        public string BuyerIp { get; set; }

        /// <summary>
        /// 区块信息
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// 商品open_iid
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 商品open_iid
        /// </summary>
        public string OpenIid { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.tae.item.detail.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("buyer_ip", this.BuyerIp);
            parameters.Add("fields", this.Fields);
            parameters.Add("id", this.Id);
            parameters.Add("open_iid", this.OpenIid);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("fields", this.Fields);
        }

        #endregion
    }
}
