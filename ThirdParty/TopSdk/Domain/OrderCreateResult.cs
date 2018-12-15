using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Domain
{
    /// <summary>
    /// OrderCreateResult Data Structure.
    /// </summary>
    [Serializable]
    public class OrderCreateResult : TopObject
    {
        /// <summary>
        /// 支付宝交易号
        /// </summary>
        [XmlElement("alipay_no")]
        public string AlipayNo { get; set; }

        /// <summary>
        /// 支付宝交易号列表
        /// </summary>
        [XmlArray("alipay_nos")]
        [XmlArrayItem("string")]
        public List<string> AlipayNos { get; set; }

        /// <summary>
        /// 支付宝支付链接
        /// </summary>
        [XmlElement("alipay_url")]
        public string AlipayUrl { get; set; }

        /// <summary>
        /// 订单号（创建交易的时候返回单个订单号）
        /// </summary>
        [XmlElement("tid")]
        public string Tid { get; set; }

        /// <summary>
        /// 交易总金额
        /// </summary>
        [XmlElement("total_fee")]
        public string TotalFee { get; set; }
    }
}
