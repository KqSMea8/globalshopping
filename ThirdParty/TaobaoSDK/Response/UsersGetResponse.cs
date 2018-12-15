using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// UsersGetResponse.
    /// </summary>
    public class UsersGetResponse : TopResponse
    {
        /// <summary>
        /// 用户信息列表
        /// </summary>
        [XmlArray("users")]
        [XmlArrayItem("user")]
        public List<Baichuan.Api.Domain.User> Users { get; set; }
    }
}
