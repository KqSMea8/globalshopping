using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// IselectTaeItempoolSearchResponse.
    /// </summary>
    public class IselectTaeItempoolSearchResponse : TopResponse
    {
        /// <summary>
        /// 搜索引擎最近一次dump日期,用于校验数据时效性。本字段仅用于参考，勿用于任何业务判断，避免出现未知错误。
        /// </summary>
        [XmlElement("dump_time")]
        public string DumpTime { get; set; }

        /// <summary>
        /// 调用是否成功
        /// </summary>
        [XmlElement("is_success")]
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 商品池数据集合
        /// </summary>
        [XmlArray("item_list")]
        [XmlArrayItem("itemlist")]
        public List<Baichuan.Api.Domain.Itemlist> ItemList { get; set; }

        /// <summary>
        /// 调用错误时的提示信息
        /// </summary>
        [XmlElement("message")]
        public string Message { get; set; }
    }
}
