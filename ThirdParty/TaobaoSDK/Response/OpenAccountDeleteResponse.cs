using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// OpenAccountDeleteResponse.
    /// </summary>
    public class OpenAccountDeleteResponse : TopResponse
    {
        /// <summary>
        /// 删除结果
        /// </summary>
        [XmlArray("datas")]
        [XmlArrayItem("openaccount_void")]
        public List<Baichuan.Api.Domain.OpenaccountVoid> Datas { get; set; }
    }
}
