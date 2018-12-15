using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// SendVerCodeRequest Data Structure.
    /// </summary>
    [Serializable]
    public class SendVerCodeRequest : TopObject
    {
        /// <summary>
        /// appKey
        /// </summary>
        [XmlElement("app_key")]
        public string AppKey { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        [XmlElement("biz_type")]
        public long BizType { get; set; }

        /// <summary>
        /// 设备id
        /// </summary>
        [XmlElement("device_id")]
        public string DeviceId { get; set; }

        /// <summary>
        /// 设备级别的发送次数限制
        /// </summary>
        [XmlElement("device_limit")]
        public long DeviceLimit { get; set; }

        /// <summary>
        /// 发送次数限制的时间，单位为秒
        /// </summary>
        [XmlElement("device_limit_in_time")]
        public long DeviceLimitInTime { get; set; }

        /// <summary>
        /// 场景域，比如登录的验证码不能用于注册
        /// </summary>
        [XmlElement("domain")]
        public string Domain { get; set; }

        /// <summary>
        /// 验证码失效时间，单位为秒
        /// </summary>
        [XmlElement("expire_time")]
        public long ExpireTime { get; set; }

        /// <summary>
        /// 外部的id
        /// </summary>
        [XmlElement("external_id")]
        public string ExternalId { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [XmlElement("mobile")]
        public string Mobile { get; set; }

        /// <summary>
        /// 手机号的次数限制
        /// </summary>
        [XmlElement("mobile_limit")]
        public long MobileLimit { get; set; }

        /// <summary>
        /// 手机号的次数限制的时间
        /// </summary>
        [XmlElement("mobile_limit_in_time")]
        public long MobileLimitInTime { get; set; }

        /// <summary>
        /// session id
        /// </summary>
        [XmlElement("session_id")]
        public string SessionId { get; set; }

        /// <summary>
        /// session级别的发送次数限制
        /// </summary>
        [XmlElement("session_limit")]
        public long SessionLimit { get; set; }

        /// <summary>
        /// 发送次数限制的时间，单位为秒
        /// </summary>
        [XmlElement("session_limit_in_time")]
        public long SessionLimitInTime { get; set; }

        /// <summary>
        /// 签名id
        /// </summary>
        [XmlElement("signature_id")]
        public long SignatureId { get; set; }

        /// <summary>
        /// 模板id
        /// </summary>
        [XmlElement("template_id")]
        public long TemplateId { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [XmlElement("user_id")]
        public long UserId { get; set; }
    }
}
