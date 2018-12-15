using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.subuser.dutys.get
    /// </summary>
    public class SubuserDutysGetRequest : BaseTopRequest<Top.Api.Response.SubuserDutysGetResponse>
    {
        /// <summary>
        /// 主账号用户名
        /// </summary>
        public string UserNick { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.subuser.dutys.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("user_nick", this.UserNick);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("user_nick", this.UserNick);
        }

        #endregion
    }
}
