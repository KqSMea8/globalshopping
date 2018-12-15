using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// RetailStore Data Structure.
    /// </summary>
    [Serializable]
    public class RetailStore : TopObject
    {
        /// <summary>
        /// 地址
        /// </summary>
        [XmlElement("address")]
        public string Address { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        [XmlElement("posx")]
        public string Posx { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        [XmlElement("posy")]
        public string Posy { get; set; }

        /// <summary>
        /// 门店id
        /// </summary>
        [XmlElement("store_id")]
        public string StoreId { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [XmlArray("telno_list")]
        [XmlArrayItem("string")]
        public List<string> TelnoList { get; set; }
    }
}
