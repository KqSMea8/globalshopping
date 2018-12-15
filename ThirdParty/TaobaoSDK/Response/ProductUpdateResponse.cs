using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// ProductUpdateResponse.
    /// </summary>
    public class ProductUpdateResponse : TopResponse
    {
        /// <summary>
        /// 返回product数据结构中的：product_id,modified
        /// </summary>
        [XmlElement("product")]
        public Baichuan.Api.Domain.Product Product { get; set; }
    }
}
