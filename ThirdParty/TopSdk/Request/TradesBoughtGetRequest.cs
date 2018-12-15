using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.trades.bought.get
    /// </summary>
    public class TradesBoughtGetRequest : BaseTopRequest<Top.Api.Response.TradesBoughtGetResponse>
    {
        /// <summary>
        /// 查询交易创建时间结束。格式:yyyy-MM-dd HH:mm:ss
        /// </summary>
        public Nullable<DateTime> EndCreated { get; set; }

        /// <summary>
        /// 可选值有ershou(二手市场的订单）,service（商城服务子订单）作为扩展类型筛选只能做单个ext_type查询，不能全部查询所有的ext_type类型
        /// </summary>
        public string ExtType { get; set; }

        /// <summary>
        /// 需要返回的字段列表，多个字段用半角逗号分隔，可选值为返回示例中能看到的所有字段。
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// 页码。取值范围:大于零的整数; 默认值:1
        /// </summary>
        public Nullable<long> PageNo { get; set; }

        /// <summary>
        /// 每页条数。取值范围:大于零的整数; 默认值:40;最大值:100
        /// </summary>
        public Nullable<long> PageSize { get; set; }

        /// <summary>
        /// 交易是否评价.默认查询所有评价状态的数据，除了默认值外每次只能查询一种状态。可选值： RATE_UNBUYER(买家未评) RATE_UNSELLER(卖家未评) RATE_BUYER_UNSELLER(买家已评，卖家未评) RATE_UNBUYER_SELLER(买家未评，卖家已评)
        /// </summary>
        public string RateStatus { get; set; }

        /// <summary>
        /// 卖家昵称
        /// </summary>
        public string SellerNick { get; set; }

        /// <summary>
        /// 是否显示订单回收站删除的订单，false不显示true（显示），如果不传入，默认是不显示的
        /// </summary>
        public Nullable<bool> ShowDeleted { get; set; }

        /// <summary>
        /// 查询交易创建时间开始。格式:yyyy-MM-dd HH:mm:ss
        /// </summary>
        public Nullable<DateTime> StartCreated { get; set; }

        /// <summary>
        /// 交易状态（<a href="http://open.taobao.com/doc/detail.htm?id=102856" target="_blank">查看可选值</a>），默认查询所有交易状态的数据，除了默认值外每次只能查询一种状态。
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 交易类型列表（<a href="http://open.taobao.com/doc/detail.htm?id=102855" target="_blank">查看可选值</a>），一次查询多种类型可用半角逗号分隔，默认同时查询guarantee_trade,auto_delivery,ec,cod,step这5种类型的数据。
        /// </summary>
        public string Type { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.trades.bought.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("end_created", this.EndCreated);
            parameters.Add("ext_type", this.ExtType);
            parameters.Add("fields", this.Fields);
            parameters.Add("page_no", this.PageNo);
            parameters.Add("page_size", this.PageSize);
            parameters.Add("rate_status", this.RateStatus);
            parameters.Add("seller_nick", this.SellerNick);
            parameters.Add("show_deleted", this.ShowDeleted);
            parameters.Add("start_created", this.StartCreated);
            parameters.Add("status", this.Status);
            parameters.Add("type", this.Type);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("fields", this.Fields);
            RequestValidator.ValidateMinValue("page_no", this.PageNo, 1);
            RequestValidator.ValidateMinValue("page_size", this.PageSize, 1);
        }

        #endregion
    }
}
