using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// AlibabaBaichuanAppcontentUploadResponse.
    /// </summary>
    public class AlibabaBaichuanAppcontentUploadResponse : TopResponse
    {
        /// <summary>
        /// 接口返回model
        /// </summary>
        [XmlElement("result")]
        public ResultDomain Result { get; set; }

	/// <summary>
/// ResultDomain Data Structure.
/// </summary>
[Serializable]

public class ResultDomain : TopObject
{
	        /// <summary>
	        /// 错误码
	        /// </summary>
	        [XmlElement("error_code")]
	        public string ErrorCode { get; set; }
	
	        /// <summary>
	        /// 错误信息
	        /// </summary>
	        [XmlElement("error_msg")]
	        public string ErrorMsg { get; set; }
	
	        /// <summary>
	        /// 预留属性扩展
	        /// </summary>
	        [XmlElement("extra")]
	        public string Extra { get; set; }
	
	        /// <summary>
	        /// 返回的信息
	        /// </summary>
	        [XmlElement("message")]
	        public string Message { get; set; }
	
	        /// <summary>
	        /// 具体返回的内容
	        /// </summary>
	        [XmlElement("result_value")]
	        public string ResultValue { get; set; }
	
	        /// <summary>
	        /// 标识成功或失败
	        /// </summary>
	        [XmlElement("success")]
	        public bool Success { get; set; }
	
	        /// <summary>
	        /// 预留信息
	        /// </summary>
	        [XmlElement("tracker")]
	        public string Tracker { get; set; }
}

    }
}
