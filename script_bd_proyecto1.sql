USE [master]
GO
/****** Object:  Database [p_buses]    Script Date: 11/11/2024 18:39:39 ******/
CREATE DATABASE [p_buses]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'p_buses', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\p_buses.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'p_buses_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\p_buses_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [p_buses] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [p_buses].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [p_buses] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [p_buses] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [p_buses] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [p_buses] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [p_buses] SET ARITHABORT OFF 
GO
ALTER DATABASE [p_buses] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [p_buses] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [p_buses] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [p_buses] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [p_buses] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [p_buses] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [p_buses] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [p_buses] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [p_buses] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [p_buses] SET  DISABLE_BROKER 
GO
ALTER DATABASE [p_buses] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [p_buses] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [p_buses] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [p_buses] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [p_buses] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [p_buses] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [p_buses] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [p_buses] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [p_buses] SET  MULTI_USER 
GO
ALTER DATABASE [p_buses] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [p_buses] SET DB_CHAINING OFF 
GO
ALTER DATABASE [p_buses] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [p_buses] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [p_buses] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [p_buses] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [p_buses] SET QUERY_STORE = ON
GO
ALTER DATABASE [p_buses] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [p_buses]
GO
/****** Object:  Table [dbo].[tbl_asientos_x_reserva]    Script Date: 11/11/2024 18:39:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_asientos_x_reserva](
	[id_asiento] [varchar](5) NOT NULL,
	[id_reserva] [varchar](5) NOT NULL,
	[tipo_asiento] [varchar](1) NOT NULL,
 CONSTRAINT [pk_asientos] PRIMARY KEY CLUSTERED 
(
	[id_asiento] ASC,
	[id_reserva] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_buses]    Script Date: 11/11/2024 18:39:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_buses](
	[id_bus] [varchar](5) NOT NULL,
	[capacidad] [varchar](5) NOT NULL,
 CONSTRAINT [pk_buses] PRIMARY KEY CLUSTERED 
(
	[id_bus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_historial_pagos]    Script Date: 11/11/2024 18:39:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_historial_pagos](
	[id_historial] [varchar](5) NOT NULL,
	[id_reserva] [varchar](5) NOT NULL,
	[monto] [numeric](14, 2) NOT NULL,
	[fecha_pago] [date] NOT NULL,
 CONSTRAINT [pk_historial] PRIMARY KEY CLUSTERED 
(
	[id_historial] ASC,
	[id_reserva] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_horarios]    Script Date: 11/11/2024 18:39:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_horarios](
	[id_horario] [varchar](5) NOT NULL,
	[id_ruta] [varchar](5) NOT NULL,
	[hora] [time](7) NOT NULL,
 CONSTRAINT [pk_horarios] PRIMARY KEY CLUSTERED 
(
	[id_horario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_horarios_x_buses]    Script Date: 11/11/2024 18:39:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_horarios_x_buses](
	[id_horario] [varchar](5) NOT NULL,
	[id_bus] [varchar](5) NOT NULL,
	[asientos_disponibles] [numeric](5, 0) NOT NULL,
 CONSTRAINT [pk_horarios_buses] PRIMARY KEY CLUSTERED 
(
	[id_horario] ASC,
	[id_bus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_lugares]    Script Date: 11/11/2024 18:39:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_lugares](
	[id_lugar] [varchar](5) NOT NULL,
	[descripcion] [varchar](300) NOT NULL,
 CONSTRAINT [pk_lugares] PRIMARY KEY CLUSTERED 
(
	[id_lugar] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_metodos_pago]    Script Date: 11/11/2024 18:39:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_metodos_pago](
	[id_metodo_pago] [varchar](5) NOT NULL,
	[descripcion] [varchar](30) NOT NULL,
 CONSTRAINT [pk_metodos_pago] PRIMARY KEY CLUSTERED 
(
	[id_metodo_pago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_personas]    Script Date: 11/11/2024 18:39:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_personas](
	[id_persona] [varchar](5) NOT NULL,
	[id_metodo_pago] [varchar](5) NOT NULL,
	[numero_identificacion] [varchar](30) NOT NULL,
	[primer_nombre] [varchar](30) NOT NULL,
	[segundo_nombre] [varchar](30) NULL,
	[primer_apellido] [varchar](30) NOT NULL,
	[segundo_apellido] [varchar](30) NULL,
 CONSTRAINT [pk_personas] PRIMARY KEY CLUSTERED 
(
	[id_persona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_promociones]    Script Date: 11/11/2024 18:39:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_promociones](
	[id_promocion] [varchar](5) NOT NULL,
	[descripcion] [varchar](500) NOT NULL,
	[descuento] [varchar](5) NOT NULL,
	[fecha_inicial] [date] NOT NULL,
	[fecha_final] [date] NOT NULL,
 CONSTRAINT [pk_promociones] PRIMARY KEY CLUSTERED 
(
	[id_promocion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_promociones_x_ruta]    Script Date: 11/11/2024 18:39:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_promociones_x_ruta](
	[id_promocion] [varchar](5) NOT NULL,
	[id_ruta] [varchar](5) NOT NULL,
 CONSTRAINT [pk_promo_rura] PRIMARY KEY CLUSTERED 
(
	[id_promocion] ASC,
	[id_ruta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_reservas]    Script Date: 11/11/2024 18:39:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_reservas](
	[id_reserva] [varchar](5) NOT NULL,
	[id_horario] [varchar](5) NOT NULL,
	[id_usuario] [varchar](5) NOT NULL,
	[estado_pago] [varchar](1) NOT NULL,
	[cantidad_asientos] [decimal](5, 0) NOT NULL,
	[fecha] [date] NOT NULL,
	[hora] [time](7) NOT NULL,
 CONSTRAINT [PK_tbl_reservas] PRIMARY KEY CLUSTERED 
(
	[id_reserva] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_rutas]    Script Date: 11/11/2024 18:39:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_rutas](
	[id_ruta] [varchar](5) NOT NULL,
	[id_lugar_origen] [varchar](5) NOT NULL,
	[id_lugar_destino] [varchar](5) NOT NULL,
	[precio] [numeric](14, 2) NOT NULL,
 CONSTRAINT [pk_rutas] PRIMARY KEY CLUSTERED 
(
	[id_ruta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_usuarios]    Script Date: 11/11/2024 18:39:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_usuarios](
	[id_usuario] [varchar](5) NOT NULL,
	[id_persona] [varchar](5) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[contrasena] [varchar](10) NOT NULL,
	[estado] [varchar](1) NOT NULL,
 CONSTRAINT [pk_usuarios] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[tbl_buses] ([id_bus], [capacidad]) VALUES (N'1', N'28')
INSERT [dbo].[tbl_buses] ([id_bus], [capacidad]) VALUES (N'2', N'28')
INSERT [dbo].[tbl_buses] ([id_bus], [capacidad]) VALUES (N'3', N'28')
GO
INSERT [dbo].[tbl_historial_pagos] ([id_historial], [id_reserva], [monto], [fecha_pago]) VALUES (N'1', N'1', CAST(20800.00 AS Numeric(14, 2)), CAST(N'2024-11-24' AS Date))
GO
INSERT [dbo].[tbl_horarios] ([id_horario], [id_ruta], [hora]) VALUES (N'1', N'1', CAST(N'06:30:00' AS Time))
INSERT [dbo].[tbl_horarios] ([id_horario], [id_ruta], [hora]) VALUES (N'10', N'4', CAST(N'06:40:00' AS Time))
INSERT [dbo].[tbl_horarios] ([id_horario], [id_ruta], [hora]) VALUES (N'11', N'4', CAST(N'11:30:00' AS Time))
INSERT [dbo].[tbl_horarios] ([id_horario], [id_ruta], [hora]) VALUES (N'12', N'4', CAST(N'17:30:00' AS Time))
INSERT [dbo].[tbl_horarios] ([id_horario], [id_ruta], [hora]) VALUES (N'13', N'5', CAST(N'07:45:00' AS Time))
INSERT [dbo].[tbl_horarios] ([id_horario], [id_ruta], [hora]) VALUES (N'14', N'5', CAST(N'12:30:00' AS Time))
INSERT [dbo].[tbl_horarios] ([id_horario], [id_ruta], [hora]) VALUES (N'15', N'5', CAST(N'15:30:00' AS Time))
INSERT [dbo].[tbl_horarios] ([id_horario], [id_ruta], [hora]) VALUES (N'16', N'6', CAST(N'07:45:00' AS Time))
INSERT [dbo].[tbl_horarios] ([id_horario], [id_ruta], [hora]) VALUES (N'17', N'6', CAST(N'12:30:00' AS Time))
INSERT [dbo].[tbl_horarios] ([id_horario], [id_ruta], [hora]) VALUES (N'18', N'6', CAST(N'15:30:00' AS Time))
INSERT [dbo].[tbl_horarios] ([id_horario], [id_ruta], [hora]) VALUES (N'2', N'1', CAST(N'13:30:00' AS Time))
INSERT [dbo].[tbl_horarios] ([id_horario], [id_ruta], [hora]) VALUES (N'3', N'1', CAST(N'16:30:00' AS Time))
INSERT [dbo].[tbl_horarios] ([id_horario], [id_ruta], [hora]) VALUES (N'4', N'2', CAST(N'06:30:00' AS Time))
INSERT [dbo].[tbl_horarios] ([id_horario], [id_ruta], [hora]) VALUES (N'5', N'2', CAST(N'13:30:00' AS Time))
INSERT [dbo].[tbl_horarios] ([id_horario], [id_ruta], [hora]) VALUES (N'6', N'2', CAST(N'16:30:00' AS Time))
INSERT [dbo].[tbl_horarios] ([id_horario], [id_ruta], [hora]) VALUES (N'7', N'3', CAST(N'06:40:00' AS Time))
INSERT [dbo].[tbl_horarios] ([id_horario], [id_ruta], [hora]) VALUES (N'8', N'3', CAST(N'11:30:00' AS Time))
INSERT [dbo].[tbl_horarios] ([id_horario], [id_ruta], [hora]) VALUES (N'9', N'3', CAST(N'17:30:00' AS Time))
GO
INSERT [dbo].[tbl_horarios_x_buses] ([id_horario], [id_bus], [asientos_disponibles]) VALUES (N'1', N'1', CAST(24 AS Numeric(5, 0)))
INSERT [dbo].[tbl_horarios_x_buses] ([id_horario], [id_bus], [asientos_disponibles]) VALUES (N'10', N'2', CAST(28 AS Numeric(5, 0)))
INSERT [dbo].[tbl_horarios_x_buses] ([id_horario], [id_bus], [asientos_disponibles]) VALUES (N'11', N'2', CAST(28 AS Numeric(5, 0)))
INSERT [dbo].[tbl_horarios_x_buses] ([id_horario], [id_bus], [asientos_disponibles]) VALUES (N'12', N'2', CAST(28 AS Numeric(5, 0)))
INSERT [dbo].[tbl_horarios_x_buses] ([id_horario], [id_bus], [asientos_disponibles]) VALUES (N'13', N'3', CAST(25 AS Numeric(5, 0)))
INSERT [dbo].[tbl_horarios_x_buses] ([id_horario], [id_bus], [asientos_disponibles]) VALUES (N'14', N'3', CAST(27 AS Numeric(5, 0)))
INSERT [dbo].[tbl_horarios_x_buses] ([id_horario], [id_bus], [asientos_disponibles]) VALUES (N'15', N'3', CAST(25 AS Numeric(5, 0)))
INSERT [dbo].[tbl_horarios_x_buses] ([id_horario], [id_bus], [asientos_disponibles]) VALUES (N'16', N'3', CAST(28 AS Numeric(5, 0)))
INSERT [dbo].[tbl_horarios_x_buses] ([id_horario], [id_bus], [asientos_disponibles]) VALUES (N'17', N'3', CAST(25 AS Numeric(5, 0)))
INSERT [dbo].[tbl_horarios_x_buses] ([id_horario], [id_bus], [asientos_disponibles]) VALUES (N'18', N'3', CAST(27 AS Numeric(5, 0)))
INSERT [dbo].[tbl_horarios_x_buses] ([id_horario], [id_bus], [asientos_disponibles]) VALUES (N'2', N'1', CAST(28 AS Numeric(5, 0)))
INSERT [dbo].[tbl_horarios_x_buses] ([id_horario], [id_bus], [asientos_disponibles]) VALUES (N'3', N'1', CAST(28 AS Numeric(5, 0)))
INSERT [dbo].[tbl_horarios_x_buses] ([id_horario], [id_bus], [asientos_disponibles]) VALUES (N'4', N'1', CAST(28 AS Numeric(5, 0)))
INSERT [dbo].[tbl_horarios_x_buses] ([id_horario], [id_bus], [asientos_disponibles]) VALUES (N'5', N'1', CAST(28 AS Numeric(5, 0)))
INSERT [dbo].[tbl_horarios_x_buses] ([id_horario], [id_bus], [asientos_disponibles]) VALUES (N'6', N'1', CAST(28 AS Numeric(5, 0)))
INSERT [dbo].[tbl_horarios_x_buses] ([id_horario], [id_bus], [asientos_disponibles]) VALUES (N'7', N'2', CAST(28 AS Numeric(5, 0)))
INSERT [dbo].[tbl_horarios_x_buses] ([id_horario], [id_bus], [asientos_disponibles]) VALUES (N'8', N'2', CAST(28 AS Numeric(5, 0)))
INSERT [dbo].[tbl_horarios_x_buses] ([id_horario], [id_bus], [asientos_disponibles]) VALUES (N'9', N'2', CAST(28 AS Numeric(5, 0)))
GO
INSERT [dbo].[tbl_lugares] ([id_lugar], [descripcion]) VALUES (N'1', N'Pérez Zeledón')
INSERT [dbo].[tbl_lugares] ([id_lugar], [descripcion]) VALUES (N'2', N'San José')
INSERT [dbo].[tbl_lugares] ([id_lugar], [descripcion]) VALUES (N'3', N'Los Santos')
INSERT [dbo].[tbl_lugares] ([id_lugar], [descripcion]) VALUES (N'4', N'Cartago')
GO
INSERT [dbo].[tbl_metodos_pago] ([id_metodo_pago], [descripcion]) VALUES (N'1', N'Tarjeta de Crédito')
INSERT [dbo].[tbl_metodos_pago] ([id_metodo_pago], [descripcion]) VALUES (N'2', N'prueba')
GO
INSERT [dbo].[tbl_personas] ([id_persona], [id_metodo_pago], [numero_identificacion], [primer_nombre], [segundo_nombre], [primer_apellido], [segundo_apellido]) VALUES (N'1', N'1', N'1157507878', N'Kenia', N'maria', N'Agüero', N'Barrios')
GO
INSERT [dbo].[tbl_reservas] ([id_reserva], [id_horario], [id_usuario], [estado_pago], [cantidad_asientos], [fecha], [hora]) VALUES (N'1', N'1', N'1', N'C', CAST(4 AS Decimal(5, 0)), CAST(N'2024-11-24' AS Date), CAST(N'06:30:00' AS Time))
GO
INSERT [dbo].[tbl_rutas] ([id_ruta], [id_lugar_origen], [id_lugar_destino], [precio]) VALUES (N'1', N'1', N'2', CAST(5200.00 AS Numeric(14, 2)))
INSERT [dbo].[tbl_rutas] ([id_ruta], [id_lugar_origen], [id_lugar_destino], [precio]) VALUES (N'2', N'2', N'1', CAST(5200.00 AS Numeric(14, 2)))
INSERT [dbo].[tbl_rutas] ([id_ruta], [id_lugar_origen], [id_lugar_destino], [precio]) VALUES (N'3', N'3', N'4', CAST(3500.00 AS Numeric(14, 2)))
INSERT [dbo].[tbl_rutas] ([id_ruta], [id_lugar_origen], [id_lugar_destino], [precio]) VALUES (N'4', N'4', N'3', CAST(3500.00 AS Numeric(14, 2)))
INSERT [dbo].[tbl_rutas] ([id_ruta], [id_lugar_origen], [id_lugar_destino], [precio]) VALUES (N'5', N'1', N'4', CAST(4000.00 AS Numeric(14, 2)))
INSERT [dbo].[tbl_rutas] ([id_ruta], [id_lugar_origen], [id_lugar_destino], [precio]) VALUES (N'6', N'4', N'1', CAST(4000.00 AS Numeric(14, 2)))
GO
INSERT [dbo].[tbl_usuarios] ([id_usuario], [id_persona], [email], [contrasena], [estado]) VALUES (N'1', N'1', N'abakm21@gmail.com', N'123', N'A')
GO
ALTER TABLE [dbo].[tbl_asientos_x_reserva]  WITH CHECK ADD  CONSTRAINT [fk_asientos_reserva_01] FOREIGN KEY([id_reserva])
REFERENCES [dbo].[tbl_reservas] ([id_reserva])
GO
ALTER TABLE [dbo].[tbl_asientos_x_reserva] CHECK CONSTRAINT [fk_asientos_reserva_01]
GO
ALTER TABLE [dbo].[tbl_historial_pagos]  WITH CHECK ADD  CONSTRAINT [fk_historial_01] FOREIGN KEY([id_reserva])
REFERENCES [dbo].[tbl_reservas] ([id_reserva])
GO
ALTER TABLE [dbo].[tbl_historial_pagos] CHECK CONSTRAINT [fk_historial_01]
GO
ALTER TABLE [dbo].[tbl_horarios_x_buses]  WITH CHECK ADD  CONSTRAINT [fk_horarios_buses_01] FOREIGN KEY([id_horario])
REFERENCES [dbo].[tbl_horarios] ([id_horario])
GO
ALTER TABLE [dbo].[tbl_horarios_x_buses] CHECK CONSTRAINT [fk_horarios_buses_01]
GO
ALTER TABLE [dbo].[tbl_horarios_x_buses]  WITH CHECK ADD  CONSTRAINT [fk_horarios_buses_02] FOREIGN KEY([id_bus])
REFERENCES [dbo].[tbl_buses] ([id_bus])
GO
ALTER TABLE [dbo].[tbl_horarios_x_buses] CHECK CONSTRAINT [fk_horarios_buses_02]
GO
ALTER TABLE [dbo].[tbl_personas]  WITH CHECK ADD  CONSTRAINT [fk_personas] FOREIGN KEY([id_metodo_pago])
REFERENCES [dbo].[tbl_metodos_pago] ([id_metodo_pago])
GO
ALTER TABLE [dbo].[tbl_personas] CHECK CONSTRAINT [fk_personas]
GO
ALTER TABLE [dbo].[tbl_promociones_x_ruta]  WITH CHECK ADD  CONSTRAINT [fk_promociones_ruta_01] FOREIGN KEY([id_ruta])
REFERENCES [dbo].[tbl_rutas] ([id_ruta])
GO
ALTER TABLE [dbo].[tbl_promociones_x_ruta] CHECK CONSTRAINT [fk_promociones_ruta_01]
GO
ALTER TABLE [dbo].[tbl_promociones_x_ruta]  WITH CHECK ADD  CONSTRAINT [fk_promociones_ruta_02] FOREIGN KEY([id_promocion])
REFERENCES [dbo].[tbl_promociones] ([id_promocion])
GO
ALTER TABLE [dbo].[tbl_promociones_x_ruta] CHECK CONSTRAINT [fk_promociones_ruta_02]
GO
ALTER TABLE [dbo].[tbl_reservas]  WITH CHECK ADD  CONSTRAINT [fk_reservas_01] FOREIGN KEY([id_horario])
REFERENCES [dbo].[tbl_horarios] ([id_horario])
GO
ALTER TABLE [dbo].[tbl_reservas] CHECK CONSTRAINT [fk_reservas_01]
GO
ALTER TABLE [dbo].[tbl_reservas]  WITH CHECK ADD  CONSTRAINT [fk_reservas_02] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[tbl_usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[tbl_reservas] CHECK CONSTRAINT [fk_reservas_02]
GO
ALTER TABLE [dbo].[tbl_rutas]  WITH CHECK ADD  CONSTRAINT [fk_rutas_01] FOREIGN KEY([id_lugar_origen])
REFERENCES [dbo].[tbl_lugares] ([id_lugar])
GO
ALTER TABLE [dbo].[tbl_rutas] CHECK CONSTRAINT [fk_rutas_01]
GO
ALTER TABLE [dbo].[tbl_rutas]  WITH CHECK ADD  CONSTRAINT [fk_rutas_02] FOREIGN KEY([id_lugar_destino])
REFERENCES [dbo].[tbl_lugares] ([id_lugar])
GO
ALTER TABLE [dbo].[tbl_rutas] CHECK CONSTRAINT [fk_rutas_02]
GO
ALTER TABLE [dbo].[tbl_usuarios]  WITH CHECK ADD  CONSTRAINT [fk_usuarios] FOREIGN KEY([id_persona])
REFERENCES [dbo].[tbl_personas] ([id_persona])
GO
ALTER TABLE [dbo].[tbl_usuarios] CHECK CONSTRAINT [fk_usuarios]
GO
ALTER TABLE [dbo].[tbl_reservas]  WITH CHECK ADD  CONSTRAINT [ck_estado_pago] CHECK  (([estado_pago]='C' OR [estado_pago]='P'))
GO
ALTER TABLE [dbo].[tbl_reservas] CHECK CONSTRAINT [ck_estado_pago]
GO
ALTER TABLE [dbo].[tbl_usuarios]  WITH CHECK ADD  CONSTRAINT [ck_estado] CHECK  (([estado]='I' OR [estado]='A'))
GO
ALTER TABLE [dbo].[tbl_usuarios] CHECK CONSTRAINT [ck_estado]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del asiento' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_asientos_x_reserva', @level2type=N'COLUMN',@level2name=N'id_asiento'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la reserva' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_asientos_x_reserva', @level2type=N'COLUMN',@level2name=N'id_reserva'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de asiento. Posibles valores: P (Preferencial), C (Comun)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_asientos_x_reserva', @level2type=N'COLUMN',@level2name=N'tipo_asiento'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del bus' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_buses', @level2type=N'COLUMN',@level2name=N'id_bus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Capacidad maxima del bus' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_buses', @level2type=N'COLUMN',@level2name=N'capacidad'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del historial' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_historial_pagos', @level2type=N'COLUMN',@level2name=N'id_historial'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la reserva' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_historial_pagos', @level2type=N'COLUMN',@level2name=N'id_reserva'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Monto total de la reserva' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_historial_pagos', @level2type=N'COLUMN',@level2name=N'monto'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha en que se realizo el pago de la compra' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_historial_pagos', @level2type=N'COLUMN',@level2name=N'fecha_pago'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del horario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_horarios', @level2type=N'COLUMN',@level2name=N'id_horario'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la ruta' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_horarios', @level2type=N'COLUMN',@level2name=N'id_ruta'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hora de la ruta' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_horarios', @level2type=N'COLUMN',@level2name=N'hora'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del horario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_horarios_x_buses', @level2type=N'COLUMN',@level2name=N'id_horario'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del bus' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_horarios_x_buses', @level2type=N'COLUMN',@level2name=N'id_bus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Representa la cantidad de asientos disponibles por horario de cada bus' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_horarios_x_buses', @level2type=N'COLUMN',@level2name=N'asientos_disponibles'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la tabla' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_metodos_pago', @level2type=N'COLUMN',@level2name=N'id_metodo_pago'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del metodo de pago' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_metodos_pago', @level2type=N'COLUMN',@level2name=N'descripcion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la tabla' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_personas', @level2type=N'COLUMN',@level2name=N'id_persona'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Referencia el metodo de pago seleccionado por la persona' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_personas', @level2type=N'COLUMN',@level2name=N'id_metodo_pago'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Corresponde al numero de identificacion de la persona' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_personas', @level2type=N'COLUMN',@level2name=N'numero_identificacion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Corresponde al primer nombre de la persona' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_personas', @level2type=N'COLUMN',@level2name=N'primer_nombre'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Corresponde al segundo nombre de la persona' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_personas', @level2type=N'COLUMN',@level2name=N'segundo_nombre'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Corresponde al primer apellido de la persona' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_personas', @level2type=N'COLUMN',@level2name=N'primer_apellido'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Corresponde al segundo apellido de la persona' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_personas', @level2type=N'COLUMN',@level2name=N'segundo_apellido'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion de la promocion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_promociones', @level2type=N'COLUMN',@level2name=N'descripcion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Porcentaje de descuento de la promocion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_promociones', @level2type=N'COLUMN',@level2name=N'descuento'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha inicial de la promocion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_promociones', @level2type=N'COLUMN',@level2name=N'fecha_inicial'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha final de la promocion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_promociones', @level2type=N'COLUMN',@level2name=N'fecha_final'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la promocion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_promociones_x_ruta', @level2type=N'COLUMN',@level2name=N'id_promocion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la ruta' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_promociones_x_ruta', @level2type=N'COLUMN',@level2name=N'id_ruta'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la tabla' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_reservas', @level2type=N'COLUMN',@level2name=N'id_reserva'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Representa el horario de la reserva' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_reservas', @level2type=N'COLUMN',@level2name=N'id_horario'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Corresponde al usuario que reserva' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_reservas', @level2type=N'COLUMN',@level2name=N'id_usuario'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Corresponde al estado del pago. Posibles valores: P (Pendiente), C (Pagado).' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_reservas', @level2type=N'COLUMN',@level2name=N'estado_pago'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cantidad de asientos' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_reservas', @level2type=N'COLUMN',@level2name=N'cantidad_asientos'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de la reserva' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_reservas', @level2type=N'COLUMN',@level2name=N'fecha'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hora de la reserva' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_reservas', @level2type=N'COLUMN',@level2name=N'hora'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la tabla' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_rutas', @level2type=N'COLUMN',@level2name=N'id_ruta'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Corresponde al identificador del lugar de origen' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_rutas', @level2type=N'COLUMN',@level2name=N'id_lugar_origen'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Corresponde al identificador del lugar de destino' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_rutas', @level2type=N'COLUMN',@level2name=N'id_lugar_destino'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Precio del ticket de la ruta' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_rutas', @level2type=N'COLUMN',@level2name=N'precio'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la tabla' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_usuarios', @level2type=N'COLUMN',@level2name=N'id_usuario'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la persona' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_usuarios', @level2type=N'COLUMN',@level2name=N'id_persona'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Corresponde al correo del usuario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_usuarios', @level2type=N'COLUMN',@level2name=N'email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Corresponde a la contraseña del usuario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_usuarios', @level2type=N'COLUMN',@level2name=N'contrasena'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Representa el estado del usuario. Posibles valores: A (Activo), I (Inactivo) ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_usuarios', @level2type=N'COLUMN',@level2name=N'estado'
GO
USE [master]
GO
ALTER DATABASE [p_buses] SET  READ_WRITE 
GO
