using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: alibaba.baichuan.appcontent.upload
    /// </summary>
    public class AlibabaBaichuanAppcontentUploadRequest : BaseTopRequest<Top.Api.Response.AlibabaBaichuanAppcontentUploadResponse>
    {
        /// <summary>
        /// app标识
        /// </summary>
        public string Appid { get; set; }

        /// <summary>
        /// 业务场景标识
        /// </summary>
        public string Bizid { get; set; }

        /// <summary>
        /// 具体操作
        /// </summary>
        public string Operate { get; set; }

        /// <summary>
        /// 入参
        /// </summary>
        public string Params { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "alibaba.baichuan.appcontent.upload";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("appid", this.Appid);
            parameters.Add("bizid", this.Bizid);
            parameters.Add("operate", this.Operate);
            parameters.Add("params", this.Params);
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
