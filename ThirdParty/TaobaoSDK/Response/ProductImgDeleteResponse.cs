using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// ProductImgDeleteResponse.
    /// </summary>
    public class ProductImgDeleteResponse : TopResponse
    {
        /// <summary>
        /// 返回productimg中的：id,product_id
        /// </summary>
        [XmlElement("product_img")]
        public Baichuan.Api.Domain.ProductImg ProductImg { get; set; }
    }
}
