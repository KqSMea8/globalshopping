using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: tmall.temai.subcats.search
    /// </summary>
    public class TmallTemaiSubcatsSearchRequest : BaseTopRequest<Top.Api.Response.TmallTemaiSubcatsSearchResponse>
    {
        /// <summary>
        /// 父类目ID，固定是特卖前台一级类目id：50100982
        /// </summary>
        public Nullable<long> Cat { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "tmall.temai.subcats.search";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("cat", this.Cat);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("cat", this.Cat);
        }

        #endregion
    }
}
