using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// DeliveryInfo Data Structure.
    /// </summary>
    [Serializable]
    public class DeliveryInfo : TopObject
    {
        /// <summary>
        /// 运费信息
        /// </summary>
        [XmlArray("carriage_list")]
        [XmlArrayItem("carriage")]
        public List<Baichuan.Api.Domain.Carriage> CarriageList { get; set; }

        /// <summary>
        /// 物流目的地
        /// </summary>
        [XmlElement("destination")]
        public string Destination { get; set; }

        /// <summary>
        /// 所在地
        /// </summary>
        [XmlElement("location")]
        public string Location { get; set; }
    }
}
