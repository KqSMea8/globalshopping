using System;
//using Nest;
using System.Collections.Generic;

namespace GlobalShopping.Core.Model
{
    //[ElasticType(IdProperty = "ProductKey")]
    //public class ProductEntity
    //{
    //    public string ProductKey
    //    {
    //        get { return this.RefId; }
    //    }
    //    public long TopProductId { get; set; }
    //    public int SellerProductId { get; set; }

    //    [ElasticProperty(OmitNorms = true, Index = FieldIndexOption.NotAnalyzed)]
    //    public string VendorName { get; set; }
    //    public int IsPartnerShopProduct { get; set; } //0表示非推荐卖家商品，1表示是推荐卖家商品
    //    public string ProductName { get; set; }

    //    [ElasticProperty(OmitNorms = true, Index = FieldIndexOption.NotAnalyzed)]
    //    public string ProductUrl { get; set; }
    //    public string ProductImage { get; set; }
    //    public decimal EstWeight { get; set; }
    //    public decimal EstVolumeWeight { get; set; }
    //    [ElasticProperty(OmitNorms = true, Index = FieldIndexOption.NotAnalyzed)]
    //    public string OriginCode { get; set; }

    //    public string CatalogCode { get; set; }
    //    public List<int> ProductCategoryIds { get; set; }
    //    public int BuyCount { get; set; }

    //    [ElasticProperty(OmitNorms = true, Index = FieldIndexOption.NotAnalyzed)]
    //    public string StateCode { get; set; }

    //    public decimal UnitPrice { get; set; }
    //    public decimal PriceSG { get; set; }
    //    public decimal PriceMY { get; set; }
    //    public decimal PriceAU { get; set; }
    //    public decimal PriceID { get; set; }
    //    public decimal PriceTH { get; set; }
        
    //    public decimal OriPriceSG { get; set; }
    //    public decimal OriPriceMY { get; set; }
    //    public decimal OriPriceAU { get; set; }
    //    public decimal OriPriceID { get; set; }
    //    public decimal OriPriceTH { get; set; }

    //    [ElasticProperty(OmitNorms = true, Index = FieldIndexOption.NotAnalyzed)]
    //    public string AvailableShipmentTypeIds { get; set; }

    //    [ElasticProperty(OmitNorms = true, Index = FieldIndexOption.NotAnalyzed)]
    //    public string DomesticShippingEta { get; set; }

    //    [ElasticProperty(OmitNorms = true, Index = FieldIndexOption.NotAnalyzed)]
    //    public string RefId { get; set; }
    //    public bool IsOnSale { get; set; }
    //    public bool IsEzShipping { get; set; }

    //    [ElasticProperty(OmitNorms = true, Index = FieldIndexOption.NotAnalyzed)]
    //    public string Source { get; set; } //做为TbCategoryName给GA统计使用
    //    public string AltProductName { get; set; }
    //    public string ProductDescription { get; set; }

    //    [ElasticProperty(OmitNorms = true, Index = FieldIndexOption.NotAnalyzed)]
    //    public string ProductImages { get; set; }
    //    public long Cid { get; set; }
    //    public int SellerId { get; set; }
    //    public DateTime FlagDate { get; set; }
    //    public bool IsCashOff { get; set; }
    //    public string CreateBy { get; set; }
    //    public DateTime CreateDate { get; set; }
    //    public string UpdateBy { get; set; }
    //    public DateTime UpdateDate { get; set; }
    //    public decimal? Weight { get; set; }//SellerProduct.Weight

    //    [ElasticProperty(OmitNorms = true, Index = FieldIndexOption.NotAnalyzed)]
    //    public double DiscountValue { get; set; }
    //    public decimal RebateDiscount { get; set; }
    //    public bool IsPrime { get; set; }
    //    public int PrimeShippingType { get; set; } //1表示经济空运，2表示是海运
    //    [ElasticProperty(OmitNorms = true)]
    //    public Dictionary<string,double> InternalShippingFees { get; set; }

    //    [ElasticProperty(OmitNorms = true)]
    //    public double FlashSalesPrice { get; set; }

    //    [ElasticProperty(OmitNorms = true)]
    //    public string FlashSalesStartTime { get; set; }

    //    [ElasticProperty(OmitNorms = true)]
    //    public string FlashSalesEndTime { get; set; }

    //    [ElasticProperty(OmitNorms = true)]
    //    public int FlashSalesLimitation { get; set; }

    //    [ElasticProperty(OmitNorms = true)]
    //    public int FlashSalesStock { get; set; }

    //    [ElasticProperty(OmitNorms = true)]
    //    public string InternalProductUrl { get; set; }

    //    [ElasticProperty(OmitNorms = true)]
    //    public double OriUnitPrice { get; set; }

    //    [ElasticProperty(OmitNorms = true, Index = FieldIndexOption.NotAnalyzed)]
    //    public bool IsBlockSG { get; set; }

    //    [ElasticProperty(OmitNorms = true, Index = FieldIndexOption.NotAnalyzed)]
    //    public bool IsBlockMY { get; set; }

    //    [ElasticProperty(OmitNorms = true, Index = FieldIndexOption.NotAnalyzed)]
    //    public bool IsBlockAU { get; set; }

    //    [ElasticProperty(OmitNorms = true, Index = FieldIndexOption.NotAnalyzed)]
    //    public bool IsBlockID { get; set; }

    //    [ElasticProperty(OmitNorms = true, Index = FieldIndexOption.NotAnalyzed)]
    //    public bool IsBlockTH { get; set; }

    //    [ElasticProperty(OmitNorms = true, Index = FieldIndexOption.NotAnalyzed)]
    //    public bool IsBlockDetailSG { get; set; }

    //    [ElasticProperty(OmitNorms = true, Index = FieldIndexOption.NotAnalyzed)]
    //    public bool IsBlockDetailMY { get; set; }

    //    [ElasticProperty(OmitNorms = true, Index = FieldIndexOption.NotAnalyzed)]
    //    public bool IsBlockDetailAU { get; set; }

    //    [ElasticProperty(OmitNorms = true, Index = FieldIndexOption.NotAnalyzed)]
    //    public bool IsBlockDetailID { get; set; }

    //    [ElasticProperty(OmitNorms = true, Index = FieldIndexOption.NotAnalyzed)]
    //    public bool IsBlockDetailTH { get; set; }

    //    [ElasticProperty(OmitNorms = true)]
    //    public long EzbuyShipmentInfo { get; set; }

    //    [ElasticProperty(OmitNorms = true)]
    //    public string CashOffKey { get; set; }

    //    [ElasticProperty(OmitNorms = true, Index = FieldIndexOption.NotAnalyzed)]
    //    public bool IsBlock { get; set; }

    //    [ElasticProperty(OmitNorms = true, Index = FieldIndexOption.NotAnalyzed)]
    //    public bool IsBlockDetail { get; set; }

    //    public bool IsEzbuy { get; set; }

    //    [ElasticProperty(OmitNorms = true, Index = FieldIndexOption.NotAnalyzed)]
    //    public ProductEntityFlashSales FlashSales { get; set; }

    //    [ElasticProperty(OmitNorms = true, Index = FieldIndexOption.NotAnalyzed)]
    //    public ProductEntityCashOff Cashoff { get; set; }

    //    [ElasticProperty(OmitNorms = true, Index = FieldIndexOption.NotAnalyzed)]
    //    public Dictionary<string,string> ProductNames { get; set; }

    //    [ElasticProperty(OmitNorms = true, Index = FieldIndexOption.NotAnalyzed)]
    //    public string CashoffEnd { get; set; }

    //    [ElasticProperty(OmitNorms = true, Index = FieldIndexOption.NotAnalyzed)]
    //    public string CashoffStart { get; set; }

    //    public long Gpid { get; set; }
        
    //}

    public enum PrimeShippingTypes
    {
        EconomicAir = 1,
        Sea = 2
    }

    public class ProductEntityFlashSales
    {
        public double Price { get; set; }
        public Time Start { get; set; }
        public Time End { get; set; }
        public int Limitation { get; set; }
        public int Stock { get; set; }
    }

    public class ProductEntityCashOff
    {
        public string Key { get; set; }
        public Time Start { get; set; }
        public Time Expire { get; set; }
    }

    public class Time
    {
        public long Seconds { get; set; }
        public long Nanos { get; set; }
    }
}
