using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// UmpPromotionIncrementGetResponse.
    /// </summary>
    public class UmpPromotionIncrementGetResponse : TopResponse
    {
        /// <summary>
        /// 优惠详情信息
        /// </summary>
        [XmlElement("promotions")]
        public Top.Api.Domain.PromotionDisplayTop Promotions { get; set; }

    }
}
