using System;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.open.account.search
    /// </summary>
    public class OpenAccountSearchRequest : BaseTopRequest<OpenAccountSearchResponse>
    {
        /// <summary>
        /// solr查询
        /// </summary>
        public string Query { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.open.account.search";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("query", this.Query);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("query", this.Query);
        }

        #endregion
    }
}
