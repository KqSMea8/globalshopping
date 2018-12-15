using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: alibaba.baichuan.appevent.upload
    /// </summary>
    public class AlibabaBaichuanAppeventUploadRequest : BaseTopRequest<Top.Api.Response.AlibabaBaichuanAppeventUploadResponse>
    {
        /// <summary>
        /// 标识app
        /// </summary>
        public string Appid { get; set; }

        /// <summary>
        /// 标识场景
        /// </summary>
        public string Bizid { get; set; }

        /// <summary>
        /// 标识具体行为
        /// </summary>
        public string Params { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "alibaba.baichuan.appevent.upload";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("appid", this.Appid);
            parameters.Add("bizid", this.Bizid);
            parameters.Add("params", this.Params);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("appid", this.Appid);
            RequestValidator.ValidateRequired("bizid", this.Bizid);
            RequestValidator.ValidateRequired("params", this.Params);
        }

        #endregion
    }
}
