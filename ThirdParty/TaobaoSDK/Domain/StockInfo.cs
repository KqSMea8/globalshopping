using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// StockInfo Data Structure.
    /// </summary>
    [Serializable]
    public class StockInfo : TopObject
    {
        /// <summary>
        /// 商品库存
        /// </summary>
        [XmlElement("item_quantity")]
        public string ItemQuantity { get; set; }

        /// <summary>
        /// sku库存列表
        /// </summary>
        [XmlArray("sku_quantity_list")]
        [XmlArrayItem("sku_quantity")]
        public List<Baichuan.Api.Domain.SkuQuantity> SkuQuantityList { get; set; }
    }
}
