using System;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.tae.items.list
    /// </summary>
    public class TaeItemsListRequest : BaseTopRequest<TaeItemsListResponse>
    {
        /// <summary>
        /// 返回值中需要的字段. 可选值 title,nick,pic_url,location,cid,price,post_fee,promoted_service,ju,shop_name字段间用 (,) 逗号分隔
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// 商品ID，英文逗号(,)分隔，最多50个。优先级低于open_iid，open_iids存在的话，则忽略本参数
        /// </summary>
        public string NumIids { get; set; }

        /// <summary>
        /// 商品混淆ID，英文逗号(,)分隔，最多50个。优先级高于open_iid，本参数存在的话，则忽略num_iids
        /// </summary>
        public string OpenIids { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.tae.items.list";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("fields", this.Fields);
            parameters.Add("num_iids", this.NumIids);
            parameters.Add("open_iids", this.OpenIids);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("fields", this.Fields);
        }

        #endregion
    }
}
