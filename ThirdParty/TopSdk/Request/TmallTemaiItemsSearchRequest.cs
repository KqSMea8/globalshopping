using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: tmall.temai.items.search
    /// </summary>
    public class TmallTemaiItemsSearchRequest : BaseTopRequest<Top.Api.Response.TmallTemaiItemsSearchResponse>
    {
        /// <summary>
        /// 特卖前台类目id，传入的必须是特卖类目50100982或其下的二级类目。
        /// </summary>
        public Nullable<long> Cat { get; set; }

        /// <summary>
        /// 排序类型。类型包括： s: 人气排序 p: 价格从低到高; pd: 价格从高到低; d: 月销量从高到低; pt: 按发布时间排序.
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// 表示查询起始位置:  start=0:返回第1条记录到第48条记录（即第一页）；  start=48:返回第48条记录到第96条记录（即第二页）；  start=96，start=144，start=192......  依此类推，每次加start值加48(每页返回记录数固定为48条)
        /// </summary>
        public Nullable<long> Start { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "tmall.temai.items.search";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("cat", this.Cat);
            parameters.Add("sort", this.Sort);
            parameters.Add("start", this.Start);
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
