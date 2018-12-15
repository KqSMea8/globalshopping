using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// AlibabaBaichuanAppeventBatchuploadResponse.
    /// </summary>
    public class AlibabaBaichuanAppeventBatchuploadResponse : TopResponse
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
	        /// 额外信息
	        /// </summary>
	        [XmlElement("extra")]
	        public string Extra { get; set; }
	
	        /// <summary>
	        /// 返回信息
	        /// </summary>
	        [XmlElement("message")]
	        public string Message { get; set; }
	
	        /// <summary>
	        /// 返回具体值
	        /// </summary>
	        [XmlElement("result_value")]
	        public string ResultValue { get; set; }
	
	        /// <summary>
	        /// 成功或失败
	        /// </summary>
	        [XmlElement("success")]
	        public bool Success { get; set; }
	
	        /// <summary>
	        /// 额外扩展
	        /// </summary>
	        [XmlElement("tracker")]
	        public string Tracker { get; set; }
}

    }
}
