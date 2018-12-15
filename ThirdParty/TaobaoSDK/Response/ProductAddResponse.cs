using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// ProductAddResponse.
    /// </summary>
    public class ProductAddResponse : TopResponse
    {
        /// <summary>
        /// 产品结构
        /// </summary>
        [XmlElement("product")]
        public Baichuan.Api.Domain.Product Product { get; set; }
    }
}
