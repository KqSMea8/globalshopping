USE [Pro]
GO

/****** Object:  Table [dbo].[CustomerAddress]    Script Date: 2018-12-08 21:26:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CustomerAddress](
	[CustomerAddressId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[ZipCode] [nvarchar](20) NOT NULL,
	[Address] [nvarchar](1000) NULL,
	[IsMajor] [bit] NOT NULL,
	[AddressToName] [nvarchar](100) NOT NULL,
	[AddressToPhone] [nvarchar](50) NULL,
	[CreateBy] [nvarchar](50) NOT NULL,
	[UpdateBy] [nvarchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[ShipToAddress1] [nvarchar](300) NULL,
	[ShipToAddress2] [nvarchar](300) NULL,
	[ShipToCity] [nvarchar](100) NULL,
	[ShipToState] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[BuildingType] [nvarchar](128) NULL,
	[Block] [nvarchar](128) NULL,
	[Street] [nvarchar](128) NULL,
	[UnitStart] [nvarchar](128) NULL,
	[UnitEnd] [nvarchar](128) NULL,
	[BuildingName] [nvarchar](128) NULL,
	[DestCode] [nvarchar](20) NULL,
	[PreferedDeliveryTime] [nvarchar](100) NULL,
	[Company] [nvarchar](1000) NULL,
	[Remark] [nvarchar](1000) NULL,
	[SubDistrict] [nvarchar](100) NULL,
	[District] [nvarchar](100) NULL,
 CONSTRAINT [PK_CustomerAddress] PRIMARY KEY CLUSTERED 
(
	[CustomerAddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CustomerAddress] ADD  DEFAULT ('Admin') FOR [CreateBy]
GO

ALTER TABLE [dbo].[CustomerAddress] ADD  DEFAULT ('Admin') FOR [UpdateBy]
GO

ALTER TABLE [dbo].[CustomerAddress] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[CustomerAddress] ADD  DEFAULT (getdate()) FOR [UpdateDate]
GO

ALTER TABLE [dbo].[CustomerAddress] ADD  DEFAULT ((1)) FOR [IsActive]
GO


