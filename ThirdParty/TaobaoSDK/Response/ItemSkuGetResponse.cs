using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// ItemSkuGetResponse.
    /// </summary>
    public class ItemSkuGetResponse : TopResponse
    {
        /// <summary>
        /// Sku
        /// </summary>
        [XmlElement("sku")]
        public Baichuan.Api.Domain.Sku Sku { get; set; }
    }
}
