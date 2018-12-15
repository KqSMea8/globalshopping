using System;
using System.Collections.Generic;

namespace GlobalShopping.Core
{
    public class GoSpiderData
    {
        public string ItemId { get; set; }
        public bool IsPreSale { get; set; }
        public long FetchTime { get; set; }
        public Dictionary<string, SkuPrices> Prices { get; set; }

    }

    public class SkuPrices
    {
        public string Price { get; set; }
        public string PromotionPrice { get; set; }
        public string Double11Price { get; set; }

        public double GetDayPrice()
        {
            double price = 0;
            DateTime dt = DateTime.Now;
            if (dt.Day == 11 && dt.Month == 11)
            {
                if (!string.IsNullOrEmpty(Double11Price))
                {
                    if (double.TryParse(Double11Price, out price))
                    {
                        return price;
                    }
                }
            }

            if (!string.IsNullOrEmpty(PromotionPrice))
            {
                if (double.TryParse(PromotionPrice, out price))
                {
                    return price;
                }
            }

            if (!string.IsNullOrEmpty(Price))
            {
                if (double.TryParse(Price, out price))
                {
                    return price;
                }
            }

            return price;
        }
    }
}
