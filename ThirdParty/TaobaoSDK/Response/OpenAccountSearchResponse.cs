using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// OpenAccountSearchResponse.
    /// </summary>
    public class OpenAccountSearchResponse : TopResponse
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        [XmlElement("data")]
        public Baichuan.Api.Domain.OpenAccountSearchResult Data { get; set; }
    }
}
