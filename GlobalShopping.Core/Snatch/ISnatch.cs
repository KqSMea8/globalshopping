using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalShopping.Core.Snatch
{
    public interface ISnatch
    {
        string Domain { get;}

        string Site { get;  }

        /// <summary>
        /// 店铺拥有者匹配
        /// </summary>
        string[] VendorNamePatterns { get; }

        /// <summary>
        /// 商品名称匹配
        /// </summary>
        string[] ProductNamePatterns { get; }
        /// <summary>
        /// 价格匹配
        /// </summary>
        string[] UnitPricePatterns { get; }
        /// <summary>
        /// 运费匹配
        /// </summary>
        string[] ShipmentFeePatterns { get; }

        /// <summary>
        /// 图片匹配
        /// </summary>
        string[] ProductImagePatterns { get; }
        /// <summary>
        /// 获取店铺拥有者
        /// </summary>
        /// <param name="pageContent">内容</param>
        /// <returns>店铺拥有者</returns>
        string GetVendorName(string pageContent);
        /// <summary>
        /// 获取商品名称
        /// </summary>
        /// <param name="pageContent">内容</param>
        /// <returns>商品名称</returns>
        string GetProductName(string pageContent);
        /// <summary>
        /// 获取商品价格
        /// </summary>
        /// <param name="pageContent">内容</param>
        /// <returns>商品价格</returns>
        double? GetUnitPrice(string pageContent);
        /// <summary>
        /// 获取运费
        /// </summary>
        /// <param name="pageContent">内容</param>
        /// <returns>运费</returns>
        double? GetShipmentFee(string pageContent);
        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="pageContent">内容</param>
        /// <returns>图片信息</returns>
        string GetProductImage(string pageContent);

        /// <summary>
        /// 获取Url信息。
        /// </summary>
        /// <param name="source">源地址</param>
        /// <returns>url地址</returns>
        string GetUrl(string source);

        /// <summary>
        /// 根据productUrl与派送地址获取在线商品信息
        /// </summary>
        /// <param name="productUrl">商品Url</param>
        /// <param name="destination">目的地</param>
        /// <returns>在线商品信息</returns>
        void Snatch(string productUrl, string destination, bool needTranslate = false);

        /// <summary>
        /// 根据选择的商品Url、仓库获取国内运费
        /// </summary>
        /// <param name="productUrl">商品Url</param>
        /// <param name="wareHouse">仓库</param>
        /// <returns>运费</returns>
        double? GetShipmentByWareHouse(string productUrl, string wareHouse);
        /// <summary>
        /// 根据商品的Url 获取商品的采购地
        /// </summary>
        /// <param name="productUrl"></param>
        /// <returns>采购地区</returns>
        string GetOriginCodeByUrl(string productUrl);
    }
}
