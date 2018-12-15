using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// ItemSkuAddResponse.
    /// </summary>
    public class ItemSkuAddResponse : TopResponse
    {
        /// <summary>
        /// sku
        /// </summary>
        [XmlElement("sku")]
        public Baichuan.Api.Domain.Sku Sku { get; set; }
    }
}
