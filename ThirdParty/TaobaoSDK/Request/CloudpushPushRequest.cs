using System;
using System.Collections.Generic;
using Baichuan.Api.Response;
using Baichuan.Api.Util;

namespace Baichuan.Api.Request
{
    /// <summary>
    /// TOP API: taobao.cloudpush.push
    /// </summary>
    public class CloudpushPushRequest : BaseTopRequest<CloudpushPushResponse>
    {
        /// <summary>
        /// Android对应的activity,仅仅当androidOpenType=2有效
        /// </summary>
        public string AndroidActivity { get; set; }

        /// <summary>
        /// 自定义的kv结构,开发者扩展用 针对android
        /// </summary>
        public string AndroidExtParameters { get; set; }

        /// <summary>
        /// android通知声音
        /// </summary>
        public string AndroidMusic { get; set; }

        /// <summary>
        /// 点击通知后动作,1:打开应用 2: 打开应用Activity 3:打开 url
        /// </summary>
        public string AndroidOpenType { get; set; }

        /// <summary>
        /// Android收到推送后打开对应的url,仅仅当androidOpenType=3有效
        /// </summary>
        public string AndroidOpenUrl { get; set; }

        /// <summary>
        /// 防打扰时长,取值范围为1~23
        /// </summary>
        public Nullable<long> AntiHarassDuration { get; set; }

        /// <summary>
        /// 防打扰开始时间点,取值范围为0~23
        /// </summary>
        public Nullable<long> AntiHarassStartTime { get; set; }

        /// <summary>
        /// 批次编号,用于活动效果统计
        /// </summary>
        public string BatchNumber { get; set; }

        /// <summary>
        /// 推送内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 设备类型,取值范围为:0~3云推送支持多种设备,各种设备类型编号如下:    iOS设备:deviceType=0; Andriod设备:deviceType=1;如果存在此字段,则向指定的设备类型推送消息。 默认为全部(3);
        /// </summary>
        public Nullable<long> DeviceType { get; set; }

        /// <summary>
        /// iOS应用图标右上角角标
        /// </summary>
        public string IosBadge { get; set; }

        /// <summary>
        /// 自定义的kv结构,开发者扩展用 针对iOS设备
        /// </summary>
        public string IosExtParameters { get; set; }

        /// <summary>
        /// iOS通知声音
        /// </summary>
        public string IosMusic { get; set; }

        /// <summary>
        /// 当APP不在线时候，是否通过通知提醒
        /// </summary>
        public Nullable<bool> Remind { get; set; }

        /// <summary>
        /// 离线消息是否保存,若保存, 在推送时候，用户即使不在线，下一次上线则会收到
        /// </summary>
        public Nullable<bool> StoreOffline { get; set; }

        /// <summary>
        /// 通知的摘要
        /// </summary>
        public string Summery { get; set; }

        /// <summary>
        /// 推送目标: device:推送给设备; account:推送给指定帐号,tag:推送给自定义标签; all: 推送给全部
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// 根据Target来设定，如Target=device, 则对应的值为 设备id1,设备id2. 多个值使用逗号分隔.(帐号与设备有一次最多100个的限制)
        /// </summary>
        public string TargetValue { get; set; }

        /// <summary>
        /// 离线消息保存时长,取值范围为1~72,若不填,则表示不保存离线消息
        /// </summary>
        public Nullable<long> Timeout { get; set; }

        /// <summary>
        /// 推送的标题内容.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 0:表示消息(默认为0),1:表示通知
        /// </summary>
        public Nullable<long> Type { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.cloudpush.push";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("android_activity", this.AndroidActivity);
            parameters.Add("android_ext_parameters", this.AndroidExtParameters);
            parameters.Add("android_music", this.AndroidMusic);
            parameters.Add("android_open_type", this.AndroidOpenType);
            parameters.Add("android_open_url", this.AndroidOpenUrl);
            parameters.Add("anti_harass_duration", this.AntiHarassDuration);
            parameters.Add("anti_harass_start_time", this.AntiHarassStartTime);
            parameters.Add("batch_number", this.BatchNumber);
            parameters.Add("body", this.Body);
            parameters.Add("device_type", this.DeviceType);
            parameters.Add("ios_badge", this.IosBadge);
            parameters.Add("ios_ext_parameters", this.IosExtParameters);
            parameters.Add("ios_music", this.IosMusic);
            parameters.Add("remind", this.Remind);
            parameters.Add("store_offline", this.StoreOffline);
            parameters.Add("summery", this.Summery);
            parameters.Add("target", this.Target);
            parameters.Add("target_value", this.TargetValue);
            parameters.Add("timeout", this.Timeout);
            parameters.Add("title", this.Title);
            parameters.Add("type", this.Type);
            if (this.OtherParams != null)
            {
                parameters.AddAll(this.OtherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("body", this.Body);
            RequestValidator.ValidateRequired("device_type", this.DeviceType);
            RequestValidator.ValidateRequired("remind", this.Remind);
            RequestValidator.ValidateRequired("store_offline", this.StoreOffline);
            RequestValidator.ValidateRequired("target", this.Target);
            RequestValidator.ValidateRequired("target_value", this.TargetValue);
            RequestValidator.ValidateRequired("title", this.Title);
        }

        #endregion
    }
}
