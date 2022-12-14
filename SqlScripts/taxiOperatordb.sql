USE [master]
GO
/****** Object:  Database [TaxiOperatorDb]    Script Date: 9/27/2022 1:52:03 PM ******/
CREATE DATABASE [TaxiOperatorDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TaxiOperatorDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TaxiOperatorDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TaxiOperatorDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TaxiOperatorDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TaxiOperatorDb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TaxiOperatorDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TaxiOperatorDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TaxiOperatorDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TaxiOperatorDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TaxiOperatorDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TaxiOperatorDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [TaxiOperatorDb] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [TaxiOperatorDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TaxiOperatorDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TaxiOperatorDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TaxiOperatorDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TaxiOperatorDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TaxiOperatorDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TaxiOperatorDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TaxiOperatorDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TaxiOperatorDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TaxiOperatorDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TaxiOperatorDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TaxiOperatorDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TaxiOperatorDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TaxiOperatorDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TaxiOperatorDb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [TaxiOperatorDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TaxiOperatorDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TaxiOperatorDb] SET  MULTI_USER 
GO
ALTER DATABASE [TaxiOperatorDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TaxiOperatorDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TaxiOperatorDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TaxiOperatorDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TaxiOperatorDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TaxiOperatorDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [TaxiOperatorDb] SET QUERY_STORE = OFF
GO
USE [TaxiOperatorDb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 9/27/2022 1:52:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 9/27/2022 1:52:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[AddressTypeId] [int] NOT NULL,
	[address_street_number] [int] NOT NULL,
	[address_street_name] [nvarchar](max) NOT NULL,
	[address_zip_postal] [int] NOT NULL,
	[CityId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Address_Type]    Script Date: 9/27/2022 1:52:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address_Type](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[address_type_description] [nvarchar](10) NULL,
 CONSTRAINT [PK_Address_Type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[admin]    Script Date: 9/27/2022 1:52:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[admin](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[admin_first_name] [nvarchar](max) NOT NULL,
	[admin_last_name] [nvarchar](max) NOT NULL,
	[admin_email] [nvarchar](max) NOT NULL,
	[admin_password] [nvarchar](max) NOT NULL,
	[admin_login_token] [uniqueidentifier] NOT NULL,
	[admin_login_token_expiry] [datetime2](7) NOT NULL,
	[admin_is_locked] [bit] NOT NULL,
	[admin_role_name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_admin] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bugs]    Script Date: 9/27/2022 1:52:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bugs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[error_source] [nvarchar](max) NOT NULL,
	[error_Message] [nvarchar](max) NOT NULL,
	[error_stacktrace] [nvarchar](max) NOT NULL,
	[error_inner_exception] [nvarchar](max) NULL,
	[error_target_site] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Bugs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cab]    Script Date: 9/27/2022 1:52:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cab](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[license_plate] [nvarchar](40) NOT NULL,
	[CarModelId] [int] NOT NULL,
	[cab_is_active] [bit] NOT NULL,
 CONSTRAINT [PK_Cab] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Car_Model]    Script Date: 9/27/2022 1:52:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Car_Model](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[model_name] [nvarchar](128) NOT NULL,
	[model_description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Car_Model] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[City]    Script Date: 9/27/2022 1:52:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[city_name] [nvarchar](50) NOT NULL,
	[CountryId] [int] NOT NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 9/27/2022 1:52:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[country_name] [nvarchar](max) NOT NULL,
	[country_initials] [nvarchar](3) NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cupon]    Script Date: 9/27/2022 1:52:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cupon](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cupon_customer_id] [int] NOT NULL,
	[cupon_date_issued] [datetime2](7) NOT NULL,
	[cupon_date_expiry] [datetime2](7) NOT NULL,
	[cupon_code] [nvarchar](50) NOT NULL,
	[cupon_discount] [int] NOT NULL,
 CONSTRAINT [PK_Cupon] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 9/27/2022 1:52:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[customer_first_name] [nvarchar](128) NOT NULL,
	[customer_last_name] [nvarchar](128) NOT NULL,
	[customer_email] [nvarchar](450) NOT NULL,
	[customer_dob] [datetime2](7) NOT NULL,
	[customer_gender] [nvarchar](max) NOT NULL,
	[customer_password] [nvarchar](max) NOT NULL,
	[customer_last_login] [datetime2](7) NOT NULL,
	[customer_failed_logins] [int] NOT NULL,
	[customer_account_disabled] [bit] NOT NULL,
	[customer_points] [decimal](18, 2) NOT NULL,
	[customer_login_token] [uniqueidentifier] NOT NULL,
	[customer_login_token_expiry] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Driver]    Script Date: 9/27/2022 1:52:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Driver](
	[driver_id] [int] IDENTITY(1,1) NOT NULL,
	[driver_first_name] [nvarchar](128) NOT NULL,
	[driver_last_name] [nvarchar](128) NOT NULL,
	[driver_dob] [datetime2](7) NOT NULL,
	[phone_number] [nvarchar](max) NOT NULL,
	[driving_license_number] [nvarchar](max) NOT NULL,
	[driving_license_expiry] [datetime2](7) NOT NULL,
	[driver_is_active] [bit] NOT NULL,
	[driver_username] [nvarchar](max) NOT NULL,
	[driver_password] [nvarchar](max) NOT NULL,
	[driver_email] [nvarchar](450) NOT NULL,
	[driver_last_login] [datetime2](7) NOT NULL,
	[driver_failed_logins] [int] NOT NULL,
	[driver_account_disabled] [bit] NOT NULL,
	[driver_login_token] [uniqueidentifier] NOT NULL,
	[driver_login_token_expiry] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Driver] PRIMARY KEY CLUSTERED 
(
	[driver_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment_Type]    Script Date: 9/27/2022 1:52:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment_Type](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[payment_type_name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Payment_Type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pricing]    Script Date: 9/27/2022 1:52:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pricing](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[pricing_name] [nvarchar](50) NOT NULL,
	[pricing_per_km] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Pricing] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ride]    Script Date: 9/27/2022 1:52:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ride](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ride_shift_id] [int] NOT NULL,
	[ride_start_time] [datetime2](7) NOT NULL,
	[ride_end_time] [datetime2](7) NULL,
	[ride_starting_address] [int] NOT NULL,
	[ride_destination_address] [int] NOT NULL,
	[ride_canceled] [bit] NOT NULL,
	[ride_done] [bit] NOT NULL,
	[ride_reward_points] [int] NOT NULL,
	[ride_price] [decimal](18, 2) NOT NULL,
	[ride_payment_type] [int] NOT NULL,
	[ride_cupon_id] [int] NULL,
	[ride_starting_latitude] [decimal](18, 6) NOT NULL,
	[ride_starting_longitude] [decimal](18, 6) NOT NULL,
	[ride_ending_latitude] [decimal](18, 6) NOT NULL,
	[ride_ending_longitude] [decimal](18, 6) NOT NULL,
	[ride_distance] [decimal](18, 2) NOT NULL,
	[ride_pricing_id] [int] NOT NULL,
	[ride_customer_id] [int] NOT NULL,
 CONSTRAINT [PK_ride] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shift]    Script Date: 9/27/2022 1:52:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shift](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[driver_id] [int] NOT NULL,
	[shift_cab_id] [int] NOT NULL,
	[shift_start] [datetime2](7) NOT NULL,
	[shift_end] [datetime2](7) NOT NULL,
	[shift_login_time] [datetime2](7) NOT NULL,
	[shift_logout_time] [datetime2](7) NOT NULL,
	[shift_is_active] [bit] NOT NULL,
	[shift_is_available] [bit] NOT NULL,
	[shift_longitude] [decimal](18, 6) NOT NULL,
	[shift_latitude] [decimal](18, 6) NOT NULL,
 CONSTRAINT [PK_Shift] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220924194328_m1', N'5.0.17')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220924194901_m2', N'5.0.17')
GO
SET IDENTITY_INSERT [dbo].[Address] ON 
GO
INSERT [dbo].[Address] ([id], [AddressTypeId], [address_street_number], [address_street_name], [address_zip_postal], [CityId], [CustomerId]) VALUES (4, 1, 1234, N'Main', 0, 1, 1)
GO
INSERT [dbo].[Address] ([id], [AddressTypeId], [address_street_number], [address_street_name], [address_zip_postal], [CityId], [CustomerId]) VALUES (5, 2, 1234, N'Main st', 0, 2, 2)
GO
INSERT [dbo].[Address] ([id], [AddressTypeId], [address_street_number], [address_street_name], [address_zip_postal], [CityId], [CustomerId]) VALUES (6, 3, 1245, N'Main st', 0, 3, 2)
GO
SET IDENTITY_INSERT [dbo].[Address] OFF
GO
SET IDENTITY_INSERT [dbo].[Address_Type] ON 
GO
INSERT [dbo].[Address_Type] ([id], [address_type_description]) VALUES (1, N'HOME')
GO
INSERT [dbo].[Address_Type] ([id], [address_type_description]) VALUES (2, N'WORK')
GO
INSERT [dbo].[Address_Type] ([id], [address_type_description]) VALUES (3, N'HOME1')
GO
INSERT [dbo].[Address_Type] ([id], [address_type_description]) VALUES (4, N'WORK1')
GO
SET IDENTITY_INSERT [dbo].[Address_Type] OFF
GO
SET IDENTITY_INSERT [dbo].[admin] ON 
GO
INSERT [dbo].[admin] ([id], [admin_first_name], [admin_last_name], [admin_email], [admin_password], [admin_login_token], [admin_login_token_expiry], [admin_is_locked], [admin_role_name]) VALUES (1, N'nael', N'ghannam', N'nael@gmail.com', N'$2a$11$bjtY7wxp28Fjf9qDQwQqYeXsFD6QyalVSbmZyuNR.iVJ1o6KGB0pW', N'8f3d1595-a22f-47cf-90fe-4674dd92b887', CAST(N'2022-09-25T00:00:00.0000000' AS DateTime2), 0, N'Admin')
GO
SET IDENTITY_INSERT [dbo].[admin] OFF
GO
SET IDENTITY_INSERT [dbo].[Bugs] ON 
GO
INSERT [dbo].[Bugs] ([id], [error_source], [error_Message], [error_stacktrace], [error_inner_exception], [error_target_site]) VALUES (1, N'EcomzExercise', N'Attempted to divide by zero.', N'   at EcomzExercise.Services.ShiftService.ListShifts() in C:\Users\G.K\Desktop\Ecomz\Backend\Data\Services\ShiftService.cs:line 224', NULL, N'System.Collections.Generic.List`1[EcomzExercise.Models.View_Models.ListShiftsVM] ListShifts()')
GO
SET IDENTITY_INSERT [dbo].[Bugs] OFF
GO
SET IDENTITY_INSERT [dbo].[Cab] ON 
GO
INSERT [dbo].[Cab] ([id], [license_plate], [CarModelId], [cab_is_active]) VALUES (1, N'76BN8', 1, 0)
GO
INSERT [dbo].[Cab] ([id], [license_plate], [CarModelId], [cab_is_active]) VALUES (2, N'7Ugsv6e', 2, 0)
GO
INSERT [dbo].[Cab] ([id], [license_plate], [CarModelId], [cab_is_active]) VALUES (3, N'7UGH65C', 3, 0)
GO
SET IDENTITY_INSERT [dbo].[Cab] OFF
GO
SET IDENTITY_INSERT [dbo].[Car_Model] ON 
GO
INSERT [dbo].[Car_Model] ([Id], [model_name], [model_description]) VALUES (1, N'Honda', N'CRV 2005')
GO
INSERT [dbo].[Car_Model] ([Id], [model_name], [model_description]) VALUES (2, N'Nissan', N'Sunny 2004')
GO
INSERT [dbo].[Car_Model] ([Id], [model_name], [model_description]) VALUES (3, N'Mercedes', N'CLK')
GO
SET IDENTITY_INSERT [dbo].[Car_Model] OFF
GO
SET IDENTITY_INSERT [dbo].[City] ON 
GO
INSERT [dbo].[City] ([id], [city_name], [CountryId]) VALUES (1, N'Chouf', 1)
GO
INSERT [dbo].[City] ([id], [city_name], [CountryId]) VALUES (2, N'Beirut', 1)
GO
INSERT [dbo].[City] ([id], [city_name], [CountryId]) VALUES (3, N'Bekaa', 1)
GO
INSERT [dbo].[City] ([id], [city_name], [CountryId]) VALUES (4, N'Aley', 1)
GO
INSERT [dbo].[City] ([id], [city_name], [CountryId]) VALUES (5, N'Berlin', 3)
GO
INSERT [dbo].[City] ([id], [city_name], [CountryId]) VALUES (6, N'Paris', 2)
GO
SET IDENTITY_INSERT [dbo].[City] OFF
GO
SET IDENTITY_INSERT [dbo].[Country] ON 
GO
INSERT [dbo].[Country] ([id], [country_name], [country_initials]) VALUES (1, N'Lebanon', N'LEB')
GO
INSERT [dbo].[Country] ([id], [country_name], [country_initials]) VALUES (2, N'France', N'FRA')
GO
INSERT [dbo].[Country] ([id], [country_name], [country_initials]) VALUES (3, N'Germany', N'GER')
GO
SET IDENTITY_INSERT [dbo].[Country] OFF
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 
GO
INSERT [dbo].[Customer] ([id], [customer_first_name], [customer_last_name], [customer_email], [customer_dob], [customer_gender], [customer_password], [customer_last_login], [customer_failed_logins], [customer_account_disabled], [customer_points], [customer_login_token], [customer_login_token_expiry]) VALUES (1, N'nael', N'ghannam', N'nael@gmail.com', CAST(N'1999-07-18T19:34:25.2600000' AS DateTime2), N'Male', N'$2a$11$aKjwAnNTX3m7O9266xbQ6.bINqGrRSkjQw3JVDKZDuSpc.PtBwMPG', CAST(N'2022-09-25T22:34:57.0221905' AS DateTime2), 0, 0, CAST(0.00 AS Decimal(18, 2)), N'1dcc8f5e-bbed-4679-87ba-f4894f6db363', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[Customer] ([id], [customer_first_name], [customer_last_name], [customer_email], [customer_dob], [customer_gender], [customer_password], [customer_last_login], [customer_failed_logins], [customer_account_disabled], [customer_points], [customer_login_token], [customer_login_token_expiry]) VALUES (2, N'john', N'doe', N'john@gmail.com', CAST(N'1987-07-18T19:34:25.2600000' AS DateTime2), N'Male', N'$2a$11$b2tkVX./czI6CYmViQemw.TQ3VnAUwAaiGzbD73bBlot8Ghp4/7u2', CAST(N'2022-09-25T22:35:13.2638567' AS DateTime2), 0, 0, CAST(0.00 AS Decimal(18, 2)), N'd4a920d7-f82f-4360-a988-aa0ec5edbf3e', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[Driver] ON 
GO
INSERT [dbo].[Driver] ([driver_id], [driver_first_name], [driver_last_name], [driver_dob], [phone_number], [driving_license_number], [driving_license_expiry], [driver_is_active], [driver_username], [driver_password], [driver_email], [driver_last_login], [driver_failed_logins], [driver_account_disabled], [driver_login_token], [driver_login_token_expiry]) VALUES (1, N'nael', N'ghannam', CAST(N'2022-09-25T21:58:22.9588860' AS DateTime2), N'0096171313732', N'1234-5678-9101112', CAST(N'2023-09-25T21:58:22.9928572' AS DateTime2), 1, N'naelghannam18', N'$2a$11$C.2XGhbAlfjM4p/Vjyll0ugIaLEACFYGJ0SAWXhmgZ1L310JSucNK', N'nael@gmail.com', CAST(N'2022-09-25T21:59:38.6777773' AS DateTime2), 0, 0, N'2d5b6289-9778-452e-8beb-d01c04c71ad8', CAST(N'2022-09-25T22:59:38.6779743' AS DateTime2))
GO
INSERT [dbo].[Driver] ([driver_id], [driver_first_name], [driver_last_name], [driver_dob], [phone_number], [driving_license_number], [driving_license_expiry], [driver_is_active], [driver_username], [driver_password], [driver_email], [driver_last_login], [driver_failed_logins], [driver_account_disabled], [driver_login_token], [driver_login_token_expiry]) VALUES (2, N'John', N'Doe', CAST(N'2022-09-25T21:59:14.2572317' AS DateTime2), N'0096171313733', N'1234-5678-9101113', CAST(N'2023-09-25T21:59:14.2572573' AS DateTime2), 0, N'johnDoe18', N'$2a$11$WatME/HD6jaB63VqQxEN1ehwLl2zmHICaFKOMq4MCJlo/P4F4aSNC', N'john@gmail.com', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, 0, N'00000000-0000-0000-0000-000000000000', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Driver] OFF
GO
SET IDENTITY_INSERT [dbo].[Payment_Type] ON 
GO
INSERT [dbo].[Payment_Type] ([id], [payment_type_name]) VALUES (1, N'Cash')
GO
INSERT [dbo].[Payment_Type] ([id], [payment_type_name]) VALUES (2, N'Credit')
GO
SET IDENTITY_INSERT [dbo].[Payment_Type] OFF
GO
SET IDENTITY_INSERT [dbo].[Shift] ON 
GO
INSERT [dbo].[Shift] ([id], [driver_id], [shift_cab_id], [shift_start], [shift_end], [shift_login_time], [shift_logout_time], [shift_is_active], [shift_is_available], [shift_longitude], [shift_latitude]) VALUES (45, 1, 1, CAST(N'2022-09-28T07:00:00.7290000' AS DateTime2), CAST(N'2022-09-28T10:00:00.7290000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 1, 1, CAST(35.483045 AS Decimal(18, 6)), CAST(33.882957 AS Decimal(18, 6)))
GO
INSERT [dbo].[Shift] ([id], [driver_id], [shift_cab_id], [shift_start], [shift_end], [shift_login_time], [shift_logout_time], [shift_is_active], [shift_is_available], [shift_longitude], [shift_latitude]) VALUES (46, 1, 1, CAST(N'2022-09-28T10:01:00.7290000' AS DateTime2), CAST(N'2022-09-28T12:00:00.7290000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 1, 1, CAST(35.497913 AS Decimal(18, 6)), CAST(33.900052 AS Decimal(18, 6)))
GO
INSERT [dbo].[Shift] ([id], [driver_id], [shift_cab_id], [shift_start], [shift_end], [shift_login_time], [shift_logout_time], [shift_is_active], [shift_is_available], [shift_longitude], [shift_latitude]) VALUES (47, 1, 1, CAST(N'2022-09-28T12:01:00.7290000' AS DateTime2), CAST(N'2022-09-28T14:00:00.7290000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 1, 1, CAST(35.489711 AS Decimal(18, 6)), CAST(33.884262 AS Decimal(18, 6)))
GO
INSERT [dbo].[Shift] ([id], [driver_id], [shift_cab_id], [shift_start], [shift_end], [shift_login_time], [shift_logout_time], [shift_is_active], [shift_is_available], [shift_longitude], [shift_latitude]) VALUES (48, 2, 2, CAST(N'2022-09-28T07:00:00.7290000' AS DateTime2), CAST(N'2022-09-28T10:00:00.7290000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 1, 1, CAST(35.494000 AS Decimal(18, 6)), CAST(33.875592 AS Decimal(18, 6)))
GO
INSERT [dbo].[Shift] ([id], [driver_id], [shift_cab_id], [shift_start], [shift_end], [shift_login_time], [shift_logout_time], [shift_is_active], [shift_is_available], [shift_longitude], [shift_latitude]) VALUES (49, 2, 2, CAST(N'2022-09-28T10:01:00.7290000' AS DateTime2), CAST(N'2022-09-28T12:00:00.7290000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 1, 1, CAST(35.487918 AS Decimal(18, 6)), CAST(33.880796 AS Decimal(18, 6)))
GO
INSERT [dbo].[Shift] ([id], [driver_id], [shift_cab_id], [shift_start], [shift_end], [shift_login_time], [shift_logout_time], [shift_is_active], [shift_is_available], [shift_longitude], [shift_latitude]) VALUES (50, 2, 1, CAST(N'2022-09-27T07:00:00.9610000' AS DateTime2), CAST(N'2022-09-27T20:00:00.9610000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 1, 1, CAST(35.498314 AS Decimal(18, 6)), CAST(33.875945 AS Decimal(18, 6)))
GO
INSERT [dbo].[Shift] ([id], [driver_id], [shift_cab_id], [shift_start], [shift_end], [shift_login_time], [shift_logout_time], [shift_is_active], [shift_is_available], [shift_longitude], [shift_latitude]) VALUES (53, 1, 1, CAST(N'2022-09-27T07:29:24.0630000' AS DateTime2), CAST(N'2022-09-27T15:29:24.0630000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 1, 1, CAST(35.496998 AS Decimal(18, 6)), CAST(33.877526 AS Decimal(18, 6)))
GO
SET IDENTITY_INSERT [dbo].[Shift] OFF
GO
/****** Object:  Index [IX_Address_AddressTypeId]    Script Date: 9/27/2022 1:52:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_Address_AddressTypeId] ON [dbo].[Address]
(
	[AddressTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Address_CityId]    Script Date: 9/27/2022 1:52:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_Address_CityId] ON [dbo].[Address]
(
	[CityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Address_CustomerId]    Script Date: 9/27/2022 1:52:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_Address_CustomerId] ON [dbo].[Address]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Cab_CarModelId]    Script Date: 9/27/2022 1:52:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_Cab_CarModelId] ON [dbo].[Cab]
(
	[CarModelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_City_CountryId]    Script Date: 9/27/2022 1:52:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_City_CountryId] ON [dbo].[City]
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Customer_customer_email]    Script Date: 9/27/2022 1:52:04 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Customer_customer_email] ON [dbo].[Customer]
(
	[customer_email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Driver_driver_email]    Script Date: 9/27/2022 1:52:04 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Driver_driver_email] ON [dbo].[Driver]
(
	[driver_email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Shift_CabId]    Script Date: 9/27/2022 1:52:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_Shift_CabId] ON [dbo].[Shift]
(
	[shift_cab_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_Address_Type_AddressTypeId] FOREIGN KEY([AddressTypeId])
REFERENCES [dbo].[Address_Type] ([id])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_Address_Type_AddressTypeId]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_City_CityId] FOREIGN KEY([CityId])
REFERENCES [dbo].[City] ([id])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_City_CityId]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_Customer_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_Customer_CustomerId]
GO
ALTER TABLE [dbo].[Cab]  WITH CHECK ADD  CONSTRAINT [FK_Cab_Car_Model_CarModelId] FOREIGN KEY([CarModelId])
REFERENCES [dbo].[Car_Model] ([Id])
GO
ALTER TABLE [dbo].[Cab] CHECK CONSTRAINT [FK_Cab_Car_Model_CarModelId]
GO
ALTER TABLE [dbo].[City]  WITH CHECK ADD  CONSTRAINT [FK_City_Country_CountryId] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([id])
GO
ALTER TABLE [dbo].[City] CHECK CONSTRAINT [FK_City_Country_CountryId]
GO
ALTER TABLE [dbo].[Cupon]  WITH CHECK ADD  CONSTRAINT [FK_cupon_Customer] FOREIGN KEY([cupon_customer_id])
REFERENCES [dbo].[Customer] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cupon] CHECK CONSTRAINT [FK_cupon_Customer]
GO
ALTER TABLE [dbo].[ride]  WITH CHECK ADD  CONSTRAINT [FK_ride_address_addressid_destination] FOREIGN KEY([ride_destination_address])
REFERENCES [dbo].[Address] ([id])
GO
ALTER TABLE [dbo].[ride] CHECK CONSTRAINT [FK_ride_address_addressid_destination]
GO
ALTER TABLE [dbo].[ride]  WITH CHECK ADD  CONSTRAINT [FK_ride_address_addressId_starting] FOREIGN KEY([ride_starting_address])
REFERENCES [dbo].[Address] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ride] CHECK CONSTRAINT [FK_ride_address_addressId_starting]
GO
ALTER TABLE [dbo].[ride]  WITH CHECK ADD  CONSTRAINT [FK_ride_cupon_cuponId] FOREIGN KEY([ride_cupon_id])
REFERENCES [dbo].[Cupon] ([id])
GO
ALTER TABLE [dbo].[ride] CHECK CONSTRAINT [FK_ride_cupon_cuponId]
GO
ALTER TABLE [dbo].[ride]  WITH CHECK ADD  CONSTRAINT [FK_ride_customer_customerId] FOREIGN KEY([ride_customer_id])
REFERENCES [dbo].[Customer] ([id])
GO
ALTER TABLE [dbo].[ride] CHECK CONSTRAINT [FK_ride_customer_customerId]
GO
ALTER TABLE [dbo].[ride]  WITH CHECK ADD  CONSTRAINT [FK_ride_paymentType_paymentTypeid] FOREIGN KEY([ride_payment_type])
REFERENCES [dbo].[Payment_Type] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ride] CHECK CONSTRAINT [FK_ride_paymentType_paymentTypeid]
GO
ALTER TABLE [dbo].[ride]  WITH CHECK ADD  CONSTRAINT [FK_ride_pricing_pricingId] FOREIGN KEY([ride_pricing_id])
REFERENCES [dbo].[Pricing] ([id])
GO
ALTER TABLE [dbo].[ride] CHECK CONSTRAINT [FK_ride_pricing_pricingId]
GO
ALTER TABLE [dbo].[ride]  WITH CHECK ADD  CONSTRAINT [FK_ride_shift_shiftid] FOREIGN KEY([ride_shift_id])
REFERENCES [dbo].[Shift] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ride] CHECK CONSTRAINT [FK_ride_shift_shiftid]
GO
ALTER TABLE [dbo].[Shift]  WITH CHECK ADD  CONSTRAINT [FK_Shift_Cab_CabId] FOREIGN KEY([shift_cab_id])
REFERENCES [dbo].[Cab] ([id])
GO
ALTER TABLE [dbo].[Shift] CHECK CONSTRAINT [FK_Shift_Cab_CabId]
GO
ALTER TABLE [dbo].[Shift]  WITH CHECK ADD  CONSTRAINT [FK_Shift_Driver_Driver_id] FOREIGN KEY([driver_id])
REFERENCES [dbo].[Driver] ([driver_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Shift] CHECK CONSTRAINT [FK_Shift_Driver_Driver_id]
GO
USE [master]
GO
ALTER DATABASE [TaxiOperatorDb] SET  READ_WRITE 
GO
