using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.picture.userinfo.get
    /// </summary>
    public class PictureUserinfoGetRequest : BaseTopRequest<Top.Api.Response.PictureUserinfoGetResponse>
    {
        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.picture.userinfo.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
        }

        #endregion
    }
}
