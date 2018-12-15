using System;
using Baichuan.Api.Domain;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.open.sms.checkvercode
    /// </summary>
    public class OpenSmsCheckvercodeRequest : BaseTopRequest<OpenSmsCheckvercodeResponse>
    {
        /// <summary>
        /// 验证验证码
        /// </summary>
        public string CheckVerCodeRequest { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.open.sms.checkvercode";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("check_ver_code_request", this.CheckVerCodeRequest);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("check_ver_code_request", this.CheckVerCodeRequest);
        }

        #endregion
    }
}
