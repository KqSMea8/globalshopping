USE [Pro]
GO

/****** Object:  Table [dbo].[Order]    Script Date: 2018-12-08 21:10:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Product](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](100) NULL,
	[ProductUrl] [nvarchar](1000) NULL,
	[VendorName] [nvarchar](1000) NULL,
	[OriginCode] [nvarchar](10) NULL,
	[Weight] [bigint] NULL,
	[VolumeWeight] [bigint] NULL,
	[ProductImage] [nvarchar](500) NULL,
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
	
	CONSTRAINT PK__[Product]__Id PRIMARY KEY (Id)
)

GO

