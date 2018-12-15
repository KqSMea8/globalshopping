using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// SkuInfo Data Structure.
    /// </summary>
    [Serializable]
    public class SkuInfo : TopObject
    {
        /// <summary>
        /// 属性sku映射
        /// </summary>
        [XmlArray("pv_map_sku_list")]
        [XmlArrayItem("pv_map_sku")]
        public List<Baichuan.Api.Domain.PvMapSku> PvMapSkuList { get; set; }

        /// <summary>
        /// SKU属性列
        /// </summary>
        [XmlArray("sku_props")]
        [XmlArrayItem("sku_property")]
        public List<Baichuan.Api.Domain.SkuProperty> SkuProps { get; set; }
    }
}
