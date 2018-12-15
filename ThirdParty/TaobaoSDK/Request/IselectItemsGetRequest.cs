using System;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.iselect.items.get
    /// </summary>
    public class IselectItemsGetRequest : BaseTopRequest<IselectItemsGetResponse>
    {
        /// <summary>
        /// 页序，第一页从0开始
        /// </summary>
        public Nullable<long> Page { get; set; }

        /// <summary>
        /// 每页数量，取值1～100
        /// </summary>
        public Nullable<long> PageSize { get; set; }

        /// <summary>
        /// 选品标签id列表，最多5个
        /// </summary>
        public string TagIds { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.iselect.items.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("page", this.Page);
            parameters.Add("page_size", this.PageSize);
            parameters.Add("tag_ids", this.TagIds);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("page", this.Page);
            RequestValidator.ValidateRequired("page_size", this.PageSize);
            RequestValidator.ValidateMaxValue("page_size", this.PageSize, 100);
            RequestValidator.ValidateMinValue("page_size", this.PageSize, 1);
            RequestValidator.ValidateRequired("tag_ids", this.TagIds);
            RequestValidator.ValidateMaxListSize("tag_ids", this.TagIds, 5);
        }

        #endregion
    }
}
