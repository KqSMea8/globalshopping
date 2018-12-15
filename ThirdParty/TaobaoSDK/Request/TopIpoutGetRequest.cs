using System;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.top.ipout.get
    /// </summary>
    public class TopIpoutGetRequest : BaseTopRequest<TopIpoutGetResponse>
    {
        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.top.ipout.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
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
