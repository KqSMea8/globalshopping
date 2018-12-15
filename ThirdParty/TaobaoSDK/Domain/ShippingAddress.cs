using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// ShippingAddress Data Structure.
    /// </summary>
    [Serializable]
    public class ShippingAddress : TopObject
    {
        /// <summary>
        /// 收货地址编号
        /// </summary>
        [XmlElement("address_id")]
        public long AddressId { get; set; }

        /// <summary>
        /// 是否作为默认收货地址
        /// </summary>
        [XmlElement("is_default")]
        public bool IsDefault { get; set; }

        /// <summary>
        /// 收货人地址信息
        /// </summary>
        [XmlElement("location")]
        public Baichuan.Api.Domain.Location Location { get; set; }

        /// <summary>
        /// 收货人移动电话号码
        /// </summary>
        [XmlElement("mobile")]
        public string Mobile { get; set; }

        /// <summary>
        /// 收货人固定电话号码
        /// </summary>
        [XmlElement("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        [XmlElement("receiver_name")]
        public string ReceiverName { get; set; }
    }
}
