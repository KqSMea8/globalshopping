CREATE TABLE [dbo].[Order](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderNumber] [nvarchar](100) NOT NULL,
	[CustomerId] [long] NOT NULL,
	[OrderStatusId] [bigint] NOT NULL,
	[ProductFee]] [money] NULL,
	[ShipmentFee] [money] NULL,
	[ExpressFee] [money] NULL,
	[PriceSymbol] [nvarchar](10) NULL,
	[CatalogCode] [nvarchar](10) NULL,
	[CustomerEstWeight] [bigint] NULL,
	[CustomerEstVolumeWeight] [bigint] NULL,
	[CreateBy] [nvarchar](50) NOT NULL,
	[UpdateBy] [nvarchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,

	CONSTRAINT PK__[Order]__Id PRIMARY KEY (Id),
	CONSTRAINT UK__[Order]__OrderNumber UNIQUE (OrderNumber)
)

GO

