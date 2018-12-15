using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// UserBaichuanRecommendGetResponse.
    /// </summary>
    public class UserBaichuanRecommendGetResponse : TopResponse
    {
        /// <summary>
        /// 商品属性值集合
        /// </summary>
        [XmlElement("result")]
        public string Result { get; set; }
    }
}
