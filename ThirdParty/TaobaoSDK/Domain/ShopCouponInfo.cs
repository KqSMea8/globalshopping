using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// ShopCouponInfo Data Structure.
    /// </summary>
    [Serializable]
    public class ShopCouponInfo : TopObject
    {
        /// <summary>
        /// true|false 是否有优惠卷
        /// </summary>
        [XmlElement("shop_coupon")]
        public string ShopCoupon { get; set; }
    }
}
