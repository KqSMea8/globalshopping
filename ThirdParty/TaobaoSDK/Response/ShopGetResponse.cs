using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// ShopGetResponse.
    /// </summary>
    public class ShopGetResponse : TopResponse
    {
        /// <summary>
        /// 店铺信息
        /// </summary>
        [XmlElement("shop")]
        public Baichuan.Api.Domain.Shop Shop { get; set; }
    }
}
