USE [master]
GO
/****** Object:  Database [AccountDb]    Script Date: 16-11-2019 16:28:30 ******/
CREATE DATABASE [AccountDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AccountDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLSERVER2017\MSSQL\DATA\AccountDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AccountDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLSERVER2017\MSSQL\DATA\AccountDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [AccountDb] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AccountDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AccountDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AccountDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AccountDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AccountDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AccountDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [AccountDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AccountDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AccountDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AccountDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AccountDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AccountDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AccountDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AccountDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AccountDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AccountDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AccountDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AccountDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AccountDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AccountDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AccountDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AccountDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AccountDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AccountDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AccountDb] SET  MULTI_USER 
GO
ALTER DATABASE [AccountDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AccountDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AccountDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AccountDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AccountDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AccountDb] SET QUERY_STORE = OFF
GO
USE [AccountDb]
GO
/****** Object:  Table [dbo].[CommissionAgentExpenses]    Script Date: 16-11-2019 16:28:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommissionAgentExpenses](
	[CommissionAgentExpensesId] [bigint] IDENTITY(1,1) NOT NULL,
	[ExpenseId] [bigint] NULL,
	[Amount] [bigint] NULL,
	[createdBy] [nvarchar](500) NULL,
	[modifiedDate] [datetime] NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[modifiedBy] [nvarchar](500) NULL,
 CONSTRAINT [PK_CommisionAgentExpenses] PRIMARY KEY CLUSTERED 
(
	[CommissionAgentExpensesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CommissionAgentPurchases]    Script Date: 16-11-2019 16:28:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommissionAgentPurchases](
	[CommisionAgentPurchasesId] [bigint] IDENTITY(1,1) NOT NULL,
	[VendorId] [bigint] NULL,
	[PurchasePrice] [bigint] NULL,
	[Quantity] [bigint] NULL,
	[Total] [bigint] NULL,
	[createdDate] [datetime] NOT NULL,
	[modifiedDate] [datetime] NOT NULL,
	[createdBy] [nvarchar](50) NULL,
	[modifiedBy] [nvarchar](50) NULL,
	[LoggedInUser] [nvarchar](500) NULL,
 CONSTRAINT [PK_CommissionAgentPurchases] PRIMARY KEY CLUSTERED 
(
	[CommisionAgentPurchasesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CommissionAgentSales]    Script Date: 16-11-2019 16:28:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommissionAgentSales](
	[CommissionAgentSalesId] [bigint] IDENTITY(1,1) NOT NULL,
	[VendorId] [bigint] NULL,
	[PurchasePrice] [bigint] NULL,
	[Quantity] [bigint] NULL,
	[Total] [bigint] NULL,
	[createdDate] [datetime] NOT NULL,
	[modifiedDate] [datetime] NOT NULL,
	[createdBy] [nvarchar](50) NULL,
	[modifiedBy] [nvarchar](50) NULL,
	[LoggedInUser] [nvarchar](500) NULL,
 CONSTRAINT [PK_CommissionAgentSales] PRIMARY KEY CLUSTERED 
(
	[CommissionAgentSalesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CommissionEarned]    Script Date: 16-11-2019 16:28:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommissionEarned](
	[CommissionEarnedId] [bigint] IDENTITY(1,1) NOT NULL,
	[StockInId] [bigint] NULL,
	[Amount] [bigint] NULL,
	[createdBy] [nvarchar](500) NULL,
	[modifiedDate] [datetime] NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[modifiedBy] [nvarchar](500) NULL,
	[CommissionPercentageId] [bigint] NULL,
 CONSTRAINT [PK_CommissionEarned] PRIMARY KEY CLUSTERED 
(
	[CommissionEarnedId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CommissionPercentage]    Script Date: 16-11-2019 16:28:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommissionPercentage](
	[CommissionPercentageId] [bigint] IDENTITY(1,1) NOT NULL,
	[Percentage] [float] NULL,
 CONSTRAINT [PK_CommissionPercentage] PRIMARY KEY CLUSTERED 
(
	[CommissionPercentageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 16-11-2019 16:28:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustId] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[MiddleName] [nvarchar](max) NULL,
	[nickName] [nvarchar](max) NULL,
	[lastName] [nvarchar](max) NULL,
	[mobile] [nvarchar](50) NULL,
	[referredBy] [nvarchar](max) NULL,
	[createdBy] [nvarchar](500) NULL,
	[modifiedDate] [datetime] NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[modifiedBy] [nvarchar](500) NULL,
	[url] [nvarchar](500) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerDetails]    Script Date: 16-11-2019 16:28:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerDetails](
	[CustAddressId] [int] IDENTITY(1,1) NOT NULL,
	[CustId] [bigint] NULL,
	[CustAddress] [nvarchar](max) NULL,
	[alternateMobile] [nvarchar](50) NULL,
	[homePhone] [nvarchar](50) NULL,
	[officePhone] [nvarchar](50) NULL,
	[email] [nvarchar](50) NULL,
	[address] [nvarchar](max) NULL,
	[city] [nvarchar](max) NULL,
	[state] [nvarchar](max) NULL,
	[shopName] [nvarchar](max) NULL,
	[shopLocation] [nvarchar](max) NULL,
 CONSTRAINT [PK_CustomerAddress] PRIMARY KEY CLUSTERED 
(
	[CustAddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerPayments]    Script Date: 16-11-2019 16:28:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerPayments](
	[CustomerPaymentId] [bigint] IDENTITY(1,1) NOT NULL,
	[CustId] [bigint] NULL,
	[AmountPaid] [bigint] NULL,
	[createdDate] [datetime] NOT NULL,
	[modifiedDate] [datetime] NOT NULL,
	[createdBy] [nvarchar](50) NULL,
	[modifiedBy] [nvarchar](50) NULL,
	[LoggedInUser] [nvarchar](500) NULL,
 CONSTRAINT [PK_CustomerPayments] PRIMARY KEY CLUSTERED 
(
	[CustomerPaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Expenses]    Script Date: 16-11-2019 16:28:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Expenses](
	[ExpenseId] [bigint] IDENTITY(1,1) NOT NULL,
	[ExpenseName] [nvarchar](max) NULL,
	[ExpenseTypeId] [bigint] NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[ExpenseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExpenseTypes]    Script Date: 16-11-2019 16:28:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExpenseTypes](
	[ExpenseTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[ExpenseTypeName] [nvarchar](max) NULL,
 CONSTRAINT [PK_ExpenseTypes] PRIMARY KEY CLUSTERED 
(
	[ExpenseTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sales]    Script Date: 16-11-2019 16:28:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales](
	[SalesId] [bigint] IDENTITY(1,1) NOT NULL,
	[StockInId] [bigint] NULL,
	[CustId] [bigint] NULL,
	[ProductType] [int] NULL,
	[Quantity] [int] NOT NULL,
	[Price] [int] NOT NULL,
	[Total] [int] NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[modifiedDate] [datetime] NOT NULL,
	[createdBy] [nvarchar](50) NULL,
	[modifiedBy] [nvarchar](50) NULL,
	[LoggedInUser] [nvarchar](500) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Sales] PRIMARY KEY CLUSTERED 
(
	[SalesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesReturns]    Script Date: 16-11-2019 16:28:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesReturns](
	[SalesReturnId] [bigint] IDENTITY(1,1) NOT NULL,
	[StockInId] [bigint] NULL,
	[CustId] [bigint] NULL,
	[ProductType] [int] NULL,
	[Quantity] [bigint] NULL,
	[Price] [bigint] NULL,
	[Total] [bigint] NULL,
	[createdDate] [datetime] NOT NULL,
	[modifiedDate] [datetime] NOT NULL,
	[createdBy] [nvarchar](50) NULL,
	[modifiedBy] [nvarchar](50) NULL,
	[LoggedInUser] [nvarchar](500) NULL,
 CONSTRAINT [PK_SalesReturns] PRIMARY KEY CLUSTERED 
(
	[SalesReturnId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockIn]    Script Date: 16-11-2019 16:28:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockIn](
	[StockID] [bigint] IDENTITY(1,1) NOT NULL,
	[VendorId] [bigint] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[LoadName] [nvarchar](max) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[LastModifiedDate] [datetime] NULL,
	[LoginID] [nvarchar](max) NULL,
	[isActive] [bit] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_StockIn] PRIMARY KEY CLUSTERED 
(
	[StockID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vendor]    Script Date: 16-11-2019 16:28:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vendor](
	[VendorId] [bigint] IDENTITY(1,1) NOT NULL,
	[firstName] [nvarchar](500) NULL,
	[middleName] [nvarchar](500) NULL,
	[lastName] [nvarchar](500) NULL,
	[mobileNo] [nvarchar](500) NULL,
	[createdDate] [datetime] NOT NULL,
	[modifiedDate] [datetime] NOT NULL,
	[createdBy] [nvarchar](50) NULL,
	[modifiedBy] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[nickName] [nvarchar](500) NULL,
	[referredBy] [nvarchar](500) NULL,
 CONSTRAINT [PK_VendorDetails] PRIMARY KEY CLUSTERED 
(
	[VendorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VendorAdvances]    Script Date: 16-11-2019 16:28:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VendorAdvances](
	[AdvancesId] [bigint] IDENTITY(1,1) NOT NULL,
	[VendorId] [bigint] NULL,
	[Amount] [bigint] NULL,
	[createdDate] [datetime] NOT NULL,
	[modifiedDate] [datetime] NOT NULL,
	[createdBy] [nvarchar](50) NULL,
	[modifiedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_VendorAdvances] PRIMARY KEY CLUSTERED 
(
	[AdvancesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VendorAdvancesReturned]    Script Date: 16-11-2019 16:28:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VendorAdvancesReturned](
	[VendorAdvancesReturnedId] [bigint] IDENTITY(1,1) NOT NULL,
	[VendorId] [bigint] NULL,
	[Amount] [bigint] NULL,
	[createdDate] [datetime] NOT NULL,
	[modifiedDate] [datetime] NOT NULL,
	[createdBy] [nvarchar](50) NULL,
	[modifiedBy] [nvarchar](50) NULL,
	[LoggedInUser] [nvarchar](500) NULL,
 CONSTRAINT [PK_VendorAdvancesReturned] PRIMARY KEY CLUSTERED 
(
	[VendorAdvancesReturnedId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VendorDetails]    Script Date: 16-11-2019 16:28:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VendorDetails](
	[VendorDetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[VendorId] [bigint] NULL,
	[homePhone] [nvarchar](500) NULL,
	[address] [nvarchar](max) NULL,
	[city] [nvarchar](500) NULL,
	[state] [nvarchar](500) NULL,
	[alternateMobile] [nvarchar](500) NULL,
	[email] [nvarchar](500) NULL,
 CONSTRAINT [PK_VendorDetails_1] PRIMARY KEY CLUSTERED 
(
	[VendorDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VendorExpenses]    Script Date: 16-11-2019 16:28:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VendorExpenses](
	[VendorExpensesId] [bigint] IDENTITY(1,1) NOT NULL,
	[StockInId] [bigint] NULL,
	[ExpenseId] [bigint] NULL,
	[Amount] [bigint] NULL,
	[createdBy] [nvarchar](500) NULL,
	[modifiedDate] [datetime] NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[modifiedBy] [nvarchar](500) NULL,
 CONSTRAINT [PK_VendorExpenses] PRIMARY KEY CLUSTERED 
(
	[VendorExpensesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VendorPayments]    Script Date: 16-11-2019 16:28:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VendorPayments](
	[VendorPaymentId] [bigint] IDENTITY(1,1) NOT NULL,
	[VendorId] [bigint] NULL,
	[StockInId] [bigint] NULL,
	[AmountPaid] [bigint] NULL,
	[createdDate] [datetime] NOT NULL,
	[modifiedDate] [datetime] NOT NULL,
	[createdBy] [nvarchar](50) NULL,
	[modifiedBy] [nvarchar](50) NULL,
	[LoggedInUser] [nvarchar](500) NULL,
 CONSTRAINT [PK_VendorPayments] PRIMARY KEY CLUSTERED 
(
	[VendorPaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CommissionAgentExpenses] ADD  CONSTRAINT [DF_CommisionAgentExpenses_modifiedDate]  DEFAULT (getdate()) FOR [modifiedDate]
GO
ALTER TABLE [dbo].[CommissionAgentExpenses] ADD  CONSTRAINT [DF_CommisionAgentExpenses_createdDate]  DEFAULT (getdate()) FOR [createdDate]
GO
ALTER TABLE [dbo].[CommissionAgentPurchases] ADD  CONSTRAINT [DF_CommissionAgentPurchases_createdDate]  DEFAULT (getdate()) FOR [createdDate]
GO
ALTER TABLE [dbo].[CommissionAgentPurchases] ADD  CONSTRAINT [DF_CommissionAgentPurchases_modifiedDate]  DEFAULT (getdate()) FOR [modifiedDate]
GO
ALTER TABLE [dbo].[CommissionAgentSales] ADD  CONSTRAINT [DF_CommissionAgentSales_createdDate]  DEFAULT (getdate()) FOR [createdDate]
GO
ALTER TABLE [dbo].[CommissionAgentSales] ADD  CONSTRAINT [DF_CommissionAgentSales_modifiedDate]  DEFAULT (getdate()) FOR [modifiedDate]
GO
ALTER TABLE [dbo].[CommissionEarned] ADD  CONSTRAINT [DF_CommissionEarned_modifiedDate]  DEFAULT (getdate()) FOR [modifiedDate]
GO
ALTER TABLE [dbo].[CommissionEarned] ADD  CONSTRAINT [DF_CommissionEarned_createdDate]  DEFAULT (getdate()) FOR [createdDate]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_modifiedDate]  DEFAULT (getdate()) FOR [modifiedDate]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_modifiedDate1]  DEFAULT (getdate()) FOR [createdDate]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[CustomerPayments] ADD  CONSTRAINT [DF_CustomerPayments_createdDate]  DEFAULT (getdate()) FOR [createdDate]
GO
ALTER TABLE [dbo].[CustomerPayments] ADD  CONSTRAINT [DF_CustomerPayments_modifiedDate]  DEFAULT (getdate()) FOR [modifiedDate]
GO
ALTER TABLE [dbo].[Sales] ADD  CONSTRAINT [DF_Sales_createdDate]  DEFAULT (getdate()) FOR [createdDate]
GO
ALTER TABLE [dbo].[Sales] ADD  CONSTRAINT [DF_Sales_modifiedDate]  DEFAULT (getdate()) FOR [modifiedDate]
GO
ALTER TABLE [dbo].[Sales] ADD  CONSTRAINT [DF_Sales_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[SalesReturns] ADD  CONSTRAINT [DF_SalesReturns_createdDate]  DEFAULT (getdate()) FOR [createdDate]
GO
ALTER TABLE [dbo].[SalesReturns] ADD  CONSTRAINT [DF_SalesReturns_modifiedDate]  DEFAULT (getdate()) FOR [modifiedDate]
GO
ALTER TABLE [dbo].[StockIn] ADD  CONSTRAINT [DF_StockIn_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[Vendor] ADD  CONSTRAINT [DF_VendorDetails_createdDate]  DEFAULT (getdate()) FOR [createdDate]
GO
ALTER TABLE [dbo].[Vendor] ADD  CONSTRAINT [DF_VendorDetails_modifiedDate]  DEFAULT (getdate()) FOR [modifiedDate]
GO
ALTER TABLE [dbo].[Vendor] ADD  CONSTRAINT [DF_Vendor_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[VendorAdvances] ADD  CONSTRAINT [DF_VendorAdvances_createdDate]  DEFAULT (getdate()) FOR [createdDate]
GO
ALTER TABLE [dbo].[VendorAdvances] ADD  CONSTRAINT [DF_VendorAdvances_modifiedDate]  DEFAULT (getdate()) FOR [modifiedDate]
GO
ALTER TABLE [dbo].[VendorAdvancesReturned] ADD  CONSTRAINT [DF_VendorAdvancesReturned_createdDate]  DEFAULT (getdate()) FOR [createdDate]
GO
ALTER TABLE [dbo].[VendorAdvancesReturned] ADD  CONSTRAINT [DF_VendorAdvancesReturned_modifiedDate]  DEFAULT (getdate()) FOR [modifiedDate]
GO
ALTER TABLE [dbo].[VendorExpenses] ADD  CONSTRAINT [DF_VendorExpenses_modifiedDate]  DEFAULT (getdate()) FOR [modifiedDate]
GO
ALTER TABLE [dbo].[VendorExpenses] ADD  CONSTRAINT [DF_VendorExpenses_createdDate]  DEFAULT (getdate()) FOR [createdDate]
GO
ALTER TABLE [dbo].[VendorPayments] ADD  CONSTRAINT [DF_VendorPayments_createdDate]  DEFAULT (getdate()) FOR [createdDate]
GO
ALTER TABLE [dbo].[VendorPayments] ADD  CONSTRAINT [DF_VendorPayments_modifiedDate]  DEFAULT (getdate()) FOR [modifiedDate]
GO
ALTER TABLE [dbo].[CommissionAgentExpenses]  WITH CHECK ADD  CONSTRAINT [FK_CommisionAgentExpenses_Expenses] FOREIGN KEY([ExpenseId])
REFERENCES [dbo].[Expenses] ([ExpenseId])
GO
ALTER TABLE [dbo].[CommissionAgentExpenses] CHECK CONSTRAINT [FK_CommisionAgentExpenses_Expenses]
GO
ALTER TABLE [dbo].[CommissionAgentPurchases]  WITH CHECK ADD  CONSTRAINT [FK_CommissionAgentPurchases_Vendor] FOREIGN KEY([VendorId])
REFERENCES [dbo].[Vendor] ([VendorId])
GO
ALTER TABLE [dbo].[CommissionAgentPurchases] CHECK CONSTRAINT [FK_CommissionAgentPurchases_Vendor]
GO
ALTER TABLE [dbo].[CommissionAgentSales]  WITH CHECK ADD  CONSTRAINT [FK_CommissionAgentSales_Vendor] FOREIGN KEY([VendorId])
REFERENCES [dbo].[Vendor] ([VendorId])
GO
ALTER TABLE [dbo].[CommissionAgentSales] CHECK CONSTRAINT [FK_CommissionAgentSales_Vendor]
GO
ALTER TABLE [dbo].[CommissionEarned]  WITH CHECK ADD  CONSTRAINT [FK_CommissionEarned_CommissionPercentage] FOREIGN KEY([CommissionPercentageId])
REFERENCES [dbo].[CommissionPercentage] ([CommissionPercentageId])
GO
ALTER TABLE [dbo].[CommissionEarned] CHECK CONSTRAINT [FK_CommissionEarned_CommissionPercentage]
GO
ALTER TABLE [dbo].[CommissionEarned]  WITH CHECK ADD  CONSTRAINT [FK_CommissionEarned_StockIn] FOREIGN KEY([StockInId])
REFERENCES [dbo].[StockIn] ([StockID])
GO
ALTER TABLE [dbo].[CommissionEarned] CHECK CONSTRAINT [FK_CommissionEarned_StockIn]
GO
ALTER TABLE [dbo].[CustomerDetails]  WITH CHECK ADD  CONSTRAINT [FK_CustomerAddress_Customer] FOREIGN KEY([CustId])
REFERENCES [dbo].[Customer] ([CustId])
GO
ALTER TABLE [dbo].[CustomerDetails] CHECK CONSTRAINT [FK_CustomerAddress_Customer]
GO
ALTER TABLE [dbo].[CustomerPayments]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPayments_Customer] FOREIGN KEY([CustId])
REFERENCES [dbo].[Customer] ([CustId])
GO
ALTER TABLE [dbo].[CustomerPayments] CHECK CONSTRAINT [FK_CustomerPayments_Customer]
GO
ALTER TABLE [dbo].[Expenses]  WITH CHECK ADD  CONSTRAINT [FK_Expenses_ExpenseTypes] FOREIGN KEY([ExpenseTypeId])
REFERENCES [dbo].[ExpenseTypes] ([ExpenseTypeId])
GO
ALTER TABLE [dbo].[Expenses] CHECK CONSTRAINT [FK_Expenses_ExpenseTypes]
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD  CONSTRAINT [FK_Sales_Customer] FOREIGN KEY([CustId])
REFERENCES [dbo].[Customer] ([CustId])
GO
ALTER TABLE [dbo].[Sales] CHECK CONSTRAINT [FK_Sales_Customer]
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD  CONSTRAINT [FK_Sales_StockIn] FOREIGN KEY([StockInId])
REFERENCES [dbo].[StockIn] ([StockID])
GO
ALTER TABLE [dbo].[Sales] CHECK CONSTRAINT [FK_Sales_StockIn]
GO
ALTER TABLE [dbo].[SalesReturns]  WITH CHECK ADD  CONSTRAINT [FK_SalesReturns_StockIn] FOREIGN KEY([CustId])
REFERENCES [dbo].[Customer] ([CustId])
GO
ALTER TABLE [dbo].[SalesReturns] CHECK CONSTRAINT [FK_SalesReturns_StockIn]
GO
ALTER TABLE [dbo].[SalesReturns]  WITH CHECK ADD  CONSTRAINT [FK_SalesReturns_StockIn1] FOREIGN KEY([StockInId])
REFERENCES [dbo].[StockIn] ([StockID])
GO
ALTER TABLE [dbo].[SalesReturns] CHECK CONSTRAINT [FK_SalesReturns_StockIn1]
GO
ALTER TABLE [dbo].[StockIn]  WITH CHECK ADD  CONSTRAINT [FK_StockIn_VendorDetails] FOREIGN KEY([VendorId])
REFERENCES [dbo].[Vendor] ([VendorId])
GO
ALTER TABLE [dbo].[StockIn] CHECK CONSTRAINT [FK_StockIn_VendorDetails]
GO
ALTER TABLE [dbo].[VendorAdvances]  WITH CHECK ADD  CONSTRAINT [FK_VendorAdvances_Vendor] FOREIGN KEY([VendorId])
REFERENCES [dbo].[Vendor] ([VendorId])
GO
ALTER TABLE [dbo].[VendorAdvances] CHECK CONSTRAINT [FK_VendorAdvances_Vendor]
GO
ALTER TABLE [dbo].[VendorAdvancesReturned]  WITH CHECK ADD  CONSTRAINT [FK_VendorAdvancesReturned_Vendor] FOREIGN KEY([VendorId])
REFERENCES [dbo].[Vendor] ([VendorId])
GO
ALTER TABLE [dbo].[VendorAdvancesReturned] CHECK CONSTRAINT [FK_VendorAdvancesReturned_Vendor]
GO
ALTER TABLE [dbo].[VendorDetails]  WITH CHECK ADD  CONSTRAINT [FK_VendorDetails_Vendor] FOREIGN KEY([VendorId])
REFERENCES [dbo].[Vendor] ([VendorId])
GO
ALTER TABLE [dbo].[VendorDetails] CHECK CONSTRAINT [FK_VendorDetails_Vendor]
GO
ALTER TABLE [dbo].[VendorExpenses]  WITH CHECK ADD  CONSTRAINT [FK_VendorExpenses_Expenses] FOREIGN KEY([ExpenseId])
REFERENCES [dbo].[Expenses] ([ExpenseId])
GO
ALTER TABLE [dbo].[VendorExpenses] CHECK CONSTRAINT [FK_VendorExpenses_Expenses]
GO
ALTER TABLE [dbo].[VendorExpenses]  WITH CHECK ADD  CONSTRAINT [FK_VendorExpenses_StockIn] FOREIGN KEY([StockInId])
REFERENCES [dbo].[StockIn] ([StockID])
GO
ALTER TABLE [dbo].[VendorExpenses] CHECK CONSTRAINT [FK_VendorExpenses_StockIn]
GO
ALTER TABLE [dbo].[VendorPayments]  WITH CHECK ADD  CONSTRAINT [FK_VendorPayments_StockIn] FOREIGN KEY([StockInId])
REFERENCES [dbo].[StockIn] ([StockID])
GO
ALTER TABLE [dbo].[VendorPayments] CHECK CONSTRAINT [FK_VendorPayments_StockIn]
GO
ALTER TABLE [dbo].[VendorPayments]  WITH CHECK ADD  CONSTRAINT [FK_VendorPayments_Vendor] FOREIGN KEY([VendorId])
REFERENCES [dbo].[Vendor] ([VendorId])
GO
ALTER TABLE [dbo].[VendorPayments] CHECK CONSTRAINT [FK_VendorPayments_Vendor]
GO
USE [master]
GO
ALTER DATABASE [AccountDb] SET  READ_WRITE 
GO
