using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// DeliverySendResponse.
    /// </summary>
    public class DeliverySendResponse : TopResponse
    {
        /// <summary>
        /// 只返回is_success
        /// </summary>
        [XmlElement("shipping")]
        public Baichuan.Api.Domain.Shipping Shipping { get; set; }
    }
}
