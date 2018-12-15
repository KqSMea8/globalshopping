using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// OpenimTribeGettribeinfoResponse.
    /// </summary>
    public class OpenimTribeGettribeinfoResponse : TopResponse
    {
        /// <summary>
        /// 群信息
        /// </summary>
        [XmlElement("tribe_info")]
        public Baichuan.Api.Domain.TribeInfo TribeInfo { get; set; }
    }
}
