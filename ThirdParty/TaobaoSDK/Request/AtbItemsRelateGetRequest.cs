using System;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.atb.items.relate.get
    /// </summary>
    public class AtbItemsRelateGetRequest : BaseTopRequest<AtbItemsRelateGetResponse>
    {
        /// <summary>
        /// 分类id.推荐类型为5时cid不能为空。仅支持叶子类目ID，即通过taobao.itemcats.get获取到is_parent=false的cid。
        /// </summary>
        public Nullable<long> Cid { get; set; }

        /// <summary>
        /// 需返回的字段列表.可选值:open_iid,title,nick,pic_url,price,click_url,commission,ommission_rate,commission_num,commission_volume,shop_click_url,seller_credit_score,item_location,volume;字段之间用","分隔
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// 指定返回结果的最大条数.实际返回结果个数根据算法来确定,所以该值会小于或者等于该值
        /// </summary>
        public Nullable<long> MaxCount { get; set; }

        /// <summary>
        /// open_iid
        /// </summary>
        public string OpenIid { get; set; }

        /// <summary>
        /// 推荐类型.  1:同类商品推荐;此时必须得输入num_iid  2:异类商品推荐;此时必须得输入num_iid  3:同店商品推荐;此时必须得输入num_iid  4:店铺热门推荐;此时必须得输入seller_id  5:类目热门推荐;此时必须得输入cid
        /// </summary>
        public Nullable<long> RelateType { get; set; }

        /// <summary>
        /// 卖家的用户id.注：推荐类型为4时seller_id不能为空
        /// </summary>
        public Nullable<long> SellerId { get; set; }

        /// <summary>
        /// 店铺类型.默认all,商城:b,集市:c
        /// </summary>
        public string ShopType { get; set; }

        /// <summary>
        /// default(默认排序,关联推荐相关度),price_desc(价格从高到低), price_asc(价格从低到高),commissionRate_desc(佣金比率从高到低), commissionRate_asc(佣金比率从低到高), commissionNum_desc(成交量成高到低), commissionNum_asc(成交量从低到高)
        /// </summary>
        public string Sort { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.atb.items.relate.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("cid", this.Cid);
            parameters.Add("fields", this.Fields);
            parameters.Add("max_count", this.MaxCount);
            parameters.Add("open_iid", this.OpenIid);
            parameters.Add("relate_type", this.RelateType);
            parameters.Add("seller_id", this.SellerId);
            parameters.Add("shop_type", this.ShopType);
            parameters.Add("sort", this.Sort);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("fields", this.Fields);
            RequestValidator.ValidateMaxListSize("fields", this.Fields, 20);
            RequestValidator.ValidateRequired("open_iid", this.OpenIid);
            RequestValidator.ValidateRequired("relate_type", this.RelateType);
        }

        #endregion
    }
}
