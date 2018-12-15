USE [Pro]
GO

/****** Object:  Table [dbo].[PrePayment]    Script Date: 2018-12-08 21:29:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PrePayment](
	[PrePaymentId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[Total] [money] NOT NULL,
	[Notes] [nvarchar](1000) NULL,
	[CreateBy] [nvarchar](50) NOT NULL,
	[UpdateBy] [nvarchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[OrderNumber] [nvarchar](100) NULL,
	[PaymentNumber] [nvarchar](100) NULL,
	[TopUpNumber] [nvarchar](100) NULL,
	[WithdrawNumber] [nvarchar](100) NULL,
	[PrePay] [money] NULL,
	[PrePaymentType] [nvarchar](50) NULL,
	[PrePaymentReason] [nvarchar](100) NULL,
	[OriginCode] [nvarchar](10) NULL,
	[PackageNumber] [nvarchar](20) NULL,
	[PurchaseType] [nvarchar](50) NULL,
	[PlatformId] [int] NULL,
	[BillCategoryId] [int] NULL,
 CONSTRAINT [PK_PrePayment] PRIMARY KEY CLUSTERED 
(
	[PrePaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PrePayment] ADD  DEFAULT ('Admin') FOR [CreateBy]
GO

ALTER TABLE [dbo].[PrePayment] ADD  DEFAULT ('Admin') FOR [UpdateBy]
GO

ALTER TABLE [dbo].[PrePayment] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[PrePayment] ADD  DEFAULT (getdate()) FOR [UpdateDate]
GO

ALTER TABLE [dbo].[PrePayment] ADD  DEFAULT (N'CN') FOR [OriginCode]
GO


