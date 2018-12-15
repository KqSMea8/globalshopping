using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// TopatsResultGetResponse.
    /// </summary>
    public class TopatsResultGetResponse : TopResponse
    {
        /// <summary>
        /// 任务结果信息
        /// </summary>
        [XmlElement("task")]
        public Baichuan.Api.Domain.Task Task { get; set; }
    }
}
