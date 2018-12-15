using System;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.tae.items.select
    /// </summary>
    public class TaeItemsSelectRequest : BaseTopRequest<TaeItemsSelectResponse>
    {
        /// <summary>
        /// 淘宝类目id
        /// </summary>
        public string Cid { get; set; }

        /// <summary>
        /// 结束价格，单位：元
        /// </summary>
        public string EndPrice { get; set; }

        /// <summary>
        /// 返回字段列表
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// 商品更新时间
        /// </summary>
        public Nullable<long> ModifiedTime { get; set; }

        /// <summary>
        /// 页码，传入大小以返回pageNo的下一页为准
        /// </summary>
        public Nullable<long> PageNo { get; set; }

        /// <summary>
        /// 每页大小 小于200
        /// </summary>
        public Nullable<long> PageSize { get; set; }

        /// <summary>
        /// 店铺类目id
        /// </summary>
        public string SellerCids { get; set; }

        /// <summary>
        /// 卖家昵称，这个参数必须要传入才能获取卖家的商品数据。
        /// </summary>
        public string SellerNick { get; set; }

        /// <summary>
        /// 开始价格，单位：元
        /// </summary>
        public string StartPrice { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.tae.items.select";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("cid", this.Cid);
            parameters.Add("end_price", this.EndPrice);
            parameters.Add("fields", this.Fields);
            parameters.Add("modified_time", this.ModifiedTime);
            parameters.Add("page_no", this.PageNo);
            parameters.Add("page_size", this.PageSize);
            parameters.Add("seller_cids", this.SellerCids);
            parameters.Add("seller_nick", this.SellerNick);
            parameters.Add("start_price", this.StartPrice);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
        }

        #endregion
    }
}
