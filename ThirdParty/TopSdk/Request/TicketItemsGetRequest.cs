using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.ticket.items.get
    /// </summary>
    public class TicketItemsGetRequest : BaseTopRequest<Top.Api.Response.TicketItemsGetResponse>
    {
        /// <summary>
        /// 需要返回的门票商品（TicketItem）对象字段，如title,price,skus等。<br>可选值：TicketItem商品结构体中所有字段均可返回；多个字段用“,”分隔。
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// 批量获取信息的商品标识。最多不能超过20个。
        /// </summary>
        public string ItemIds { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.ticket.items.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("fields", this.Fields);
            parameters.Add("item_ids", this.ItemIds);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("fields", this.Fields);
            RequestValidator.ValidateRequired("item_ids", this.ItemIds);
        }

        #endregion
    }
}
