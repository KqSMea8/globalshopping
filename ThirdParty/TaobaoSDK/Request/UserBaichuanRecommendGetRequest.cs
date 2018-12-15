using System;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.user.Baichuan.recommend.get
    /// </summary>
    public class UserBaichuanRecommendGetRequest : BaseTopRequest<UserBaichuanRecommendGetResponse>
    {
        /// <summary>
        /// idtype，目前支持1
        /// </summary>
        public Nullable<long> IdType { get; set; }

        /// <summary>
        /// isvid
        /// </summary>
        public string IsvAppId { get; set; }

        /// <summary>
        /// 混淆userid
        /// </summary>
        public string UserId { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.user.Baichuan.recommend.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("id_type", this.IdType);
            parameters.Add("isv_app_id", this.IsvAppId);
            parameters.Add("user_id", this.UserId);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("id_type", this.IdType);
            RequestValidator.ValidateRequired("isv_app_id", this.IsvAppId);
            RequestValidator.ValidateRequired("user_id", this.UserId);
        }

        #endregion
    }
}
