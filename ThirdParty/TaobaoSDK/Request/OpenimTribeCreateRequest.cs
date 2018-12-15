using System;
using Baichuan.Api.Domain;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.openim.tribe.create
    /// </summary>
    public class OpenimTribeCreateRequest : BaseTopRequest<OpenimTribeCreateResponse>
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public string Members { get; set; }

        /// <summary>
        /// 群公告
        /// </summary>
        public string Notice { get; set; }

        /// <summary>
        /// 群名称
        /// </summary>
        public string TribeName { get; set; }

        /// <summary>
        /// 群类型
        /// </summary>
        public Nullable<long> TribeType { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public string User { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.openim.tribe.create";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("members", this.Members);
            parameters.Add("notice", this.Notice);
            parameters.Add("tribe_name", this.TribeName);
            parameters.Add("tribe_type", this.TribeType);
            parameters.Add("user", this.User);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateObjectMaxListSize("members", this.Members, 20);
            RequestValidator.ValidateRequired("notice", this.Notice);
            RequestValidator.ValidateRequired("tribe_name", this.TribeName);
            RequestValidator.ValidateRequired("tribe_type", this.TribeType);
            RequestValidator.ValidateRequired("user", this.User);
        }

        #endregion
    }
}
