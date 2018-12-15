using System;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.atb.items.detail.get
    /// </summary>
    public class AtbItemsDetailGetRequest : BaseTopRequest<AtbItemsDetailGetResponse>
    {
        /// <summary>
        /// 需返回的字段列表.可选值:AitaobaoItemDetail淘宝客商品结构体中的所有字段;字段之间用","分隔。item_detail需要设置到Item模型下的字段,如设置:open_iid,detail_url等; 只设置item_detail,则不返回的Item下的所有信息.注：item结构中的skus、videos、props_name不返回
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// open_iid串.最大输入10个.格式如:"value1,value2,value3" 用" , "号分隔
        /// </summary>
        public string OpenIids { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.atb.items.detail.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("fields", this.Fields);
            parameters.Add("open_iids", this.OpenIids);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("fields", this.Fields);
            RequestValidator.ValidateMaxListSize("fields", this.Fields, 200);
            RequestValidator.ValidateRequired("open_iids", this.OpenIids);
        }

        #endregion
    }
}
