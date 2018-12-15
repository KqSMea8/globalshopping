using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// UserGetResponse.
    /// </summary>
    public class UserGetResponse : TopResponse
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        [XmlElement("user")]
        public Baichuan.Api.Domain.User User { get; set; }
    }
}
