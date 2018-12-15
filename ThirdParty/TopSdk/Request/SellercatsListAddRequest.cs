using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.sellercats.list.add
    /// </summary>
    public class SellercatsListAddRequest : BaseTopRequest<Top.Api.Response.SellercatsListAddResponse>
    {
        /// <summary>
        /// 卖家自定义类目名称。不超过20个字符
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父类目编号，如果类目为店铺下的一级类目：值等于0，如果类目为子类目，调用获取taobao.sellercats.list.get父类目编号
        /// </summary>
        public Nullable<long> ParentCid { get; set; }

        /// <summary>
        /// 链接图片URL地址。(绝对地址，格式：http://host/image_path)
        /// </summary>
        public string PictUrl { get; set; }

        /// <summary>
        /// 该类目在页面上的排序位置,取值范围:大于零的整数
        /// </summary>
        public Nullable<long> SortOrder { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.sellercats.list.add";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("name", this.Name);
            parameters.Add("parent_cid", this.ParentCid);
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
            RequestValidator.ValidateRequired("name", this.Name);
        }

        #endregion
    }
}
