using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// OpenAccountTokenApplyResponse.
    /// </summary>
    public class OpenAccountTokenApplyResponse : TopResponse
    {
        /// <summary>
        /// 返回的token结果
        /// </summary>
        [XmlElement("data")]
        public Baichuan.Api.Domain.OpenAccountTokenApplyResult Data { get; set; }
    }
}
