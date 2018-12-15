using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// DetailPriceInfo Data Structure.
    /// </summary>
    [Serializable]
    public class DetailPriceInfo : TopObject
    {
        /// <summary>
        /// 商品对应的价格
        /// </summary>
        [XmlElement("item_price")]
        public Baichuan.Api.Domain.DetailPrice ItemPrice { get; set; }

        /// <summary>
        /// sku对应的价格列表
        /// </summary>
        [XmlArray("sku_price_list")]
        [XmlArrayItem("sku_price_item")]
        public List<Baichuan.Api.Domain.SkuPriceItem> SkuPriceList { get; set; }
    }
}
