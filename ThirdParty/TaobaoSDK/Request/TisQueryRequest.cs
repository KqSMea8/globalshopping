using System;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.tis.query
    /// </summary>
    public class TisQueryRequest : BaseTopRequest<TisQueryResponse>
    {
        /// <summary>
        /// 检索字符串，routeValue是分组键值对
        /// </summary>
        public string QueryStr { get; set; }

        /// <summary>
        /// 索引名称
        /// </summary>
        public string ServiceName { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.tis.query";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("query_str", this.QueryStr);
            parameters.Add("service_name", this.ServiceName);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("query_str", this.QueryStr);
            RequestValidator.ValidateRequired("service_name", this.ServiceName);
            RequestValidator.ValidateMaxLength("service_name", this.ServiceName, 100);
        }

        #endregion
    }
}
