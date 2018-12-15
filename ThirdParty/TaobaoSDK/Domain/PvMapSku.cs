using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// PvMapSku Data Structure.
    /// </summary>
    [Serializable]
    public class PvMapSku : TopObject
    {
        /// <summary>
        /// 用户选择的属性对
        /// </summary>
        [XmlElement("pv")]
        public string Pv { get; set; }

        /// <summary>
        /// sku id
        /// </summary>
        [XmlElement("sku_id")]
        public string SkuId { get; set; }
    }
}
