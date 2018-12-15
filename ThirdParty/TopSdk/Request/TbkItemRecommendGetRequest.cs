using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.tbk.item.recommend.get
    /// </summary>
    public class TbkItemRecommendGetRequest : BaseTopRequest<Top.Api.Response.TbkItemRecommendGetResponse>
    {
        /// <summary>
        /// 后台类目Id，仅支持叶子类目Id，即通过taobao.itemcats.get获取到is_parent=false的cid
        /// </summary>
        public Nullable<long> Cat { get; set; }

        /// <summary>
        /// 返回数量，默认20，最大值40
        /// </summary>
        public Nullable<long> Count { get; set; }

        /// <summary>
        /// 需返回的字段列表
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        public Nullable<long> NumIid { get; set; }

        /// <summary>
        /// 链接形式：1：PC，2：无线，默认：１
        /// </summary>
        public Nullable<long> Platform { get; set; }

        /// <summary>
        /// 推荐类型，1:同类商品推荐，2:异类商品推荐，3:同店商品推荐，此时必须输入num_iid;4:店铺热门推荐，此时必须输入user_id，这里的user_id得通过taobao.tbk.shop.get这个接口去获取user_id字段;5:类目热门推荐，此时必须输入cid
        /// </summary>
        public Nullable<long> RelateType { get; set; }

        /// <summary>
        /// 卖家Id
        /// </summary>
        public Nullable<long> UserId { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.tbk.item.recommend.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("cat", this.Cat);
            parameters.Add("count", this.Count);
            parameters.Add("fields", this.Fields);
            parameters.Add("num_iid", this.NumIid);
            parameters.Add("platform", this.Platform);
            parameters.Add("relate_type", this.RelateType);
            parameters.Add("user_id", this.UserId);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
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
