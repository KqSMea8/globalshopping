using System;
using Baichuan.Api.Domain;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.open.account.create
    /// </summary>
    public class OpenAccountCreateRequest : BaseTopRequest<OpenAccountCreateResponse>
    {
        /// <summary>
        /// Open Account的列表
        /// </summary>
        public string ParamList { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.open.account.create";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("param_list", this.ParamList);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateObjectMaxListSize("param_list", this.ParamList, 20);
        }

        #endregion
    }
}
