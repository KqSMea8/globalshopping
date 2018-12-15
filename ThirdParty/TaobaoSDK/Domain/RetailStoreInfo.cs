using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Baichuan.Api.Domain
{
    /// <summary>
    /// RetailStoreInfo Data Structure.
    /// </summary>
    [Serializable]
    public class RetailStoreInfo : TopObject
    {
        /// <summary>
        /// 商品可售线下门店
        /// </summary>
        [XmlArray("store_list")]
        [XmlArrayItem("retail_store")]
        public List<Baichuan.Api.Domain.RetailStore> StoreList { get; set; }
    }
}
