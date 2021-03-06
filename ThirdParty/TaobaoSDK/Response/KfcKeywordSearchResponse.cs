using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// KfcKeywordSearchResponse.
    /// </summary>
    public class KfcKeywordSearchResponse : TopResponse
    {
        /// <summary>
        /// KFC关键词匹配返回的结果信息
        /// </summary>
        [XmlElement("kfc_search_result")]
        public Baichuan.Api.Domain.KfcSearchResult KfcSearchResult { get; set; }
    }
}
