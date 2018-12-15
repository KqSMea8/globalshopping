USE [Pro]
GO

/****** Object:  Table [dbo].[PaymentBillDetail]    Script Date: 2018-12-08 21:17:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PaymentBillDetail](
	[PaymentBillDetailId] [int] IDENTITY(1,1) NOT NULL,
	[PaymentBillId] [int] NOT NULL,
	[BillCategoryId] [int] NOT NULL,
	[BillCategoryName] [nvarchar](100) NOT NULL,
	[AltBillCategoryName] [nvarchar](100) NOT NULL,
	[Total] [money] NOT NULL,
	[CreateBy] [nvarchar](50) NOT NULL,
	[UpdateBy] [nvarchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[IsAfterArrival] [bit] NULL,
 CONSTRAINT [PK_PaymentBillDetail] PRIMARY KEY CLUSTERED 
(
	[PaymentBillDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PaymentBillDetail] ADD  DEFAULT ('Admin') FOR [CreateBy]
GO

ALTER TABLE [dbo].[PaymentBillDetail] ADD  DEFAULT ('Admin') FOR [UpdateBy]
GO

ALTER TABLE [dbo].[PaymentBillDetail] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[PaymentBillDetail] ADD  DEFAULT (getdate()) FOR [UpdateDate]
GO


