using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// XItem Data Structure.
    /// </summary>
    [Serializable]
    public class XItem : TopObject
    {
        /// <summary>
        /// 商品叶子类目
        /// </summary>
        [XmlElement("cid")]
        public long Cid { get; set; }

        /// <summary>
        /// 是否淘客商品
        /// </summary>
        [XmlElement("istk")]
        public bool Istk { get; set; }

        /// <summary>
        /// 聚划算活动结束时间，1970年到现在的毫秒数。如果不是聚划算商品，该值为空
        /// </summary>
        [XmlElement("ju_end")]
        public long JuEnd { get; set; }

        /// <summary>
        /// 是否是聚划算商品,如果查询参数的fields里没有设置ju条件，该值为空
        /// </summary>
        [XmlElement("ju_item")]
        public bool JuItem { get; set; }

        /// <summary>
        /// 聚划算参团价格，如果不是聚划算商品，该值为空
        /// </summary>
        [XmlElement("ju_price")]
        public long JuPrice { get; set; }

        /// <summary>
        /// 聚划算活动开始时间，1970年到现在的毫秒数。如果不是聚划算商品，该值为空
        /// </summary>
        [XmlElement("ju_start")]
        public long JuStart { get; set; }

        /// <summary>
        /// 位置信息
        /// </summary>
        [XmlElement("location")]
        public Baichuan.Api.Domain.XLocation Location { get; set; }

        /// <summary>
        /// 是否天猫宝贝. true 是, false 不是
        /// </summary>
        [XmlElement("mall")]
        public bool Mall { get; set; }

        /// <summary>
        /// 卖家nick
        /// </summary>
        [XmlElement("nick")]
        public string Nick { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        [XmlElement("num")]
        public long Num { get; set; }

        /// <summary>
        /// 混淆的商品ID(准备废弃，由open_iid代替)
        /// </summary>
        [XmlElement("open_auction_iid")]
        public string OpenAuctionIid { get; set; }

        /// <summary>
        /// 商品ID 注意这个不是混淆商品ID
        /// </summary>
        [XmlElement("open_id")]
        public long OpenId { get; set; }

        /// <summary>
        /// 商品混淆ID
        /// </summary>
        [XmlElement("open_iid")]
        public string OpenIid { get; set; }

        /// <summary>
        /// 主图链接
        /// </summary>
        [XmlElement("pic_url")]
        public string PicUrl { get; set; }

        /// <summary>
        /// 平邮邮费. 单位:元,精确到分
        /// </summary>
        [XmlElement("post_fee")]
        public string PostFee { get; set; }

        /// <summary>
        /// 商品优惠价格(PC端),可能为空. 单位:元,精确到分。当PC端访问,且当前时间落在price_start_time到price_end_time区间内时使用该价格
        /// </summary>
        [XmlElement("price")]
        public string Price { get; set; }

        /// <summary>
        /// PC端商品优惠价格开始时间。如果当前没有PC端优惠，该字段为空
        /// </summary>
        [XmlElement("price_end_time")]
        public string PriceEndTime { get; set; }

        /// <summary>
        /// PC端商品优惠价格结束时间。如果当前没有PC端优惠，该字段为空
        /// </summary>
        [XmlElement("price_start_time")]
        public string PriceStartTime { get; set; }

        /// <summary>
        /// 手机端商品优惠价格. 可能为空。单位:元,精确到分。当手机端访问且当前时间落在price_wap_start_time到price_wap_end_time之间的话，使用该价格。如果改价格为空，请使用reserve_price.
        /// </summary>
        [XmlElement("price_wap")]
        public string PriceWap { get; set; }

        /// <summary>
        /// 手机端商品优惠价格结束时间。如果当前没有手机端优惠，该字段为空
        /// </summary>
        [XmlElement("price_wap_end_time")]
        public string PriceWapEndTime { get; set; }

        /// <summary>
        /// 手机端商品优惠价格开始时间。如果当前没有手机端优惠，该字段为空
        /// </summary>
        [XmlElement("price_wap_start_time")]
        public string PriceWapStartTime { get; set; }

        /// <summary>
        /// 消保类型，多个类型以,分割。可取以下值： 2：假一赔三；4：7天无理由退换货；
        /// </summary>
        [XmlElement("promoted_service")]
        public string PromotedService { get; set; }

        /// <summary>
        /// 商品的一口价
        /// </summary>
        [XmlElement("reserve_price")]
        public string ReservePrice { get; set; }

        /// <summary>
        /// 店铺名称
        /// </summary>
        [XmlElement("shop_name")]
        public string ShopName { get; set; }

        /// <summary>
        /// 商品标题
        /// </summary>
        [XmlElement("title")]
        public string Title { get; set; }

        /// <summary>
        /// 淘客佣金比例
        /// </summary>
        [XmlElement("tk_rate")]
        public string TkRate { get; set; }
    }
}
