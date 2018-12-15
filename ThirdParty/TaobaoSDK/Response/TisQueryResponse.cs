using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// TisQueryResponse.
    /// </summary>
    public class TisQueryResponse : TopResponse
    {
        /// <summary>
        /// TIS检索返回结果json字符串
        /// </summary>
        [XmlElement("query_result")]
        public string QueryResult { get; set; }
    }
}
