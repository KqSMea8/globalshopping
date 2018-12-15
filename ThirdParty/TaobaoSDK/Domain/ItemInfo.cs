using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// ItemInfo Data Structure.
    /// </summary>
    [Serializable]
    public class ItemInfo : TopObject
    {
        /// <summary>
        /// true:商品可以卖，false:商品不可卖
        /// </summary>
        [XmlElement("in_sale")]
        public string InSale { get; set; }

        /// <summary>
        /// 商品属性
        /// </summary>
        [XmlArray("item_props")]
        [XmlArrayItem("item_property")]
        public List<Baichuan.Api.Domain.ItemProperty> ItemProps { get; set; }

        /// <summary>
        /// 商品图片，第一张主图
        /// </summary>
        [XmlArray("pics")]
        [XmlArrayItem("string")]
        public List<string> Pics { get; set; }

        /// <summary>
        /// true:sku商品买家购买需要选择sku,false:买家购买不需要选择sku
        /// </summary>
        [XmlElement("sku_item")]
        public string SkuItem { get; set; }

        /// <summary>
        /// 商品售卖时间，单位毫秒
        /// </summary>
        [XmlElement("start")]
        public string Start { get; set; }

        /// <summary>
        /// 商品子标题
        /// </summary>
        [XmlElement("sub_title")]
        public string SubTitle { get; set; }

        /// <summary>
        /// 商品标题
        /// </summary>
        [XmlElement("title")]
        public string Title { get; set; }
    }
}
