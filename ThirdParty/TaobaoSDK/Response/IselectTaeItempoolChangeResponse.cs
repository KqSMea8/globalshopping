using System;
using System.Xml.Serialization;

namespace Baichuan.Api.Response
{
    /// <summary>
    /// IselectTaeItempoolChangeResponse.
    /// </summary>
    public class IselectTaeItempoolChangeResponse : TopResponse
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        [XmlElement("err_code")]
        public string ErrCode { get; set; }

        /// <summary>
        /// 错误描述
        /// </summary>
        [XmlElement("err_msg")]
        public string ErrMsg { get; set; }

        /// <summary>
        /// 操作结果
        /// </summary>
        [XmlElement("error")]
        public bool Error { get; set; }
    }
}
