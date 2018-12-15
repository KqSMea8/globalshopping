using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// IselectItemsGetResponse.
    /// </summary>
    public class IselectItemsGetResponse : TopResponse
    {
        /// <summary>
        /// 1
        /// </summary>
        [XmlArray("items")]
        [XmlArrayItem("i_select_item")]
        public List<Baichuan.Api.Domain.ISelectItem> Items { get; set; }
    }
}
