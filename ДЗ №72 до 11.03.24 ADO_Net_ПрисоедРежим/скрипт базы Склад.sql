USE [master]
GO
/****** Object:  Database [Склад]    Script Date: 11.03.2024 2:51:59 ******/
CREATE DATABASE [Склад]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Склад', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Склад.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Склад_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Склад_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Склад] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Склад].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Склад] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Склад] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Склад] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Склад] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Склад] SET ARITHABORT OFF 
GO
ALTER DATABASE [Склад] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Склад] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Склад] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Склад] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Склад] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Склад] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Склад] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Склад] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Склад] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Склад] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Склад] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Склад] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Склад] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Склад] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Склад] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Склад] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Склад] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Склад] SET RECOVERY FULL 
GO
ALTER DATABASE [Склад] SET  MULTI_USER 
GO
ALTER DATABASE [Склад] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Склад] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Склад] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Склад] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Склад] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Склад] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Склад', N'ON'
GO
ALTER DATABASE [Склад] SET QUERY_STORE = OFF
GO
USE [Склад]
GO
/****** Object:  Table [dbo].[Поставки]    Script Date: 11.03.2024 2:52:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Поставки](
	[ID_поставки] [int] IDENTITY(1,1) NOT NULL,
	[ДатаПоставки] [date] NULL,
	[Себестоимость] [decimal](6, 2) NULL,
	[Количество] [int] NULL,
 CONSTRAINT [PK_Поставки] PRIMARY KEY CLUSTERED 
(
	[ID_поставки] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ПоставкиПоставщик]    Script Date: 11.03.2024 2:52:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ПоставкиПоставщик](
	[ID_Пост_Пост] [int] IDENTITY(1,1) NOT NULL,
	[ID_поставки] [int] NULL,
	[ID_поставщика] [int] NULL,
 CONSTRAINT [PK_ПоставкиПоставщик] PRIMARY KEY CLUSTERED 
(
	[ID_Пост_Пост] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Поставщики]    Script Date: 11.03.2024 2:52:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Поставщики](
	[ID_поставщика] [int] IDENTITY(1,1) NOT NULL,
	[ОПФ] [nvarchar](100) NULL,
	[Наименование_поставщика] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_поставщика] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ТоварПоставка]    Script Date: 11.03.2024 2:52:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ТоварПоставка](
	[ID_Тов_Пост] [int] NOT NULL,
	[ID_товара] [int] NULL,
	[ID_поставки] [int] NULL,
 CONSTRAINT [PK_ТоварПоставщик] PRIMARY KEY CLUSTERED 
(
	[ID_Тов_Пост] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Товары]    Script Date: 11.03.2024 2:52:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Товары](
	[ID_товара] [int] IDENTITY(1,1) NOT NULL,
	[Тип] [nvarchar](50) NULL,
	[Название_товара] [nvarchar](100) NULL,
 CONSTRAINT [PK__Товары__CECF3F04013C2854] PRIMARY KEY CLUSTERED 
(
	[ID_товара] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Поставки] ON 

INSERT [dbo].[Поставки] ([ID_поставки], [ДатаПоставки], [Себестоимость], [Количество]) VALUES (1, CAST(N'2023-11-01' AS Date), CAST(100.00 AS Decimal(6, 2)), 10)
INSERT [dbo].[Поставки] ([ID_поставки], [ДатаПоставки], [Себестоимость], [Количество]) VALUES (2, CAST(N'2023-11-01' AS Date), CAST(200.00 AS Decimal(6, 2)), 15)
INSERT [dbo].[Поставки] ([ID_поставки], [ДатаПоставки], [Себестоимость], [Количество]) VALUES (3, CAST(N'2023-12-05' AS Date), CAST(150.00 AS Decimal(6, 2)), 15)
INSERT [dbo].[Поставки] ([ID_поставки], [ДатаПоставки], [Себестоимость], [Количество]) VALUES (4, CAST(N'2023-12-25' AS Date), CAST(25.00 AS Decimal(6, 2)), 20)
INSERT [dbo].[Поставки] ([ID_поставки], [ДатаПоставки], [Себестоимость], [Количество]) VALUES (5, CAST(N'2024-01-15' AS Date), CAST(300.00 AS Decimal(6, 2)), 35)
INSERT [dbo].[Поставки] ([ID_поставки], [ДатаПоставки], [Себестоимость], [Количество]) VALUES (6, CAST(N'2024-01-29' AS Date), CAST(2000.00 AS Decimal(6, 2)), 10)
INSERT [dbo].[Поставки] ([ID_поставки], [ДатаПоставки], [Себестоимость], [Количество]) VALUES (7, CAST(N'2024-02-10' AS Date), CAST(1500.00 AS Decimal(6, 2)), 15)
INSERT [dbo].[Поставки] ([ID_поставки], [ДатаПоставки], [Себестоимость], [Количество]) VALUES (8, CAST(N'2024-02-28' AS Date), CAST(450.00 AS Decimal(6, 2)), 27)
INSERT [dbo].[Поставки] ([ID_поставки], [ДатаПоставки], [Себестоимость], [Количество]) VALUES (9, CAST(N'2024-03-01' AS Date), CAST(200.00 AS Decimal(6, 2)), 36)
INSERT [dbo].[Поставки] ([ID_поставки], [ДатаПоставки], [Себестоимость], [Количество]) VALUES (10, CAST(N'2024-03-07' AS Date), CAST(180.00 AS Decimal(6, 2)), 50)
INSERT [dbo].[Поставки] ([ID_поставки], [ДатаПоставки], [Себестоимость], [Количество]) VALUES (11, CAST(N'2024-03-08' AS Date), CAST(90.00 AS Decimal(6, 2)), 20)
INSERT [dbo].[Поставки] ([ID_поставки], [ДатаПоставки], [Себестоимость], [Количество]) VALUES (12, CAST(N'2024-03-08' AS Date), CAST(200.00 AS Decimal(6, 2)), 35)
INSERT [dbo].[Поставки] ([ID_поставки], [ДатаПоставки], [Себестоимость], [Количество]) VALUES (13, CAST(N'2024-03-09' AS Date), CAST(100.00 AS Decimal(6, 2)), 20)
INSERT [dbo].[Поставки] ([ID_поставки], [ДатаПоставки], [Себестоимость], [Количество]) VALUES (14, CAST(N'2024-03-09' AS Date), CAST(300.00 AS Decimal(6, 2)), 100)
INSERT [dbo].[Поставки] ([ID_поставки], [ДатаПоставки], [Себестоимость], [Количество]) VALUES (15, CAST(N'2024-03-09' AS Date), CAST(2200.00 AS Decimal(6, 2)), 2)
SET IDENTITY_INSERT [dbo].[Поставки] OFF
GO
SET IDENTITY_INSERT [dbo].[ПоставкиПоставщик] ON 

INSERT [dbo].[ПоставкиПоставщик] ([ID_Пост_Пост], [ID_поставки], [ID_поставщика]) VALUES (1, 1, 1)
INSERT [dbo].[ПоставкиПоставщик] ([ID_Пост_Пост], [ID_поставки], [ID_поставщика]) VALUES (2, 2, 2)
INSERT [dbo].[ПоставкиПоставщик] ([ID_Пост_Пост], [ID_поставки], [ID_поставщика]) VALUES (3, 3, 3)
INSERT [dbo].[ПоставкиПоставщик] ([ID_Пост_Пост], [ID_поставки], [ID_поставщика]) VALUES (4, 4, 4)
INSERT [dbo].[ПоставкиПоставщик] ([ID_Пост_Пост], [ID_поставки], [ID_поставщика]) VALUES (5, 5, 1)
INSERT [dbo].[ПоставкиПоставщик] ([ID_Пост_Пост], [ID_поставки], [ID_поставщика]) VALUES (6, 6, 2)
INSERT [dbo].[ПоставкиПоставщик] ([ID_Пост_Пост], [ID_поставки], [ID_поставщика]) VALUES (7, 7, 3)
INSERT [dbo].[ПоставкиПоставщик] ([ID_Пост_Пост], [ID_поставки], [ID_поставщика]) VALUES (8, 8, 4)
INSERT [dbo].[ПоставкиПоставщик] ([ID_Пост_Пост], [ID_поставки], [ID_поставщика]) VALUES (9, 9, 1)
INSERT [dbo].[ПоставкиПоставщик] ([ID_Пост_Пост], [ID_поставки], [ID_поставщика]) VALUES (10, 10, 2)
INSERT [dbo].[ПоставкиПоставщик] ([ID_Пост_Пост], [ID_поставки], [ID_поставщика]) VALUES (11, 11, 3)
INSERT [dbo].[ПоставкиПоставщик] ([ID_Пост_Пост], [ID_поставки], [ID_поставщика]) VALUES (12, 12, 4)
INSERT [dbo].[ПоставкиПоставщик] ([ID_Пост_Пост], [ID_поставки], [ID_поставщика]) VALUES (13, 13, 1)
INSERT [dbo].[ПоставкиПоставщик] ([ID_Пост_Пост], [ID_поставки], [ID_поставщика]) VALUES (14, 14, 2)
INSERT [dbo].[ПоставкиПоставщик] ([ID_Пост_Пост], [ID_поставки], [ID_поставщика]) VALUES (15, 15, 3)
SET IDENTITY_INSERT [dbo].[ПоставкиПоставщик] OFF
GO
SET IDENTITY_INSERT [dbo].[Поставщики] ON 

INSERT [dbo].[Поставщики] ([ID_поставщика], [ОПФ], [Наименование_поставщика]) VALUES (1, N'ООО', N'ТоргРесурс')
INSERT [dbo].[Поставщики] ([ID_поставщика], [ОПФ], [Наименование_поставщика]) VALUES (2, N'ЗАО', N'ПоставПром')
INSERT [dbo].[Поставщики] ([ID_поставщика], [ОПФ], [Наименование_поставщика]) VALUES (3, N'ИП', N'Петров А.В.')
INSERT [dbo].[Поставщики] ([ID_поставщика], [ОПФ], [Наименование_поставщика]) VALUES (4, N'ООО', N'ВекторСнаб')
SET IDENTITY_INSERT [dbo].[Поставщики] OFF
GO
INSERT [dbo].[ТоварПоставка] ([ID_Тов_Пост], [ID_товара], [ID_поставки]) VALUES (1, 1, 1)
INSERT [dbo].[ТоварПоставка] ([ID_Тов_Пост], [ID_товара], [ID_поставки]) VALUES (2, 2, 2)
INSERT [dbo].[ТоварПоставка] ([ID_Тов_Пост], [ID_товара], [ID_поставки]) VALUES (3, 3, 3)
INSERT [dbo].[ТоварПоставка] ([ID_Тов_Пост], [ID_товара], [ID_поставки]) VALUES (4, 4, 4)
INSERT [dbo].[ТоварПоставка] ([ID_Тов_Пост], [ID_товара], [ID_поставки]) VALUES (5, 5, 5)
INSERT [dbo].[ТоварПоставка] ([ID_Тов_Пост], [ID_товара], [ID_поставки]) VALUES (6, 6, 6)
INSERT [dbo].[ТоварПоставка] ([ID_Тов_Пост], [ID_товара], [ID_поставки]) VALUES (7, 7, 7)
INSERT [dbo].[ТоварПоставка] ([ID_Тов_Пост], [ID_товара], [ID_поставки]) VALUES (8, 8, 8)
INSERT [dbo].[ТоварПоставка] ([ID_Тов_Пост], [ID_товара], [ID_поставки]) VALUES (9, 9, 9)
INSERT [dbo].[ТоварПоставка] ([ID_Тов_Пост], [ID_товара], [ID_поставки]) VALUES (10, 10, 10)
INSERT [dbo].[ТоварПоставка] ([ID_Тов_Пост], [ID_товара], [ID_поставки]) VALUES (11, 1, 11)
INSERT [dbo].[ТоварПоставка] ([ID_Тов_Пост], [ID_товара], [ID_поставки]) VALUES (12, 2, 12)
INSERT [dbo].[ТоварПоставка] ([ID_Тов_Пост], [ID_товара], [ID_поставки]) VALUES (13, 3, 13)
INSERT [dbo].[ТоварПоставка] ([ID_Тов_Пост], [ID_товара], [ID_поставки]) VALUES (14, 5, 14)
INSERT [dbo].[ТоварПоставка] ([ID_Тов_Пост], [ID_товара], [ID_поставки]) VALUES (15, 6, 15)
GO
SET IDENTITY_INSERT [dbo].[Товары] ON 

INSERT [dbo].[Товары] ([ID_товара], [Тип], [Название_товара]) VALUES (1, N'Посуда', N'Кружка')
INSERT [dbo].[Товары] ([ID_товара], [Тип], [Название_товара]) VALUES (2, N'Посуда', N'Тарелка')
INSERT [dbo].[Товары] ([ID_товара], [Тип], [Название_товара]) VALUES (3, N'Напитки', N'Сок')
INSERT [dbo].[Товары] ([ID_товара], [Тип], [Название_товара]) VALUES (4, N'Напитки', N'Вода')
INSERT [dbo].[Товары] ([ID_товара], [Тип], [Название_товара]) VALUES (5, N'Автотовары', N'Ароматизатор')
INSERT [dbo].[Товары] ([ID_товара], [Тип], [Название_товара]) VALUES (6, N'Автотовары', N'Масло ДВС')
INSERT [dbo].[Товары] ([ID_товара], [Тип], [Название_товара]) VALUES (7, N'Бытовая химия', N'Стиральный порошок')
INSERT [dbo].[Товары] ([ID_товара], [Тип], [Название_товара]) VALUES (8, N'Бытовая химия', N'Чистящее средство')
INSERT [dbo].[Товары] ([ID_товара], [Тип], [Название_товара]) VALUES (9, N'Бытовая химия', N'Средство для мытья пола')
INSERT [dbo].[Товары] ([ID_товара], [Тип], [Название_товара]) VALUES (10, N'Товары для красоты', N'Шампунь')
SET IDENTITY_INSERT [dbo].[Товары] OFF
GO
ALTER TABLE [dbo].[ПоставкиПоставщик]  WITH CHECK ADD  CONSTRAINT [FK_ПоставкиПоставщик_Поставки] FOREIGN KEY([ID_поставки])
REFERENCES [dbo].[Поставки] ([ID_поставки])
GO
ALTER TABLE [dbo].[ПоставкиПоставщик] CHECK CONSTRAINT [FK_ПоставкиПоставщик_Поставки]
GO
ALTER TABLE [dbo].[ПоставкиПоставщик]  WITH CHECK ADD  CONSTRAINT [FK_ПоставкиПоставщик_Поставщики] FOREIGN KEY([ID_поставщика])
REFERENCES [dbo].[Поставщики] ([ID_поставщика])
GO
ALTER TABLE [dbo].[ПоставкиПоставщик] CHECK CONSTRAINT [FK_ПоставкиПоставщик_Поставщики]
GO
ALTER TABLE [dbo].[ТоварПоставка]  WITH CHECK ADD  CONSTRAINT [FK_ТоварПоставка_Поставки] FOREIGN KEY([ID_поставки])
REFERENCES [dbo].[Поставки] ([ID_поставки])
GO
ALTER TABLE [dbo].[ТоварПоставка] CHECK CONSTRAINT [FK_ТоварПоставка_Поставки]
GO
ALTER TABLE [dbo].[ТоварПоставка]  WITH CHECK ADD  CONSTRAINT [FK_ТоварПоставщик_Товары] FOREIGN KEY([ID_товара])
REFERENCES [dbo].[Товары] ([ID_товара])
GO
ALTER TABLE [dbo].[ТоварПоставка] CHECK CONSTRAINT [FK_ТоварПоставщик_Товары]
GO
USE [master]
GO
ALTER DATABASE [Склад] SET  READ_WRITE 
GO
