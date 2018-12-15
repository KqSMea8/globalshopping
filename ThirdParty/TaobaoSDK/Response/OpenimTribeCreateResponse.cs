using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// OpenimTribeCreateResponse.
    /// </summary>
    public class OpenimTribeCreateResponse : TopResponse
    {
        /// <summary>
        /// 创建群的信息
        /// </summary>
        [XmlElement("tribe_info")]
        public Baichuan.Api.Domain.TribeInfo TribeInfo { get; set; }
    }
}
