using System;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.open.account.delete
    /// </summary>
    public class OpenAccountDeleteRequest : BaseTopRequest<OpenAccountDeleteResponse>
    {
        /// <summary>
        /// ISV自己账号的id列表
        /// </summary>
        public string IsvAccountIds { get; set; }

        /// <summary>
        /// Open Account的id列表
        /// </summary>
        public string OpenAccountIds { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.open.account.delete";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("isv_account_ids", this.IsvAccountIds);
            parameters.Add("open_account_ids", this.OpenAccountIds);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateMaxListSize("isv_account_ids", this.IsvAccountIds, 20);
            RequestValidator.ValidateMaxListSize("open_account_ids", this.OpenAccountIds, 20);
        }

        #endregion
    }
}
