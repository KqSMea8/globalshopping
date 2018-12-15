using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// AitaobaoItemDetail Data Structure.
    /// </summary>
    [Serializable]
    public class AitaobaoItemDetail : TopObject
    {
        /// <summary>
        /// 商品详细信息. fields中需要设置Item下的字段,如设置:iid,detail_url等; 只设置item_detail,则不返回的Item下的所有信息.
        /// </summary>
        [XmlElement("item")]
        public Baichuan.Api.Domain.Item Item { get; set; }

        /// <summary>
        /// 商品所属卖家的信用等级
        /// </summary>
        [XmlElement("seller_credit_score")]
        public long SellerCreditScore { get; set; }
    }
}
