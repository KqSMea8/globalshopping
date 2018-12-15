using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: alibaba.baichuan.app.recommend
    /// </summary>
    public class AlibabaBaichuanAppRecommendRequest : BaseTopRequest<Top.Api.Response.AlibabaBaichuanAppRecommendResponse>
    {
        /// <summary>
        /// app标识
        /// </summary>
        public string Appid { get; set; }

        /// <summary>
        /// 场景标识
        /// </summary>
        public string Bizid { get; set; }

        /// <summary>
        /// 业务参数
        /// </summary>
        public string Params { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "alibaba.baichuan.app.recommend";
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
