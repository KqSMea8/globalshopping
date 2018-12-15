using System;
using Baichuan.Api.Domain;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.openim.users.update
    /// </summary>
    public class OpenimUsersUpdateRequest : BaseTopRequest<OpenimUsersUpdateResponse>
    {
        /// <summary>
        /// 用户信息列表
        /// </summary>
        public string Userinfos { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.openim.users.update";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("userinfos", this.Userinfos);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateObjectMaxListSize("userinfos", this.Userinfos, 100);
        }

        #endregion
    }
}
