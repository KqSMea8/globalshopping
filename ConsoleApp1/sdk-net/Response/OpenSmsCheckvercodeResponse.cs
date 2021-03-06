using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// OpenSmsCheckvercodeResponse.
    /// </summary>
    public class OpenSmsCheckvercodeResponse : TopResponse
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        [XmlElement("result")]
        public Top.Api.Domain.BmcResult Result { get; set; }

    }
}
