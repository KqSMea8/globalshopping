using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.shoprecommend.shops.get
    /// </summary>
    public class ShoprecommendShopsGetRequest : BaseTopRequest<Top.Api.Response.ShoprecommendShopsGetResponse>
    {
        /// <summary>
        /// 请求个数，建议获取16个
        /// </summary>
        public Nullable<long> Count { get; set; }

        /// <summary>
        /// 额外参数
        /// </summary>
        public string Ext { get; set; }

        /// <summary>
        /// 请求类型，1：关联店铺推荐。其他值当非法值处理
        /// </summary>
        public Nullable<long> RecommendType { get; set; }

        /// <summary>
        /// 传入卖家ID
        /// </summary>
        public Nullable<long> SellerId { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.shoprecommend.shops.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("count", this.Count);
            parameters.Add("ext", this.Ext);
            parameters.Add("recommend_type", this.RecommendType);
            parameters.Add("seller_id", this.SellerId);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("count", this.Count);
            RequestValidator.ValidateRequired("recommend_type", this.RecommendType);
            RequestValidator.ValidateRequired("seller_id", this.SellerId);
        }

        #endregion
    }
}