using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// ItemSkuUpdateResponse.
    /// </summary>
    public class ItemSkuUpdateResponse : TopResponse
    {
        /// <summary>
        /// 商品Sku
        /// </summary>
        [XmlElement("sku")]
        public Baichuan.Api.Domain.Sku Sku { get; set; }
    }
}
