using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// MobileDescInfo Data Structure.
    /// </summary>
    [Serializable]
    public class MobileDescInfo : TopObject
    {
        /// <summary>
        /// 无线描述信息
        /// </summary>
        [XmlArray("desc_list")]
        [XmlArrayItem("desc_fragment")]
        public List<Baichuan.Api.Domain.DescFragment> DescList { get; set; }
    }
}
