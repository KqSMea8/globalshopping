using System;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.top.auth.token.create
    /// </summary>
    public class TopAuthTokenCreateRequest : BaseTopRequest<TopAuthTokenCreateResponse>
    {
        /// <summary>
        /// 授权code，grantType==authorization_code 时需要
        /// </summary>
        public string Code { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.top.auth.token.create";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("code", this.Code);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("code", this.Code);
        }

        #endregion
    }
}
