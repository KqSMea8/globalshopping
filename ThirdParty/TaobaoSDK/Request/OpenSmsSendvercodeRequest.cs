using System;
using Baichuan.Api.Domain;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.open.sms.sendvercode
    /// </summary>
    public class OpenSmsSendvercodeRequest : BaseTopRequest<OpenSmsSendvercodeResponse>
    {
        /// <summary>
        /// 发送验证码请求
        /// </summary>
        public string SendVerCodeRequest { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.open.sms.sendvercode";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("send_ver_code_request", this.SendVerCodeRequest);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("send_ver_code_request", this.SendVerCodeRequest);
        }

        #endregion
    }
}
