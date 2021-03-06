using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.skus.custom.get
    /// </summary>
    public class SkusCustomGetRequest : BaseTopRequest<Top.Api.Response.SkusCustomGetResponse>
    {
        /// <summary>
        /// 需返回的字段列表。可选值：Sku结构体中的所有字段；字段之间用“,”隔开
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// Sku的外部商家ID
        /// </summary>
        public string OuterId { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.skus.custom.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("fields", this.Fields);
            parameters.Add("outer_id", this.OuterId);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("fields", this.Fields);
            RequestValidator.ValidateRequired("outer_id", this.OuterId);
        }

        #endregion
    }
}
