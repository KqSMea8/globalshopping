using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// ISelectItem Data Structure.
    /// </summary>
    [Serializable]
    public class ISelectItem : TopObject
    {
        /// <summary>
        /// 折后价,单位为元,精确到分,没有折扣就和原价相等
        /// </summary>
        [XmlElement("discount_price")]
        public string DiscountPrice { get; set; }

        /// <summary>
        /// 商品混淆id
        /// </summary>
        [XmlElement("open_iid")]
        public string OpenIid { get; set; }

        /// <summary>
        /// 商品主图url
        /// </summary>
        [XmlElement("pic_url")]
        public string PicUrl { get; set; }

        /// <summary>
        /// 商品多图地址，第一张是主图
        /// </summary>
        [XmlElement("pics")]
        public string Pics { get; set; }

        /// <summary>
        /// 商品原价,单位为元,精确到分
        /// </summary>
        [XmlElement("price")]
        public string Price { get; set; }

        /// <summary>
        /// 商品属性、属性值串，可能为空，格式p:v;p:v
        /// </summary>
        [XmlElement("properties_and_values")]
        public string PropertiesAndValues { get; set; }

        /// <summary>
        /// 商品的店铺类型，0为淘宝店铺，1为天猫店铺
        /// </summary>
        [XmlElement("shop_type")]
        public long ShopType { get; set; }

        /// <summary>
        /// 选品商品上的标签id
        /// </summary>
        [XmlElement("tag_id")]
        public long TagId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [XmlElement("title")]
        public string Title { get; set; }
    }
}
