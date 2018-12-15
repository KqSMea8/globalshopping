using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// ItemSkusGetResponse.
    /// </summary>
    public class ItemSkusGetResponse : TopResponse
    {
        /// <summary>
        /// Sku列表
        /// </summary>
        [XmlArray("skus")]
        [XmlArrayItem("sku")]
        public List<Baichuan.Api.Domain.Sku> Skus { get; set; }
    }
}
