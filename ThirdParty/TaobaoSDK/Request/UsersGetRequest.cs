using System;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.users.get
    /// </summary>
    public class UsersGetRequest : BaseTopRequest<UsersGetResponse>
    {
        /// <summary>
        /// 查询字段：User数据结构的公开信息字段列表，以半角逗号(,)分隔
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// 用户昵称，多个以半角逗号(,)分隔，最多40个
        /// </summary>
        public string Nicks { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.users.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("fields", this.Fields);
            parameters.Add("nicks", this.Nicks);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("fields", this.Fields);
            RequestValidator.ValidateRequired("nicks", this.Nicks);
        }

        #endregion
    }
}
