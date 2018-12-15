using System;
using Baichuan.Api.Domain;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.openim.tribe.setmanager
    /// </summary>
    public class OpenimTribeSetmanagerRequest : BaseTopRequest<OpenimTribeSetmanagerResponse>
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public string Member { get; set; }

        /// <summary>
        /// 群id
        /// </summary>
        public Nullable<long> Tid { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public string User { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.openim.tribe.setmanager";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("member", this.Member);
            parameters.Add("tid", this.Tid);
            parameters.Add("user", this.User);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("member", this.Member);
            RequestValidator.ValidateRequired("tid", this.Tid);
            RequestValidator.ValidateRequired("user", this.User);
        }

        #endregion
    }
}
