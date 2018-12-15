using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// ItemImgDeleteResponse.
    /// </summary>
    public class ItemImgDeleteResponse : TopResponse
    {
        /// <summary>
        /// 商品图片结构
        /// </summary>
        [XmlElement("item_img")]
        public Baichuan.Api.Domain.ItemImg ItemImg { get; set; }
    }
}
