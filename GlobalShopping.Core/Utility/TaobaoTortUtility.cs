using System.Collections.Generic;
using System.Linq;

namespace GlobalShopping.Core.Utility
{
    /// <summary>
    ///     淘宝侵权商品处理
    /// </summary>
    public class TaobaoTortUtility
    {
        static TaobaoTortUtility()
        {
            Torts = new List<string>
            {
                "10759296846",
                "36358901361",
                "35326659205",
                "24808264928",
                "36979280261",
                "36528000285",
                "21191463556",
                "35289076013",
                "14305296698",
                "38480238595",
                "37737836066",
                "37550121546",
                "19784929360",
                "16096834182",
                "14652911291",
                "18771327895",
                "39194747344",
                "22959468483",
                "18372654716",
                "39168526207",
                "38516750857",
                "38672353658",
                "19553633868",
                "38227857454",
                "39366492417",
                "37127385657",
                "37894472551",
                "37678601925",
                "37508052007"
            };
        }

        /// <summary>
        ///     侵权商品集合
        /// </summary>
        public static List<string> Torts { get; set; }

        /// <summary>
        /// 判断某个商品是否侵权
        /// </summary>
        /// <param name="key">商品主键</param>
        /// <returns></returns>
        public static bool IsTort(string key)
        {
            return Torts.Any(p => p == key);
        }
    }
}