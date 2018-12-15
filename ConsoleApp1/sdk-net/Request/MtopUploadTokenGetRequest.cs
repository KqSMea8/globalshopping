using System;
using System.Xml.Serialization;
using Top.Api.Domain;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.mtop.upload.token.get
    /// </summary>
    public class MtopUploadTokenGetRequest : BaseTopRequest<Top.Api.Response.MtopUploadTokenGetResponse>
    {
        /// <summary>
        /// 系统自动生成
        /// </summary>
        public string ParamUploadTokenRequest { get; set; }

        public UploadTokenRequestV ParamUploadTokenRequest_ { set { this.ParamUploadTokenRequest = TopUtils.ObjectToJson(value); } } 

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.mtop.upload.token.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("param_upload_token_request", this.ParamUploadTokenRequest);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
        }

        #endregion
    }
}
