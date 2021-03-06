using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// BaichuanPayresultQueryResponse.
    /// </summary>
    public class BaichuanPayresultQueryResponse : TopResponse
    {
        /// <summary>
        /// name
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }
    }
}
