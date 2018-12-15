using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.trade.create
    /// </summary>
    public class TradeCreateRequest : BaseTopRequest<Top.Api.Response.TradeCreateResponse>
    {
        /// <summary>
        /// 邮寄地址的id（当此参数不为空时收货地址详细信息已经确认，则new_divisioncode、new_postcode、new_address、new_name、new_mobile、new_phone将被无效。当有新的收货地址时，address_id则为空，收货地址将采用新的收货地址信息，则new_divisioncode、new_postcode、new_address、new_name、new_mobile、new_phone的值将被采用。）
        /// </summary>
        public string AddressId { get; set; }

        /// <summary>
        /// 买家留言
        /// </summary>
        public string BuyerMemo { get; set; }

        /// <summary>
        /// 一个或多个商品所有数量优惠id列表，多个商品用'θ'号不同宝贝优惠id，'θ'分割item_promotions可以为空并且必保持与num_iids严格一致
        /// </summary>
        public string ItemPromotions { get; set; }

        /// <summary>
        /// 新增收货地址的收件人地址（当address_id不为空时，此值传入无效，此时可以不传入）
        /// </summary>
        public string NewAddress { get; set; }

        /// <summary>
        /// 新的收货地址是否保存，如果设置为false，则将新的收货地址不保存到买家收货地址列表中，如果为true，则保存（此参数不传，默认是保存这个收货地址的）
        /// </summary>
        public Nullable<bool> NewAddressStore { get; set; }

        /// <summary>
        /// 新增收货地址的区域码（通过调用接口得到标准的区域码。当address_id不为空时，此值传入无效，此时可以不传入）区位码严格按照提供的xml中省市区对应的区域码来设置。(TOP提供省市区转换区域码的xml文件)
        /// </summary>
        public string NewDivisioncode { get; set; }

        /// <summary>
        /// 新增收货地址的手机号码（当address_id不为空时，此值传入无效，此时可以不传入。new_mobile与new_phone至少一个不为空）
        /// </summary>
        public string NewMobile { get; set; }

        /// <summary>
        /// 新增收货地址的收件人姓名（当address_id不为空时，此值传入无效，此时可以不传入）
        /// </summary>
        public string NewName { get; set; }

        /// <summary>
        /// 新增收货地址的电话号码（当address_id不为空时，此值传入无效，此时可以不传入。new_mobile与new_phone至少一个不为空）
        /// </summary>
        public string NewPhone { get; set; }

        /// <summary>
        /// 新增收货地址的邮编（当address_id不为空时，此值传入无效，此时可以不传入）
        /// </summary>
        public string NewPostcode { get; set; }

        /// <summary>
        /// 商品的id序列，用'θ'间隔并列的商品id。(最多支持50个商品id)
        /// </summary>
        public string NumIids { get; set; }

        /// <summary>
        /// 一个或多个商品所有数量nums列表，多个商品用'θ'号不同宝贝num，'θ'分割nums不能为空并且必保持与num_iids严格一致。
        /// </summary>
        public string Nums { get; set; }

        /// <summary>
        /// medicine(商城医药馆)、MSHOP(手机店铺)和AGENT(第三方网站)、wap_medicine(天猫无线医药馆)、ownshop（商家官网）
        /// </summary>
        public string OrderFrom { get; set; }

        /// <summary>
        /// 订单的外部订单号，用来防止重复提交。
        /// </summary>
        public string OutId { get; set; }

        /// <summary>
        /// 淘宝客id（创建交易支持淘宝客可以传入该参数）
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 创建交易时的物流方式。  可选值：ems, express, post, free
        /// </summary>
        public string ShippingType { get; set; }

        /// <summary>
        /// 店铺级优惠的id
        /// </summary>
        public string ShopPromotion { get; set; }

        /// <summary>
        /// 一件或者多件商品所有skuid列表，多个商品用'θ'号不同宝贝skuid，'θ'分割的sku若为空则用0表示，且分隔的序列必保持与num_iids严格一致。
        /// </summary>
        public string SkuIids { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.trade.create";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("address_id", this.AddressId);
            parameters.Add("buyer_memo", this.BuyerMemo);
            parameters.Add("item_promotions", this.ItemPromotions);
            parameters.Add("new_address", this.NewAddress);
            parameters.Add("new_address_store", this.NewAddressStore);
            parameters.Add("new_divisioncode", this.NewDivisioncode);
            parameters.Add("new_mobile", this.NewMobile);
            parameters.Add("new_name", this.NewName);
            parameters.Add("new_phone", this.NewPhone);
            parameters.Add("new_postcode", this.NewPostcode);
            parameters.Add("num_iids", this.NumIids);
            parameters.Add("nums", this.Nums);
            parameters.Add("order_from", this.OrderFrom);
            parameters.Add("out_id", this.OutId);
            parameters.Add("pid", this.Pid);
            parameters.Add("shipping_type", this.ShippingType);
            parameters.Add("shop_promotion", this.ShopPromotion);
            parameters.Add("sku_iids", this.SkuIids);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("num_iids", this.NumIids);
            RequestValidator.ValidateRequired("nums", this.Nums);
            RequestValidator.ValidateRequired("order_from", this.OrderFrom);
            RequestValidator.ValidateRequired("out_id", this.OutId);
            RequestValidator.ValidateRequired("shipping_type", this.ShippingType);
            RequestValidator.ValidateRequired("sku_iids", this.SkuIids);
        }

        #endregion
    }
}
