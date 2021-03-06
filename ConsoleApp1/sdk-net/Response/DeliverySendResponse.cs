using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
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
        public Top.Api.Domain.Shipping Shipping { get; set; }

    }
}
