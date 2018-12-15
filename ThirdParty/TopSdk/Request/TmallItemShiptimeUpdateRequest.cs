using System;
using System.Xml.Serialization;
using Top.Api.Domain;
using System.Collections.Generic;
using System.Xml.Serialization;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: tmall.item.shiptime.update
    /// </summary>
    public class TmallItemShiptimeUpdateRequest : BaseTopRequest<Top.Api.Response.TmallItemShiptimeUpdateResponse>
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public Nullable<long> ItemId { get; set; }

        /// <summary>
        /// 批量更新商品/SKU发货时间的备选项
        /// </summary>
        public string Option { get; set; }

        public UpdateItemShipTimeOption Option_ { set { this.Option = TopUtils.ObjectToJson(value); } } 

        /// <summary>
        /// 被更新发货时间（商品级）；格式和具体设置的发货时间格式相关。绝对发货时间填写yyyy-MM-dd;相对发货时间填写数字。发货时间必须在当前时间后三天。如果设置的绝对时间小于当前时间的三天后，会清除该商品的发货时间设置。如果是相对时间小于3，则会提示出错。如果shiptimeType为0，要清除商品上的发货时间，该字段可以填任意字符,也可以不填。
        /// </summary>
        public string ShipTime { get; set; }

        /// <summary>
        /// 被更新SKU的发货时间，后台会根据三个子属性去查找匹配的sku，如果找到就默认对sku进行更新，当无匹配sku且更新类型针对sku，会报错。
        /// </summary>
        public string SkuShipTimes { get; set; }

        public List<UpdateSkuShipTime> SkuShipTimes_ { set { this.SkuShipTimes = TopUtils.ObjectToJson(value); } } 

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "tmall.item.shiptime.update";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("item_id", this.ItemId);
            parameters.Add("option", this.Option);
            parameters.Add("ship_time", this.ShipTime);
            parameters.Add("sku_ship_times", this.SkuShipTimes);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("item_id", this.ItemId);
            RequestValidator.ValidateRequired("option", this.Option);
            RequestValidator.ValidateObjectMaxListSize("sku_ship_times", this.SkuShipTimes, 20);
        }

        #endregion
    }
}
