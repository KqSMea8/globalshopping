using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// SkuPropertyValue Data Structure.
    /// </summary>
    [Serializable]
    public class SkuPropertyValue : TopObject
    {
        /// <summary>
        /// sku属性url，地址：http://img02.taobaocdn.com/uploaded/i2/916162201/T2kvHcXqpXXXXXXXXX_!!916162201.jpg
        /// </summary>
        [XmlElement("img_url")]
        public string ImgUrl { get; set; }

        /// <summary>
        /// sku属性值名称
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// sku属性值id
        /// </summary>
        [XmlElement("value_id")]
        public string ValueId { get; set; }
    }
}
