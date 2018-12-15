using System;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.iselect.tae.itempool.search
    /// </summary>
    public class IselectTaeItempoolSearchRequest : BaseTopRequest<IselectTaeItempoolSearchResponse>
    {
        /// <summary>
        /// 数字类型的字段的查询参数，可用字段有rule_id,auction_id,price,seller_id,shop_type,tk_item。格式k1,op1,v1;k2,op2,v2。k代表查询的字段，op代表比较操作符(只有5种:>,<,>=,<=,=)。v表示具体数字。分好之间是"与"关系。比如：rule_id,>,100;price,>,100;price,<,200  表示rule_id大于100并且price大于100并且price小于200的商品。  该字段为空时，qwords不能为空
        /// </summary>
        public string Numwords { get; set; }

        /// <summary>
        /// 排序字段,可用字段有title,seller_nick,rule_id,auction_id,price,seller_id,shop_type,tk_item。格式k1:v1;k2:v2。k表示具体排序字段，v表示升序或降序(只有这两种:desc,asc)。表示按某个字段升序或降序排列。比如： price:asc;auction_id:desc  表示按商品价格升序排列(一级排序)，然后按商品ID降序排列(二级排序)
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// 页数,从1开始
        /// </summary>
        public Nullable<long> Pagenum { get; set; }

        /// <summary>
        /// 每页行数
        /// </summary>
        public Nullable<long> Pagesize { get; set; }

        /// <summary>
        /// 匹配查询所用字段,可用字段有title,seller_nick,rule_id,auction_id,price,seller_id,shop_type,tk_item。格式key1:v11,v12;key2:v21,v22。逗号之间的关系是"与"，分号之间的关系也是"与" 。比如：title:女装,短袖;seller_nick:xxx大卖家;tk_item:1 ,表示商品标题匹配"女装"和"短袖"两个字段并且卖家名称匹配xxx大卖家并且是淘客的商品。特别注意：rule_id必须是ISV自己所建的商品规则并且状态是启用。如果填的rule_id不是本人的或者状态非启用，那么会忽略该rule_id。不传入rule_id，会默认调用该ISV建立的所有启用状态的rule_id.  该字段为空时，numwords不能为空。
        /// </summary>
        public string Qwords { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.iselect.tae.itempool.search";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("numwords", this.Numwords);
            parameters.Add("order", this.Order);
            parameters.Add("pagenum", this.Pagenum);
            parameters.Add("pagesize", this.Pagesize);
            parameters.Add("qwords", this.Qwords);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("pagenum", this.Pagenum);
            RequestValidator.ValidateRequired("pagesize", this.Pagesize);
        }

        #endregion
    }
}
