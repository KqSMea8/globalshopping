using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// BaichuanTaokeTraceResponse.
    /// </summary>
    public class BaichuanTaokeTraceResponse : TopResponse
    {
        /// <summary>
        /// name
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }
    }
}
