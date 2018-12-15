using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// ItemPropimgUploadResponse.
    /// </summary>
    public class ItemPropimgUploadResponse : TopResponse
    {
        /// <summary>
        /// PropImg属性图片结构
        /// </summary>
        [XmlElement("prop_img")]
        public Baichuan.Api.Domain.PropImg PropImg { get; set; }
    }
}
