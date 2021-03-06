using System;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.cloudpush.notice.android
    /// </summary>
    public class CloudpushNoticeAndroidRequest : BaseTopRequest<CloudpushNoticeAndroidResponse>
    {
        /// <summary>
        /// 通知摘要
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 推送目标: device:推送给设备; account:推送给指定帐号,all: 推送给全部
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// 根据Target来设定，如Target=device, 则对应的值为 设备id1,设备id2. 多个值使用逗号分隔
        /// </summary>
        public string TargetValue { get; set; }

        /// <summary>
        /// 通知的标题.
        /// </summary>
        public string Title { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.cloudpush.notice.android";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("summary", this.Summary);
            parameters.Add("target", this.Target);
            parameters.Add("target_value", this.TargetValue);
            parameters.Add("title", this.Title);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("summary", this.Summary);
            RequestValidator.ValidateRequired("target", this.Target);
            RequestValidator.ValidateRequired("target_value", this.TargetValue);
            RequestValidator.ValidateRequired("title", this.Title);
        }

        #endregion
    }
}
