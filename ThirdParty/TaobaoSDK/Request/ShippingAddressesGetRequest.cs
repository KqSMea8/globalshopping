using System;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.shipping.addresses.get
    /// </summary>
    public class ShippingAddressesGetRequest : BaseTopRequest<ShippingAddressesGetResponse>
    {
        /// <summary>
        /// 需返回的字段列表，目前支持有：address_id, receiver_name, phone, mobile, is_default, location.address, location.zip, location.city, location.district,location.state
        /// </summary>
        public string Fields { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.shipping.addresses.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("fields", this.Fields);
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
