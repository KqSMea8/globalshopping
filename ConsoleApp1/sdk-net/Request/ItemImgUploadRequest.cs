using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.item.img.upload
    /// </summary>
    public class ItemImgUploadRequest : BaseTopRequest<ItemImgUploadResponse>, ITopUploadRequest<ItemImgUploadResponse>
    {
        /// <summary>
        /// 商品图片id(如果是更新图片，则需要传该参数)
        /// </summary>
        public Nullable<long> Id { get; set; }

        /// <summary>
        /// 商品图片内容类型:JPG,GIF;最大:3M 。支持的文件类型：gif,jpg,jpeg,png
        /// </summary>
        public FileItem Image { get; set; }

        /// <summary>
        /// 是否将该图片设为主图,可选值:true,false;默认值:false(非主图)
        /// </summary>
        public Nullable<bool> IsMajor { get; set; }

        /// <summary>
        /// 是否3:4长方形图片，绑定3:4主图视频时用于上传3:4商品主图
        /// </summary>
        public Nullable<bool> IsRectangle { get; set; }

        /// <summary>
        /// 商品数字ID，该参数必须
        /// </summary>
        public Nullable<long> NumIid { get; set; }

        /// <summary>
        /// 图片序号
        /// </summary>
        public Nullable<long> Position { get; set; }

        #region BaseTopRequest Members

        public override string GetApiName()
        {
            return "taobao.item.img.upload";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("id", this.Id);
            parameters.Add("is_major", this.IsMajor);
            parameters.Add("is_rectangle", this.IsRectangle);
            parameters.Add("num_iid", this.NumIid);
            parameters.Add("position", this.Position);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateMaxLength("image", this.Image, 3145728);
            RequestValidator.ValidateRequired("num_iid", this.NumIid);
            RequestValidator.ValidateMinValue("num_iid", this.NumIid, 0);
        }

        #endregion

        #region ITopUploadRequest Members

        public IDictionary<string, FileItem> GetFileParameters()
        {
            IDictionary<string, FileItem> parameters = new Dictionary<string, FileItem>();
            parameters.Add("image", this.Image);
            return parameters;
        }

        #endregion
    }
}
