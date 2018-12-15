using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// TaeExistShopCouponGetResponse.
    /// </summary>
    public class TaeExistShopCouponGetResponse : TopResponse
    {
        /// <summary>
        /// true:存在店铺优惠券；  false:不存在店铺优惠券；
        /// </summary>
        [XmlElement("is_exist")]
        public bool IsExist { get; set; }
    }
}
