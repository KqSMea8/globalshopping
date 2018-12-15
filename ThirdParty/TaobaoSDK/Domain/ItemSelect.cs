using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// ItemSelect Data Structure.
    /// </summary>
    [Serializable]
    public class ItemSelect : TopObject
    {
        /// <summary>
        /// 商品类目名称
        /// </summary>
        [XmlElement("category_name")]
        public string CategoryName { get; set; }

        /// <summary>
        /// 后台类目id
        /// </summary>
        [XmlElement("cid")]
        public long Cid { get; set; }

        /// <summary>
        /// 商品的最近修改时间。格式为yyyymmddhhmmss
        /// </summary>
        [XmlElement("last_modified")]
        public string LastModified { get; set; }

        /// <summary>
        /// 商品id。字段已经废弃，请使用open_iid
        /// </summary>
        [XmlElement("num_iid")]
        public long NumIid { get; set; }

        /// <summary>
        /// 混淆的商品id
        /// </summary>
        [XmlElement("open_iid")]
        public string OpenIid { get; set; }

        /// <summary>
        /// 商家ERP商品ID
        /// </summary>
        [XmlElement("outer_id")]
        public string OuterId { get; set; }

        /// <summary>
        /// 商品主图
        /// </summary>
        [XmlElement("pict_url")]
        public string PictUrl { get; set; }

        /// <summary>
        /// 商品价格(元)
        /// </summary>
        [XmlElement("price")]
        public string Price { get; set; }

        /// <summary>
        /// 选品输出结果中包含ｓｋｕ信息
        /// </summary>
        [XmlArray("skus")]
        [XmlArrayItem("sku_select_info")]
        public List<Baichuan.Api.Domain.SkuSelectInfo> Skus { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [XmlElement("title")]
        public string Title { get; set; }

        /// <summary>
        /// 0为C店；1为B店
        /// </summary>
        [XmlElement("user_type")]
        public long UserType { get; set; }
    }
}
