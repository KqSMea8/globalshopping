using System;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.top.auth.token.refresh
    /// </summary>
    public class TopAuthTokenRefreshRequest : BaseTopRequest<TopAuthTokenRefreshResponse>
    {
        /// <summary>
        /// grantType==refresh_token 时需要
        /// </summary>
        public string RefreshToken { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.top.auth.token.refresh";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("refresh_token", this.RefreshToken);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("refresh_token", this.RefreshToken);
        }

        #endregion
    }
}
