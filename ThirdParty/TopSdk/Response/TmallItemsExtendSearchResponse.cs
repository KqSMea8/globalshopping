using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// TmallItemsExtendSearchResponse.
    /// </summary>
    public class TmallItemsExtendSearchResponse : TopResponse
    {
        /// <summary>
        /// 品牌列表
        /// </summary>
        [XmlArray("brand_list")]
        [XmlArrayItem("tmall_brand")]
        public List<Top.Api.Domain.TmallBrand> BrandList { get; set; }

        /// <summary>
        /// 类目列表
        /// </summary>
        [XmlArray("cat_list")]
        [XmlArrayItem("tmall_cat")]
        public List<Top.Api.Domain.TmallCat> CatList { get; set; }

        /// <summary>
        /// 商品列表
        /// </summary>
        [XmlArray("item_list")]
        [XmlArrayItem("tmall_extend_search_item")]
        public List<Top.Api.Domain.TmallExtendSearchItem> ItemList { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        [XmlElement("q")]
        public string Q { get; set; }

        /// <summary>
        /// 总商品数量
        /// </summary>
        [XmlElement("total_results")]
        public long TotalResults { get; set; }

    }
}
