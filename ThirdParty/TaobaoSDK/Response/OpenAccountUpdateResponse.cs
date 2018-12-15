using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// OpenAccountUpdateResponse.
    /// </summary>
    public class OpenAccountUpdateResponse : TopResponse
    {
        /// <summary>
        /// update是否成功
        /// </summary>
        [XmlArray("datas")]
        [XmlArrayItem("openaccount_void")]
        public List<Baichuan.Api.Domain.OpenaccountVoid> Datas { get; set; }
    }
}
