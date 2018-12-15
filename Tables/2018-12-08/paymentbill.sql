USE [Pro]
GO

/****** Object:  Table [dbo].[PaymentBill]    Script Date: 2018-12-08 21:14:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[PaymentBill](
	[PaymentBillId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[PaymentNumber] [nvarchar](40) NOT NULL,
	[ShipmentId] [int] NULL,
	[IsPayed] [bit] NOT NULL,
	[IsCancelled] [bit] NOT NULL,
	[PayDate] [datetime] NULL,
	[PaymentType] [nvarchar](200) NOT NULL,
	[Total] [money] NOT NULL,
	[Exchange] [decimal](16, 8) NULL,
	[SerialNumber] [nvarchar](200) NULL,
	[BankName] [nvarchar](200) NULL,
	[PaymentMethod] [nvarchar](200) NULL,
	[PaypalFee] [money] NULL,
	[PrePay] [money] NULL,
	[ActualPay] [money] NULL,
	[AuthorizeForBalance] [bit] NULL,
	[AuthorizeBalance] [money] NULL,
	[TopUpId] [int] NULL,
	[OriginCode] [nvarchar](20) NULL,
	[RebateAmount] [money] NOT NULL,
	[PlatformId] [int] NULL,
	[CreateBy] [nvarchar](50) NOT NULL,
	[UpdateBy] [nvarchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[CreditCardCouponCode] [varchar](100) NULL,
	[PayDueDate] [datetime] NULL,
	[IsEzShipping] [bit] NOT NULL,
	[PrimeMembershipType] [int] NULL,
	[OriginTotal] [money] NULL,
	[IsFromBuyForMeSgNewFlow] [bit] NULL,
 CONSTRAINT [PK_PaymentBill] PRIMARY KEY CLUSTERED 
(
	[PaymentBillId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[PaymentBill] ADD  DEFAULT ((1)) FOR [IsPayed]
GO

ALTER TABLE [dbo].[PaymentBill] ADD  DEFAULT ((0)) FOR [IsCancelled]
GO

ALTER TABLE [dbo].[PaymentBill] ADD  DEFAULT ((0)) FOR [RebateAmount]
GO

ALTER TABLE [dbo].[PaymentBill] ADD  DEFAULT ('Admin') FOR [CreateBy]
GO

ALTER TABLE [dbo].[PaymentBill] ADD  DEFAULT ('Admin') FOR [UpdateBy]
GO

ALTER TABLE [dbo].[PaymentBill] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[PaymentBill] ADD  DEFAULT (getdate()) FOR [UpdateDate]
GO

ALTER TABLE [dbo].[PaymentBill] ADD  DEFAULT ((0)) FOR [IsEzShipping]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'付款成功后的时间，关联充值成功后会赋值，创建订单【WWWCreateOrder】时，账单的此值为被设置为Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PaymentBill', @level2type=N'COLUMN',@level2name=N'PayDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信用卡折扣码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PaymentBill', @level2type=N'COLUMN',@level2name=N'CreditCardCouponCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'该账单的Prime会员缴交类型【1：月会员，2：年会员，3：续费年会员】' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PaymentBill', @level2type=N'COLUMN',@level2name=N'PrimeMembershipType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否来自于新加坡代购新流程' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PaymentBill', @level2type=N'COLUMN',@level2name=N'IsFromBuyForMeSgNewFlow'
GO


