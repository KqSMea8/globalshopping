using System;
using System.Xml.Serialization;

namespace Top.Api.Domain
{
    /// <summary>
    /// UpdateItemPriceOption Data Structure.
    /// </summary>
    [Serializable]
    public class UpdateItemPriceOption : TopObject
    {
        /// <summary>
        /// 目标币种，非必填，仅用于天猫国际官网同购商家将外币价格修改成人民币价格
        /// </summary>
        [XmlElement("currency_type")]
        public string CurrencyType { get; set; }

        /// <summary>
        /// 是否忽略涉嫌炒信警告信息
        /// </summary>
        [XmlElement("ignore_fake_credit")]
        public Nullable<bool> IgnoreFakeCredit { get; set; }
    }
}
