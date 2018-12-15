using System;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.openim.users.get
    /// </summary>
    public class OpenimUsersGetRequest : BaseTopRequest<OpenimUsersGetResponse>
    {
        /// <summary>
        /// uid列表
        /// </summary>
        public string Userids { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.openim.users.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("userids", this.Userids);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("userids", this.Userids);
            RequestValidator.ValidateMaxListSize("userids", this.Userids, 100);
        }

        #endregion
    }
}
