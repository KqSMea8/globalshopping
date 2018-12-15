using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// UserShippingAddressesGetResponse.
    /// </summary>
    public class UserShippingAddressesGetResponse : TopResponse
    {
        /// <summary>
        /// 收货地址信息列表
        /// </summary>
        [XmlArray("shipping_addresses")]
        [XmlArrayItem("shipping_address")]
        public List<Top.Api.Domain.ShippingAddress> ShippingAddresses { get; set; }

    }
}
