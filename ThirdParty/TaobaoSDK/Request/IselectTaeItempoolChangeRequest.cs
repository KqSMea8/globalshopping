using System;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.iselect.tae.itempool.change
    /// </summary>
    public class IselectTaeItempoolChangeRequest : BaseTopRequest<IselectTaeItempoolChangeResponse>
    {
        /// <summary>
        /// 操作类型，0：新增商品，1：删除商品
        /// </summary>
        public Nullable<long> Act { get; set; }

        /// <summary>
        /// 商品的open_iid
        /// </summary>
        public string OpenIid { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.iselect.tae.itempool.change";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("act", this.Act);
            parameters.Add("open_iid", this.OpenIid);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("act", this.Act);
            RequestValidator.ValidateRequired("open_iid", this.OpenIid);
        }

        #endregion
    }
}
