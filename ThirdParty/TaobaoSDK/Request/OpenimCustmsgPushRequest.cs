using System;
using Baichuan.Api.Domain;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.openim.custmsg.push
    /// </summary>
    public class OpenimCustmsgPushRequest : BaseTopRequest<OpenimCustmsgPushResponse>
    {
        /// <summary>
        /// 自定义消息内容
        /// </summary>
        public string Custmsg { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.openim.custmsg.push";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("custmsg", this.Custmsg);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("custmsg", this.Custmsg);
        }

        #endregion
    }
}
