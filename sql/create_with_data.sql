USE [master]
GO
/****** Object:  Database [Favourites]    Script Date: 09/05/2016 18:15:46 ******/
CREATE DATABASE [Favourites]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Favourites', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Favourites.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Favourites_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Favourites_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Favourites] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Favourites].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Favourites] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Favourites] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Favourites] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Favourites] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Favourites] SET ARITHABORT OFF 
GO
ALTER DATABASE [Favourites] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Favourites] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Favourites] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Favourites] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Favourites] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Favourites] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Favourites] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Favourites] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Favourites] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Favourites] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Favourites] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Favourites] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Favourites] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Favourites] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Favourites] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Favourites] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Favourites] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Favourites] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Favourites] SET  MULTI_USER 
GO
ALTER DATABASE [Favourites] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Favourites] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Favourites] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Favourites] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Favourites] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Favourites]
GO
/****** Object:  Table [dbo].[Favourites]    Script Date: 09/05/2016 18:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Favourites](
	[Id] [uniqueidentifier] NOT NULL,
	[Sedol] [nvarchar](50) NULL,
	[HierarchyId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hierarchy]    Script Date: 09/05/2016 18:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hierarchy](
	[Id] [uniqueidentifier] NOT NULL,
	[ParentId] [uniqueidentifier] NULL,
	[Description] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
INSERT [dbo].[Favourites] ([Id], [Sedol], [HierarchyId]) VALUES (N'1c670f93-998c-4a9e-aceb-be3660ec4664', N'User Sedol 1', N'9aab5d59-e96d-42e5-911d-b8edc9ce39f3')
GO
INSERT [dbo].[Favourites] ([Id], [Sedol], [HierarchyId]) VALUES (N'0723e06b-ca78-494e-b2aa-5086f5781638', N'User Sedol 2', N'9aab5d59-e96d-42e5-911d-b8edc9ce39f3')
GO
INSERT [dbo].[Favourites] ([Id], [Sedol], [HierarchyId]) VALUES (N'92910baf-1e74-4c04-85e2-5ea3f2265ffe', N'User Sedol 3', N'9aab5d59-e96d-42e5-911d-b8edc9ce39f3')
GO
INSERT [dbo].[Favourites] ([Id], [Sedol], [HierarchyId]) VALUES (N'3ea81600-2f09-4a3a-8ced-f28b3e02f6f3', N'Sub Sedol 1', N'21b7e7ff-f375-46fa-be0e-d84ecb4cdef5')
GO
INSERT [dbo].[Favourites] ([Id], [Sedol], [HierarchyId]) VALUES (N'8c94cc61-2935-44c9-bc38-1daab495c7c3', N'Sub Sedol 2', N'21b7e7ff-f375-46fa-be0e-d84ecb4cdef5')
GO
INSERT [dbo].[Favourites] ([Id], [Sedol], [HierarchyId]) VALUES (N'05a8fa16-e791-4755-a6e4-be777f6b9b77', N'Sub Sedol 3', N'21b7e7ff-f375-46fa-be0e-d84ecb4cdef5')
GO
INSERT [dbo].[Favourites] ([Id], [Sedol], [HierarchyId]) VALUES (N'0b01c532-0c4c-4173-acb1-3d5f00bdeca5', N'Company Sedol 1', N'7ea2c2f9-7e4e-4e15-a643-da4f0020231e')
GO
INSERT [dbo].[Favourites] ([Id], [Sedol], [HierarchyId]) VALUES (N'995fb161-dfe6-4fd2-b0bd-22505ecb2194', N'Company Sedol 2', N'7ea2c2f9-7e4e-4e15-a643-da4f0020231e')
GO
INSERT [dbo].[Favourites] ([Id], [Sedol], [HierarchyId]) VALUES (N'2a4561e7-0f8b-477c-9875-0e1d03b60a92', N'Company Sedol 3', N'7ea2c2f9-7e4e-4e15-a643-da4f0020231e')
GO
INSERT [dbo].[Favourites] ([Id], [Sedol], [HierarchyId]) VALUES (N'5109adc3-e08d-4999-9042-bd46488a0c5e', N'Company 2 Sedol 1', N'cdc0df6e-5c10-4eee-bb4d-2f8a190a8727')
GO
INSERT [dbo].[Favourites] ([Id], [Sedol], [HierarchyId]) VALUES (N'c79efa2b-fe92-4885-a151-1b7113d9c511', N'Company 2 Sedol 2', N'cdc0df6e-5c10-4eee-bb4d-2f8a190a8727')
GO
INSERT [dbo].[Favourites] ([Id], [Sedol], [HierarchyId]) VALUES (N'5c0ae955-48a5-4141-9929-c48495bd1877', N'Company 2 Sedol 3', N'cdc0df6e-5c10-4eee-bb4d-2f8a190a8727')
GO
INSERT [dbo].[Hierarchy] ([Id], [ParentId], [Description]) VALUES (N'9aab5d59-e96d-42e5-911d-b8edc9ce39f3', N'21b7e7ff-f375-46fa-be0e-d84ecb4cdef5', N'User')
GO
INSERT [dbo].[Hierarchy] ([Id], [ParentId], [Description]) VALUES (N'21b7e7ff-f375-46fa-be0e-d84ecb4cdef5', N'7ea2c2f9-7e4e-4e15-a643-da4f0020231e', N'Sub Company')
GO
INSERT [dbo].[Hierarchy] ([Id], [ParentId], [Description]) VALUES (N'7ea2c2f9-7e4e-4e15-a643-da4f0020231e', NULL, N'Company')
GO
INSERT [dbo].[Hierarchy] ([Id], [ParentId], [Description]) VALUES (N'fd149e82-6e93-49a5-8573-41fc0b1b5957', N'21b7e7ff-f375-46fa-be0e-d84ecb4cdef5', N'User')
GO
INSERT [dbo].[Hierarchy] ([Id], [ParentId], [Description]) VALUES (N'cdc0df6e-5c10-4eee-bb4d-2f8a190a8727', NULL, N'Company')
GO
INSERT [dbo].[Hierarchy] ([Id], [ParentId], [Description]) VALUES (N'a5fc912e-baef-46da-b48b-ae84be303740', N'cdc0df6e-5c10-4eee-bb4d-2f8a190a8727', N'Sub Company')
GO
INSERT [dbo].[Hierarchy] ([Id], [ParentId], [Description]) VALUES (N'0905c1ee-4f76-43b5-954a-dbd062c3b1cc', N'a5fc912e-baef-46da-b48b-ae84be303740', N'User')
GO
USE [master]
GO
ALTER DATABASE [Favourites] SET  READ_WRITE 
GO
