using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// Itemlist Data Structure.
    /// </summary>
    [Serializable]
    public class Itemlist : TopObject
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        [XmlElement("auction_id")]
        public long AuctionId { get; set; }

        /// <summary>
        /// 商品主图
        /// </summary>
        [XmlElement("pic_url")]
        public string PicUrl { get; set; }

        /// <summary>
        /// 商品价格,元为单位,精确到分
        /// </summary>
        [XmlElement("price")]
        public string Price { get; set; }

        /// <summary>
        /// 商品池对应的规则的ID
        /// </summary>
        [XmlElement("rule_id")]
        public long RuleId { get; set; }

        /// <summary>
        /// 卖家ID
        /// </summary>
        [XmlElement("seller_id")]
        public long SellerId { get; set; }

        /// <summary>
        /// 卖家昵称
        /// </summary>
        [XmlElement("seller_nick")]
        public string SellerNick { get; set; }

        /// <summary>
        /// 店铺类型,0淘宝店铺,1天猫店铺
        /// </summary>
        [XmlElement("shop_type")]
        public long ShopType { get; set; }

        /// <summary>
        /// 商品标题
        /// </summary>
        [XmlElement("title")]
        public string Title { get; set; }

        /// <summary>
        /// 是否是淘客商品,0不是,1是
        /// </summary>
        [XmlElement("tk_item")]
        public long TkItem { get; set; }
    }
}
