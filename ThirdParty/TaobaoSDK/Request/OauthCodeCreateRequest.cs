using System;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.oauth.code.create
    /// </summary>
    public class OauthCodeCreateRequest : BaseTopRequest<OauthCodeCreateResponse>
    {
        /// <summary>
        /// mock param
        /// </summary>
        public Nullable<long> Test { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.oauth.code.create";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("test", this.Test);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
        }

        #endregion
    }
}
