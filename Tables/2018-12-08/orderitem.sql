CREATE TABLE [dbo].[OrderItem](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderId] [bigint] NOT NULL,
	[OrderItemId] [bigint] NOT NULL,
	[ProductId] [bigint] NULL,
	[ProductName] [nvarchar](100) NULL,
	[ProductUrl] [nvarchar](1000) NULL,
	[VendorName] [nvarchar](1000) NULL,
	[OriginCode] [nvarchar](10) NULL,
	[OriginUnitPrice] [money] NULL,
	[OriginPriceSymble] [nvarchar](10) NULL,
	[Qty] [bigint] NULL,
	[OriginProductFee]] [money] NULL,
	[OriginShipmentFee]] [money] NULL,
	[ProductFee]] [money] NULL,
	[ShipmentFee] [money] NULL,
	[ExpressFee] [money] NULL,
	[Currency] [money] NULL,
	[CustomerEstWeight] [bigint] NULL,
	[CustomerEstVolumeWeight] [bigint] NULL,
	[ProductImage] [nvarchar](500) NULL,
	[ProductRemark] [nvarchar](2000) NULL,
	[TbProductId] [bigint] NULL,
	[ShopName] [nvarchar](100) NULL,
	[PlatformId] [bigint] NULL,
	[Sku] [nvarchar](1000) NULL,
	[SkuPrice] [money] NOT NULL,
	[SkuProperties] [nvarchar](1000) NULL,
	[SkuId] [bigint] NULL,
	[SkuImg] [nvarchar](1000) NULL,
	[TbCid] [bigint] NULL,

	[CreateBy] [nvarchar](50) NOT NULL,
	[UpdateBy] [nvarchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	
	CONSTRAINT PK__[OrderItem]__Id PRIMARY KEY (Id)
	CONSTRAINT UK__[OrderItem]__OrderIdItemId UNIQUE (OrderId,OrderItemId)
 )

GO

