using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.sellercats.list.update
    /// </summary>
    public class SellercatsListUpdateRequest : BaseTopRequest<Top.Api.Response.SellercatsListUpdateResponse>
    {
        /// <summary>
        /// 卖家自定义类目编号
        /// </summary>
        public Nullable<long> Cid { get; set; }

        /// <summary>
        /// 卖家自定义类目名称。不超过20个字符
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 链接图片URL地址
        /// </summary>
        public string PictUrl { get; set; }

        /// <summary>
        /// 该类目在页面上的排序位置,取值范围:大于零的整数
        /// </summary>
        public Nullable<long> SortOrder { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.sellercats.list.update";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("cid", this.Cid);
            parameters.Add("name", this.Name);
            parameters.Add("pict_url", this.PictUrl);
            parameters.Add("sort_order", this.SortOrder);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("cid", this.Cid);
        }

        #endregion
    }
}
