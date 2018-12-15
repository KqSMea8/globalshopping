using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalShopping.Core
{
    /// <summary>
    /// 商品状态
    /// </summary>
    public enum StatusCode
    {
        //下架
        Instock = 30001,
        //侵权
        Tort = 30002,

        Error = 500,//API获取错误

    }
}
