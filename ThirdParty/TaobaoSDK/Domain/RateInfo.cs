using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// RateInfo Data Structure.
    /// </summary>
    [Serializable]
    public class RateInfo : TopObject
    {
        /// <summary>
        /// 评价信息
        /// </summary>
        [XmlArray("rate_list")]
        [XmlArrayItem("rate_item")]
        public List<Baichuan.Api.Domain.RateItem> RateList { get; set; }
    }
}
