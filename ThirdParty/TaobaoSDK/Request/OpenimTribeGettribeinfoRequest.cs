using System;
using Baichuan.Api.Domain;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.openim.tribe.gettribeinfo
    /// </summary>
    public class OpenimTribeGettribeinfoRequest : BaseTopRequest<OpenimTribeGettribeinfoResponse>
    {
        /// <summary>
        /// 群ID
        /// </summary>
        public Nullable<long> TribeId { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public string User { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.openim.tribe.gettribeinfo";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("tribe_id", this.TribeId);
            parameters.Add("user", this.User);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("tribe_id", this.TribeId);
            RequestValidator.ValidateRequired("user", this.User);
        }

        #endregion
    }
}
