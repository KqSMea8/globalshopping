using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// Userinfos Data Structure.
    /// </summary>
    [Serializable]
    public class Userinfos : TopObject
    {
        /// <summary>
        /// email地址
        /// </summary>
        [XmlElement("email")]
        public string Email { get; set; }

        /// <summary>
        /// 头像url
        /// </summary>
        [XmlElement("icon_url")]
        public string IconUrl { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [XmlElement("mobile")]
        public string Mobile { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [XmlElement("nick")]
        public string Nick { get; set; }

        /// <summary>
        /// im密码
        /// </summary>
        [XmlElement("password")]
        public string Password { get; set; }

        /// <summary>
        /// 淘宝账号
        /// </summary>
        [XmlElement("taobaoid")]
        public string Taobaoid { get; set; }

        /// <summary>
        /// im用户名
        /// </summary>
        [XmlElement("userid")]
        public string Userid { get; set; }
    }
}
