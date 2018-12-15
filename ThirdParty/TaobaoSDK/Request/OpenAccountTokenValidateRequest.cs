using System;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.open.account.token.validate
    /// </summary>
    public class OpenAccountTokenValidateRequest : BaseTopRequest<OpenAccountTokenValidateResponse>
    {
        /// <summary>
        /// token
        /// </summary>
        public string ParamToken { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.open.account.token.validate";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("param_token", this.ParamToken);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("param_token", this.ParamToken);
        }

        #endregion
    }
}
