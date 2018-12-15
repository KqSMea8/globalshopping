using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// ShippingAddressesGetResponse.
    /// </summary>
    public class ShippingAddressesGetResponse : TopResponse
    {
        /// <summary>
        /// 收货地址列表
        /// </summary>
        [XmlArray("shipping_addresss")]
        [XmlArrayItem("shipping_address")]
        public List<Baichuan.Api.Domain.ShippingAddress> ShippingAddresss { get; set; }
    }
}
