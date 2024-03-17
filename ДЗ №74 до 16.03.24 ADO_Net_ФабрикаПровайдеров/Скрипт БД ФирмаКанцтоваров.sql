USE [master]
GO
/****** Object:  Database [ФирмаКанцтоваров]    Script Date: 14.03.2024 3:12:37 ******/
CREATE DATABASE [ФирмаКанцтоваров]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ФирмаКанцтоваров', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ФирмаКанцтоваров.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ФирмаКанцтоваров_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ФирмаКанцтоваров_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ФирмаКанцтоваров] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ФирмаКанцтоваров].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ФирмаКанцтоваров] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ФирмаКанцтоваров] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ФирмаКанцтоваров] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ФирмаКанцтоваров] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ФирмаКанцтоваров] SET ARITHABORT OFF 
GO
ALTER DATABASE [ФирмаКанцтоваров] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ФирмаКанцтоваров] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ФирмаКанцтоваров] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ФирмаКанцтоваров] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ФирмаКанцтоваров] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ФирмаКанцтоваров] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ФирмаКанцтоваров] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ФирмаКанцтоваров] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ФирмаКанцтоваров] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ФирмаКанцтоваров] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ФирмаКанцтоваров] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ФирмаКанцтоваров] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ФирмаКанцтоваров] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ФирмаКанцтоваров] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ФирмаКанцтоваров] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ФирмаКанцтоваров] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ФирмаКанцтоваров] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ФирмаКанцтоваров] SET RECOVERY FULL 
GO
ALTER DATABASE [ФирмаКанцтоваров] SET  MULTI_USER 
GO
ALTER DATABASE [ФирмаКанцтоваров] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ФирмаКанцтоваров] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ФирмаКанцтоваров] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ФирмаКанцтоваров] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ФирмаКанцтоваров] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ФирмаКанцтоваров] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ФирмаКанцтоваров', N'ON'
GO
ALTER DATABASE [ФирмаКанцтоваров] SET QUERY_STORE = OFF
GO
USE [ФирмаКанцтоваров]
GO
/****** Object:  UserDefinedFunction [dbo].[GetAverageQuantityByType]    Script Date: 14.03.2024 3:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetAverageQuantityByType](@Тип nvarchar(50))
RETURNS decimal(18, 2)
AS
BEGIN
    DECLARE @averageQuantity decimal(18, 2)

    SELECT @averageQuantity = AVG(Количество) 
    FROM Товары 
    WHERE Тип = @Тип

    RETURN ISNULL(@averageQuantity, 0)
END
GO
/****** Object:  Table [dbo].[Менеджеры]    Script Date: 14.03.2024 3:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Менеджеры](
	[МенеджерID] [int] IDENTITY(1,1) NOT NULL,
	[Имя] [varchar](100) NULL,
	[Фамилия] [varchar](100) NULL,
 CONSTRAINT [PK__Менеджер__3214EC27CD17AAE9] PRIMARY KEY CLUSTERED 
(
	[МенеджерID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Продажи]    Script Date: 14.03.2024 3:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Продажи](
	[ПродажаID] [int] IDENTITY(1,1) NOT NULL,
	[ТоварID] [int] NULL,
	[МенеджерID] [int] NULL,
	[ФирмаID] [int] NULL,
	[КоличествоПроданных] [int] NULL,
	[ЦенаЗаЕдиницу] [decimal](10, 2) NULL,
	[ДатаПродажи] [date] NULL,
 CONSTRAINT [PK__Продажи__3214EC27F6F0416B] PRIMARY KEY CLUSTERED 
(
	[ПродажаID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Товары]    Script Date: 14.03.2024 3:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Товары](
	[ТоварID] [int] IDENTITY(1,1) NOT NULL,
	[Название] [varchar](100) NULL,
	[Тип] [varchar](50) NULL,
	[Количество] [int] NULL,
	[Себестоимость] [decimal](10, 2) NULL,
 CONSTRAINT [PK__Товары__3214EC275C73831D] PRIMARY KEY CLUSTERED 
(
	[ТоварID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Фирмы]    Script Date: 14.03.2024 3:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Фирмы](
	[ФирмаID] [int] IDENTITY(1,1) NOT NULL,
	[Название] [varchar](100) NULL,
 CONSTRAINT [PK__Фирмы__3214EC2797C20E9C] PRIMARY KEY CLUSTERED 
(
	[ФирмаID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Менеджеры] ON 

INSERT [dbo].[Менеджеры] ([МенеджерID], [Имя], [Фамилия]) VALUES (1, N'Иван', N'Иванов')
INSERT [dbo].[Менеджеры] ([МенеджерID], [Имя], [Фамилия]) VALUES (2, N'Петр', N'Петров')
INSERT [dbo].[Менеджеры] ([МенеджерID], [Имя], [Фамилия]) VALUES (3, N'Сергей', N'Сергеев')
INSERT [dbo].[Менеджеры] ([МенеджерID], [Имя], [Фамилия]) VALUES (4, N'Анна', N'Аннова')
INSERT [dbo].[Менеджеры] ([МенеджерID], [Имя], [Фамилия]) VALUES (5, N'Мария', N'Мариева')
SET IDENTITY_INSERT [dbo].[Менеджеры] OFF
GO
SET IDENTITY_INSERT [dbo].[Продажи] ON 

INSERT [dbo].[Продажи] ([ПродажаID], [ТоварID], [МенеджерID], [ФирмаID], [КоличествоПроданных], [ЦенаЗаЕдиницу], [ДатаПродажи]) VALUES (1, 1, 2, 3, 10, CAST(37.87 AS Decimal(10, 2)), CAST(N'2024-02-14' AS Date))
INSERT [dbo].[Продажи] ([ПродажаID], [ТоварID], [МенеджерID], [ФирмаID], [КоличествоПроданных], [ЦенаЗаЕдиницу], [ДатаПродажи]) VALUES (2, 8, 2, 4, 91, CAST(1.72 AS Decimal(10, 2)), CAST(N'2024-01-21' AS Date))
INSERT [dbo].[Продажи] ([ПродажаID], [ТоварID], [МенеджерID], [ФирмаID], [КоличествоПроданных], [ЦенаЗаЕдиницу], [ДатаПродажи]) VALUES (3, 2, 1, 3, 40, CAST(13.20 AS Decimal(10, 2)), CAST(N'2023-12-10' AS Date))
INSERT [dbo].[Продажи] ([ПродажаID], [ТоварID], [МенеджерID], [ФирмаID], [КоличествоПроданных], [ЦенаЗаЕдиницу], [ДатаПродажи]) VALUES (4, 9, 4, 1, 30, CAST(68.54 AS Decimal(10, 2)), CAST(N'2024-02-19' AS Date))
INSERT [dbo].[Продажи] ([ПродажаID], [ТоварID], [МенеджерID], [ФирмаID], [КоличествоПроданных], [ЦенаЗаЕдиницу], [ДатаПродажи]) VALUES (5, 5, 1, 1, 60, CAST(26.87 AS Decimal(10, 2)), CAST(N'2023-11-02' AS Date))
INSERT [dbo].[Продажи] ([ПродажаID], [ТоварID], [МенеджерID], [ФирмаID], [КоличествоПроданных], [ЦенаЗаЕдиницу], [ДатаПродажи]) VALUES (6, 9, 3, 3, 71, CAST(94.80 AS Decimal(10, 2)), CAST(N'2023-12-21' AS Date))
INSERT [dbo].[Продажи] ([ПродажаID], [ТоварID], [МенеджерID], [ФирмаID], [КоличествоПроданных], [ЦенаЗаЕдиницу], [ДатаПродажи]) VALUES (7, 4, 5, 1, 59, CAST(46.76 AS Decimal(10, 2)), CAST(N'2023-12-02' AS Date))
INSERT [dbo].[Продажи] ([ПродажаID], [ТоварID], [МенеджерID], [ФирмаID], [КоличествоПроданных], [ЦенаЗаЕдиницу], [ДатаПродажи]) VALUES (8, 10, 2, 4, 38, CAST(28.27 AS Decimal(10, 2)), CAST(N'2024-01-24' AS Date))
INSERT [dbo].[Продажи] ([ПродажаID], [ТоварID], [МенеджерID], [ФирмаID], [КоличествоПроданных], [ЦенаЗаЕдиницу], [ДатаПродажи]) VALUES (9, 3, 2, 4, 26, CAST(50.59 AS Decimal(10, 2)), CAST(N'2024-02-29' AS Date))
INSERT [dbo].[Продажи] ([ПродажаID], [ТоварID], [МенеджерID], [ФирмаID], [КоличествоПроданных], [ЦенаЗаЕдиницу], [ДатаПродажи]) VALUES (10, 6, 3, 3, 91, CAST(45.34 AS Decimal(10, 2)), CAST(N'2023-12-13' AS Date))
INSERT [dbo].[Продажи] ([ПродажаID], [ТоварID], [МенеджерID], [ФирмаID], [КоличествоПроданных], [ЦенаЗаЕдиницу], [ДатаПродажи]) VALUES (11, 1, 2, 3, 91, CAST(82.95 AS Decimal(10, 2)), CAST(N'2023-12-28' AS Date))
INSERT [dbo].[Продажи] ([ПродажаID], [ТоварID], [МенеджерID], [ФирмаID], [КоличествоПроданных], [ЦенаЗаЕдиницу], [ДатаПродажи]) VALUES (12, 9, 3, 3, 64, CAST(95.77 AS Decimal(10, 2)), CAST(N'2024-02-24' AS Date))
INSERT [dbo].[Продажи] ([ПродажаID], [ТоварID], [МенеджерID], [ФирмаID], [КоличествоПроданных], [ЦенаЗаЕдиницу], [ДатаПродажи]) VALUES (13, 10, 5, 1, 95, CAST(39.53 AS Decimal(10, 2)), CAST(N'2024-01-05' AS Date))
INSERT [dbo].[Продажи] ([ПродажаID], [ТоварID], [МенеджерID], [ФирмаID], [КоличествоПроданных], [ЦенаЗаЕдиницу], [ДатаПродажи]) VALUES (14, 7, 3, 2, 50, CAST(31.67 AS Decimal(10, 2)), CAST(N'2024-02-22' AS Date))
INSERT [dbo].[Продажи] ([ПродажаID], [ТоварID], [МенеджерID], [ФирмаID], [КоличествоПроданных], [ЦенаЗаЕдиницу], [ДатаПродажи]) VALUES (15, 8, 5, 1, 91, CAST(38.20 AS Decimal(10, 2)), CAST(N'2024-02-08' AS Date))
INSERT [dbo].[Продажи] ([ПродажаID], [ТоварID], [МенеджерID], [ФирмаID], [КоличествоПроданных], [ЦенаЗаЕдиницу], [ДатаПродажи]) VALUES (16, 8, 3, 2, 16, CAST(86.99 AS Decimal(10, 2)), CAST(N'2024-01-22' AS Date))
INSERT [dbo].[Продажи] ([ПродажаID], [ТоварID], [МенеджерID], [ФирмаID], [КоличествоПроданных], [ЦенаЗаЕдиницу], [ДатаПродажи]) VALUES (17, 10, 1, 3, 25, CAST(60.27 AS Decimal(10, 2)), CAST(N'2024-02-18' AS Date))
INSERT [dbo].[Продажи] ([ПродажаID], [ТоварID], [МенеджерID], [ФирмаID], [КоличествоПроданных], [ЦенаЗаЕдиницу], [ДатаПродажи]) VALUES (18, 7, 2, 4, 84, CAST(56.69 AS Decimal(10, 2)), CAST(N'2024-01-14' AS Date))
INSERT [dbo].[Продажи] ([ПродажаID], [ТоварID], [МенеджерID], [ФирмаID], [КоличествоПроданных], [ЦенаЗаЕдиницу], [ДатаПродажи]) VALUES (19, 7, 5, 4, 56, CAST(12.83 AS Decimal(10, 2)), CAST(N'2024-01-11' AS Date))
INSERT [dbo].[Продажи] ([ПродажаID], [ТоварID], [МенеджерID], [ФирмаID], [КоличествоПроданных], [ЦенаЗаЕдиницу], [ДатаПродажи]) VALUES (20, 3, 3, 3, 73, CAST(20.78 AS Decimal(10, 2)), CAST(N'2024-02-28' AS Date))
SET IDENTITY_INSERT [dbo].[Продажи] OFF
GO
SET IDENTITY_INSERT [dbo].[Товары] ON 

INSERT [dbo].[Товары] ([ТоварID], [Название], [Тип], [Количество], [Себестоимость]) VALUES (1, N'Ручка', N'Письменные принадлежности', 102, CAST(10.00 AS Decimal(10, 2)))
INSERT [dbo].[Товары] ([ТоварID], [Название], [Тип], [Количество], [Себестоимость]) VALUES (2, N'Карандаш', N'Письменные принадлежности', 200, CAST(5.00 AS Decimal(10, 2)))
INSERT [dbo].[Товары] ([ТоварID], [Название], [Тип], [Количество], [Себестоимость]) VALUES (3, N'Линейка', N'Измерительные инструменты', 150, CAST(7.50 AS Decimal(10, 2)))
INSERT [dbo].[Товары] ([ТоварID], [Название], [Тип], [Количество], [Себестоимость]) VALUES (4, N'Тетрадь', N'Бумажные изделия', 300, CAST(15.00 AS Decimal(10, 2)))
INSERT [dbo].[Товары] ([ТоварID], [Название], [Тип], [Количество], [Себестоимость]) VALUES (5, N'Альбом', N'Бумажные изделия', 120, CAST(20.00 AS Decimal(10, 2)))
INSERT [dbo].[Товары] ([ТоварID], [Название], [Тип], [Количество], [Себестоимость]) VALUES (6, N'Маркер', N'Письменные принадлежности', 110, CAST(12.50 AS Decimal(10, 2)))
INSERT [dbo].[Товары] ([ТоварID], [Название], [Тип], [Количество], [Себестоимость]) VALUES (7, N'Стикеры', N'Бумажные изделия', 500, CAST(8.00 AS Decimal(10, 2)))
INSERT [dbo].[Товары] ([ТоварID], [Название], [Тип], [Количество], [Себестоимость]) VALUES (8, N'Скотч', N'Клейкие ленты', 200, CAST(6.00 AS Decimal(10, 2)))
INSERT [dbo].[Товары] ([ТоварID], [Название], [Тип], [Количество], [Себестоимость]) VALUES (9, N'Клей', N'Клеевые средства', 350, CAST(10.00 AS Decimal(10, 2)))
INSERT [dbo].[Товары] ([ТоварID], [Название], [Тип], [Количество], [Себестоимость]) VALUES (10, N'Бумага', N'Бумажные изделия', 1000, CAST(25.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Товары] OFF
GO
SET IDENTITY_INSERT [dbo].[Фирмы] ON 

INSERT [dbo].[Фирмы] ([ФирмаID], [Название]) VALUES (1, N'ООО Радуга')
INSERT [dbo].[Фирмы] ([ФирмаID], [Название]) VALUES (2, N'ЗАО Сфера')
INSERT [dbo].[Фирмы] ([ФирмаID], [Название]) VALUES (3, N'ООО Стартап')
INSERT [dbo].[Фирмы] ([ФирмаID], [Название]) VALUES (4, N'ООО Прогресс')
SET IDENTITY_INSERT [dbo].[Фирмы] OFF
GO
ALTER TABLE [dbo].[Продажи]  WITH CHECK ADD  CONSTRAINT [FK__Продажи__Менедже__35BCFE0A] FOREIGN KEY([МенеджерID])
REFERENCES [dbo].[Менеджеры] ([МенеджерID])
GO
ALTER TABLE [dbo].[Продажи] CHECK CONSTRAINT [FK__Продажи__Менедже__35BCFE0A]
GO
ALTER TABLE [dbo].[Продажи]  WITH CHECK ADD  CONSTRAINT [FK__Продажи__ТоварID__34C8D9D1] FOREIGN KEY([ТоварID])
REFERENCES [dbo].[Товары] ([ТоварID])
GO
ALTER TABLE [dbo].[Продажи] CHECK CONSTRAINT [FK__Продажи__ТоварID__34C8D9D1]
GO
ALTER TABLE [dbo].[Продажи]  WITH CHECK ADD  CONSTRAINT [FK__Продажи__ФирмаID__36B12243] FOREIGN KEY([ФирмаID])
REFERENCES [dbo].[Фирмы] ([ФирмаID])
GO
ALTER TABLE [dbo].[Продажи] CHECK CONSTRAINT [FK__Продажи__ФирмаID__36B12243]
GO
/****** Object:  Trigger [dbo].[CheckCompanyBeforeSale]    Script Date: 14.03.2024 3:12:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[CheckCompanyBeforeSale]
ON [dbo].[Продажи]
FOR INSERT
AS
BEGIN
    DECLARE @ФирмаID int

    SELECT @ФирмаID = ФирмаID FROM inserted

    IF NOT EXISTS (SELECT 1 FROM Фирмы WHERE ФирмаID = @ФирмаID)
    BEGIN
        RAISERROR ('Фирма с таким ID не существует', 16, 1)
        ROLLBACK TRANSACTION
    END
END
GO
ALTER TABLE [dbo].[Продажи] ENABLE TRIGGER [CheckCompanyBeforeSale]
GO
/****** Object:  Trigger [dbo].[CheckManagerBeforeSale]    Script Date: 14.03.2024 3:12:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[CheckManagerBeforeSale]
ON [dbo].[Продажи]
FOR INSERT
AS
BEGIN
    DECLARE @МенеджерID int

    SELECT @МенеджерID = МенеджерID FROM inserted

    IF NOT EXISTS (SELECT 1 FROM Менеджеры WHERE МенеджерID = @МенеджерID)
    BEGIN
        RAISERROR ('Менеджер с таким ID не существует', 16, 1)
        ROLLBACK TRANSACTION
    END
END
GO
ALTER TABLE [dbo].[Продажи] ENABLE TRIGGER [CheckManagerBeforeSale]
GO
/****** Object:  Trigger [dbo].[CheckProductBeforeSale]    Script Date: 14.03.2024 3:12:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[CheckProductBeforeSale]
ON [dbo].[Продажи]
FOR INSERT
AS
BEGIN
    DECLARE @ТоварID int

    SELECT @ТоварID = ТоварID FROM inserted

    IF NOT EXISTS (SELECT 1 FROM Товары WHERE ТоварID = @ТоварID)
    BEGIN
        RAISERROR ('Товар с таким ID не существует', 16, 1)
        ROLLBACK TRANSACTION
    END
END
GO
ALTER TABLE [dbo].[Продажи] ENABLE TRIGGER [CheckProductBeforeSale]
GO
/****** Object:  Trigger [dbo].[CheckQuantityBeforeSale]    Script Date: 14.03.2024 3:12:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[CheckQuantityBeforeSale]
ON [dbo].[Продажи]
FOR INSERT
AS
BEGIN
    DECLARE @ТоварID int, @КоличествоПроданных int, @Остаток int

    SELECT @ТоварID = ТоварID, @КоличествоПроданных = КоличествоПроданных FROM inserted

    SELECT @Остаток = Количество FROM Товары WHERE ТоварID = @ТоварID

    IF @КоличествоПроданных > @Остаток
    BEGIN
        RAISERROR ('Недостаточно товара на складе', 16, 1)
        ROLLBACK TRANSACTION
    END
END
GO
ALTER TABLE [dbo].[Продажи] ENABLE TRIGGER [CheckQuantityBeforeSale]
GO
USE [master]
GO
ALTER DATABASE [ФирмаКанцтоваров] SET  READ_WRITE 
GO
