using System;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.tae.exist.shop.coupon.get
    /// </summary>
    public class TaeExistShopCouponGetRequest : BaseTopRequest<TaeExistShopCouponGetResponse>
    {
        /// <summary>
        /// 卖家昵称
        /// </summary>
        public string SellerNick { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.tae.exist.shop.coupon.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("seller_nick", this.SellerNick);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("seller_nick", this.SellerNick);
        }

        #endregion
    }
}
