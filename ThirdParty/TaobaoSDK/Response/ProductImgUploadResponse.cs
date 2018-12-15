using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// ProductImgUploadResponse.
    /// </summary>
    public class ProductImgUploadResponse : TopResponse
    {
        /// <summary>
        /// 返回产品图片结构中的：url,id,created,modified
        /// </summary>
        [XmlElement("product_img")]
        public Baichuan.Api.Domain.ProductImg ProductImg { get; set; }
    }
}
