using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: alibaba.baichuan.appevent.batchupload
    /// </summary>
    public class AlibabaBaichuanAppeventBatchuploadRequest : BaseTopRequest<Top.Api.Response.AlibabaBaichuanAppeventBatchuploadResponse>
    {
        /// <summary>
        /// app标识
        /// </summary>
        public string Appid { get; set; }

        /// <summary>
        /// 场景标识
        /// </summary>
        public string Bizid { get; set; }

        /// <summary>
        /// 具体实例集合
        /// </summary>
        public string Params { get; set; }

        public List<JsonDomain> Params_ { set { this.Params = TopUtils.ObjectToJson(value); } } 

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "alibaba.baichuan.appevent.batchupload";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("appid", this.Appid);
            parameters.Add("bizid", this.Bizid);
            parameters.Add("params", this.Params);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateObjectMaxListSize("params", this.Params, 20);
        }

	/// <summary>
/// JsonDomain Data Structure.
/// </summary>
[Serializable]

public class JsonDomain : TopObject
{
	        /// <summary>
	        /// app用户id
	        /// </summary>
	        [XmlElement("app_user_id")]
	        public string AppUserId { get; set; }
	
	        /// <summary>
	        /// 是否点击
	        /// </summary>
	        [XmlElement("click")]
	        public string Click { get; set; }
	
	        /// <summary>
	        /// 内容id
	        /// </summary>
	        [XmlElement("content_id")]
	        public string ContentId { get; set; }
	
	        /// <summary>
	        /// imei
	        /// </summary>
	        [XmlElement("device_id")]
	        public string DeviceId { get; set; }
	
	        /// <summary>
	        /// 是否跳转
	        /// </summary>
	        [XmlElement("is_out")]
	        public string IsOut { get; set; }
	
	        /// <summary>
	        /// 喜欢程度分数
	        /// </summary>
	        [XmlElement("like_score")]
	        public string LikeScore { get; set; }
	
	        /// <summary>
	        /// 跳转链接
	        /// </summary>
	        [XmlElement("out_url")]
	        public string OutUrl { get; set; }
	
	        /// <summary>
	        /// pvid
	        /// </summary>
	        [XmlElement("pvid")]
	        public string Pvid { get; set; }
	
	        /// <summary>
	        /// scm埋点
	        /// </summary>
	        [XmlElement("scm")]
	        public string Scm { get; set; }
	
	        /// <summary>
	        /// 淘系商品id
	        /// </summary>
	        [XmlElement("tao_item_id")]
	        public string TaoItemId { get; set; }
	
	        /// <summary>
	        /// 淘系用户id
	        /// </summary>
	        [XmlElement("tao_user_id")]
	        public string TaoUserId { get; set; }
	
	        /// <summary>
	        /// 淘系设备唯一id
	        /// </summary>
	        [XmlElement("utdid")]
	        public string Utdid { get; set; }
}

        #endregion
    }
}
