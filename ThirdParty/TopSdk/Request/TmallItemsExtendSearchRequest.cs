using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: tmall.items.extend.search
    /// </summary>
    public class TmallItemsExtendSearchRequest : BaseTopRequest<Top.Api.Response.TmallItemsExtendSearchResponse>
    {
        /// <summary>
        /// 商品标签。支持多选过滤,auction_tag=auction_tag1,auction_tag2,不支持天猫精品库8578
        /// </summary>
        public string AuctionTag { get; set; }

        /// <summary>
        /// 品牌的id。支持多选过滤，brand=brand1,brand2
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 前台类目id，支持多选过滤，cat=catid1,catid2
        /// </summary>
        public string Cat { get; set; }

        /// <summary>
        /// 后台类目id，category=categoryId
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 过滤搭配减价宝贝时，combo=1
        /// </summary>
        public Nullable<long> Combo { get; set; }

        /// <summary>
        /// 在宝贝页面中进行价格筛选的时候，如果填写了最高价格，就会显示该字段。
        /// </summary>
        public string EndPrice { get; set; }

        /// <summary>
        /// 宝贝卖家所在地，中文gbk编码
        /// </summary>
        public string Loc { get; set; }

        /// <summary>
        /// 是否多倍积分，1为多倍积分
        /// </summary>
        public Nullable<long> ManyPoints { get; set; }

        /// <summary>
        /// 过滤折扣宝贝时，miaosha=1
        /// </summary>
        public Nullable<long> Miaosha { get; set; }

        /// <summary>
        /// 是否需要spu聚合的开关:1为关闭，不传表示遵循后端聚合逻辑。默认不作spu聚合。
        /// </summary>
        public Nullable<long> Nspu { get; set; }

        /// <summary>
        /// 页码。取值范围：大于零的整数；最大值：100；默认值：1，即默认返回第一页数据。
        /// </summary>
        public Nullable<long> PageNo { get; set; }

        /// <summary>
        /// 每页条数。取值范围：大于零的整数；最大值：100；默认值：40
        /// </summary>
        public Nullable<long> PageSize { get; set; }

        /// <summary>
        /// 是否包邮，-1为包邮
        /// </summary>
        public Nullable<long> PostFee { get; set; }

        /// <summary>
        /// 以“属性id：属性值”的形式传入;
        /// </summary>
        public string Prop { get; set; }

        /// <summary>
        /// 表示搜索的关键字，例如搜索query=nike。当输入关键字为中文时，将对他进行URLEncode的UTF-8格式编码，如 耐克，那么q=%E8%80%90%E5%85%8B。
        /// </summary>
        public string Q { get; set; }

        /// <summary>
        /// 排序类型。类型包括：s: 人气排序p: 价格从低到高;pd: 价格从高到低;d: 月销量从高到低;td: 总销量从高到低;pt: 按发布时间排序.
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// 可以根据产品Id搜索属于这个spu的商品。
        /// </summary>
        public Nullable<long> Spuid { get; set; }

        /// <summary>
        /// 在宝贝页面中进行价格筛选的时候，如果填写了最低价格，就会显示该字段。
        /// </summary>
        public string StartPrice { get; set; }

        /// <summary>
        /// 是否货到付款，1为货到付款
        /// </summary>
        public Nullable<long> SupportCod { get; set; }

        /// <summary>
        /// 可以根据卖家id搜索属于该卖家的商品
        /// </summary>
        public Nullable<long> UserId { get; set; }

        /// <summary>
        /// 过滤vip宝贝时，vip=1
        /// </summary>
        public Nullable<long> Vip { get; set; }

        /// <summary>
        /// 显示旺旺在线卖家的宝贝时，wwonline=1
        /// </summary>
        public Nullable<long> Wwonline { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "tmall.items.extend.search";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("auction_tag", this.AuctionTag);
            parameters.Add("brand", this.Brand);
            parameters.Add("cat", this.Cat);
            parameters.Add("category", this.Category);
            parameters.Add("combo", this.Combo);
            parameters.Add("end_price", this.EndPrice);
            parameters.Add("loc", this.Loc);
            parameters.Add("many_points", this.ManyPoints);
            parameters.Add("miaosha", this.Miaosha);
            parameters.Add("nspu", this.Nspu);
            parameters.Add("page_no", this.PageNo);
            parameters.Add("page_size", this.PageSize);
            parameters.Add("post_fee", this.PostFee);
            parameters.Add("prop", this.Prop);
            parameters.Add("q", this.Q);
            parameters.Add("sort", this.Sort);
            parameters.Add("spuid", this.Spuid);
            parameters.Add("start_price", this.StartPrice);
            parameters.Add("support_cod", this.SupportCod);
            parameters.Add("user_id", this.UserId);
            parameters.Add("vip", this.Vip);
            parameters.Add("wwonline", this.Wwonline);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
        }

        #endregion
    }
}
