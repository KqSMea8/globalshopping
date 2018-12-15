using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// ItemDetailData Data Structure.
    /// </summary>
    [Serializable]
    public class ItemDetailData : TopObject
    {
        /// <summary>
        /// 优惠卷信息
        /// </summary>
        [XmlElement("coupon_info")]
        public Baichuan.Api.Domain.ShopCouponInfo CouponInfo { get; set; }

        /// <summary>
        /// 物流信息
        /// </summary>
        [XmlElement("delivery_info")]
        public Baichuan.Api.Domain.DeliveryInfo DeliveryInfo { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        [XmlElement("desc_info")]
        public Baichuan.Api.Domain.DescInfo DescInfo { get; set; }

        /// <summary>
        /// 购买约束信息
        /// </summary>
        [XmlElement("item_buy_info")]
        public Baichuan.Api.Domain.ItemBuyInfo ItemBuyInfo { get; set; }

        /// <summary>
        /// 商品信息
        /// </summary>
        [XmlElement("item_info")]
        public Baichuan.Api.Domain.ItemInfo ItemInfo { get; set; }

        /// <summary>
        /// 移动描述信息
        /// </summary>
        [XmlElement("mobile_desc_info")]
        public Baichuan.Api.Domain.MobileDescInfo MobileDescInfo { get; set; }

        /// <summary>
        /// 价格信息
        /// </summary>
        [XmlElement("price_info")]
        public Baichuan.Api.Domain.DetailPriceInfo PriceInfo { get; set; }

        /// <summary>
        /// 评价信息
        /// </summary>
        [XmlElement("rate_info")]
        public Baichuan.Api.Domain.RateInfo RateInfo { get; set; }

        /// <summary>
        /// 卖家信息
        /// </summary>
        [XmlElement("seller_info")]
        public Baichuan.Api.Domain.SellerInfo SellerInfo { get; set; }

        /// <summary>
        /// sku信息
        /// </summary>
        [XmlElement("sku_info")]
        public Baichuan.Api.Domain.SkuInfo SkuInfo { get; set; }

        /// <summary>
        /// 库存信息
        /// </summary>
        [XmlElement("stock_info")]
        public Baichuan.Api.Domain.StockInfo StockInfo { get; set; }

        /// <summary>
        /// 门店信息
        /// </summary>
        [XmlElement("store_info")]
        public Baichuan.Api.Domain.RetailStoreInfo StoreInfo { get; set; }
    }
}
