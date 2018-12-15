using System;
using System.Xml.Serialization;

namespace Top.Api.Domain
{
    /// <summary>
    /// FeeDesc Data Structure.
    /// </summary>
    [Serializable]
    public class FeeDesc : TopObject
    {
        /// <summary>
        /// 运送方式费用，<font color = red>注意：单位为分</font>  <br/>  <font color = red>  比如:一笔订单的快递的费用为1500</font>
        /// </summary>
        [XmlElement("fee")]
        public string Fee { get; set; }

        /// <summary>
        /// 费用项说明，一般是空
        /// </summary>
        [XmlElement("memo")]
        public string Memo { get; set; }

        /// <summary>
        /// 描述费用项名称  <br/>  <font color = red>比如：  快递(express)、平邮(post)、EMS(ems)  </font>
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// 运费对应的服务类型  <br/>  <font color = red>  快递对应的服务类型为-4、平邮(-1)、EMS(-7)</font>
        /// </summary>
        [XmlElement("service_type")]
        public long ServiceType { get; set; }
    }
}
