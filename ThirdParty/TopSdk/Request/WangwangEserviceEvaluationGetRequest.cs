using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.wangwang.eservice.evaluation.get
    /// </summary>
    public class WangwangEserviceEvaluationGetRequest : BaseTopRequest<Top.Api.Response.WangwangEserviceEvaluationGetResponse>
    {
        /// <summary>
        /// 查询结束日期
        /// </summary>
        public Nullable<DateTime> EndDate { get; set; }

        /// <summary>
        /// 客服人员id：cntaobao+淘宝nick，例如cntaobaotest
        /// </summary>
        public string ServiceStaffId { get; set; }

        /// <summary>
        /// 查询开始日期
        /// </summary>
        public Nullable<DateTime> StartDate { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.wangwang.eservice.evaluation.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("end_date", this.EndDate);
            parameters.Add("service_staff_id", this.ServiceStaffId);
            parameters.Add("start_date", this.StartDate);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("end_date", this.EndDate);
            RequestValidator.ValidateRequired("service_staff_id", this.ServiceStaffId);
            RequestValidator.ValidateMaxLength("service_staff_id", this.ServiceStaffId, 1900);
            RequestValidator.ValidateRequired("start_date", this.StartDate);
        }

        #endregion
    }
}
