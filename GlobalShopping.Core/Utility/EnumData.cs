
namespace GlobalShopping.Core.Utility
{
    public class EnumData
    {
        /// <summary>
        /// 针对淘宝的获取，三种获取方式
        /// </summary>
        public enum FetchBy
        {
            API, //从API读取
            HTML, //从淘宝商品详情页面获取
            Panli //从panli页面获取
        }
    }
}
