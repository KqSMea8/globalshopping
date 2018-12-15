using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.baichuan.payresult.query
    /// </summary>
    public class BaichuanPayresultQueryRequest : BaseTopRequest<Top.Api.Response.BaichuanPayresultQueryResponse>
    {
        /// <summary>
        /// name
        /// </summary>
        public string Name { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.baichuan.payresult.query";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("name", this.Name);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
        }

        #endregion
    }
}
