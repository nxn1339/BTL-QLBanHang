USE [master]
GO
/****** Object:  Database [QLBanHang]    Script Date: 12/16/2022 7:55:32 PM ******/
CREATE DATABASE [QLBanHang]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QLBanHang', FILENAME = N'E:\SQL\MSSQL15.SQLEXPRESS\MSSQL\DATA\QLBanHang.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QLBanHang_log', FILENAME = N'E:\SQL\MSSQL15.SQLEXPRESS\MSSQL\DATA\QLBanHang_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [QLBanHang] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLBanHang].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLBanHang] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QLBanHang] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QLBanHang] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QLBanHang] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QLBanHang] SET ARITHABORT OFF 
GO
ALTER DATABASE [QLBanHang] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QLBanHang] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QLBanHang] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QLBanHang] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QLBanHang] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QLBanHang] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QLBanHang] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QLBanHang] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QLBanHang] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QLBanHang] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QLBanHang] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QLBanHang] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QLBanHang] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QLBanHang] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QLBanHang] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QLBanHang] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QLBanHang] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QLBanHang] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QLBanHang] SET  MULTI_USER 
GO
ALTER DATABASE [QLBanHang] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QLBanHang] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QLBanHang] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QLBanHang] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QLBanHang] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QLBanHang] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [QLBanHang] SET QUERY_STORE = OFF
GO
USE [QLBanHang]
GO
/****** Object:  Table [dbo].[CHUCVU]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHUCVU](
	[MaCV] [char](10) NOT NULL,
	[TenCV] [nvarchar](50) NOT NULL,
	[LuongCB] [money] NULL,
 CONSTRAINT [PK_CHUCVU_1] PRIMARY KEY CLUSTERED 
(
	[MaCV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CTHOADON]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CTHOADON](
	[MaHD] [char](10) NOT NULL,
	[MaMH] [char](10) NOT NULL,
	[DgBan] [money] NOT NULL,
	[SlBan] [int] NOT NULL,
	[ThanhTien] [money] NULL,
	[TrangThai] [bit] NOT NULL,
 CONSTRAINT [PK_CTHOADON] PRIMARY KEY CLUSTERED 
(
	[MaHD] ASC,
	[MaMH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HOADON]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOADON](
	[MaHD] [char](10) NOT NULL,
	[Ngaylap] [datetime] NULL,
	[MaNV] [char](10) NOT NULL,
	[MaKH] [char](10) NOT NULL,
 CONSTRAINT [PK_HOADON] PRIMARY KEY CLUSTERED 
(
	[MaHD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KHACHHANG]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KHACHHANG](
	[MaKH] [char](10) NOT NULL,
	[TenKH] [nvarchar](50) NOT NULL,
	[DiaChi] [nvarchar](100) NOT NULL,
	[SDT] [varchar](20) NOT NULL,
 CONSTRAINT [PK_KHACHHANG] PRIMARY KEY CLUSTERED 
(
	[MaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MATHANG]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MATHANG](
	[MaMH] [char](10) NOT NULL,
	[TenMH] [nvarchar](50) NOT NULL,
	[DvTinh] [nvarchar](10) NULL,
	[DgBan] [money] NOT NULL,
	[SlCon] [int] NOT NULL,
	[NgayCapNhat] [datetime] NULL,
	[FileAnh] [nvarchar](500) NULL,
 CONSTRAINT [PK_MATHANG_1] PRIMARY KEY CLUSTERED 
(
	[MaMH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NHANVIEN]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NHANVIEN](
	[MaNV] [char](10) NOT NULL,
	[TenNV] [nvarchar](50) NOT NULL,
	[CMND] [char](15) NOT NULL,
	[GioiTinh] [nvarchar](10) NOT NULL,
	[NgaySinh] [date] NOT NULL,
	[DiaChi] [nvarchar](200) NOT NULL,
	[SDT] [varchar](20) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[FileAnh] [nvarchar](500) NULL,
	[MaCV] [char](10) NOT NULL,
 CONSTRAINT [PK_NhanVien] PRIMARY KEY CLUSTERED 
(
	[MaNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PNHAP]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PNHAP](
	[SoPN] [char](10) NOT NULL,
	[Ngaynhap] [datetime] NOT NULL,
	[DgNhap] [money] NOT NULL,
	[SlNhap] [int] NOT NULL,
	[MaNV] [char](10) NOT NULL,
	[MaMH] [char](10) NOT NULL,
 CONSTRAINT [PK_PNHAP] PRIMARY KEY CLUSTERED 
(
	[SoPN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TAIKHOAN]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TAIKHOAN](
	[TaiKhoan] [nchar](20) NOT NULL,
	[MatKhau] [nvarchar](50) NOT NULL,
	[MaNV] [char](10) NOT NULL,
 CONSTRAINT [PK_TAIKHOAN] PRIMARY KEY CLUSTERED 
(
	[TaiKhoan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[CHUCVU] ([MaCV], [TenCV], [LuongCB]) VALUES (N'BH        ', N'Bán Hàng', 20000000.0000)
INSERT [dbo].[CHUCVU] ([MaCV], [TenCV], [LuongCB]) VALUES (N'KT        ', N'Kế Toán', 15000000.0000)
INSERT [dbo].[CHUCVU] ([MaCV], [TenCV], [LuongCB]) VALUES (N'QL        ', N'Quản Lý', 50000000.0000)
INSERT [dbo].[CHUCVU] ([MaCV], [TenCV], [LuongCB]) VALUES (N'Test      ', N'TTT', 1111111111111.0000)
GO
INSERT [dbo].[CTHOADON] ([MaHD], [MaMH], [DgBan], [SlBan], [ThanhTien], [TrangThai]) VALUES (N'HD001     ', N'MH001     ', 130000.0000, 10, 1300000.0000, 1)
INSERT [dbo].[CTHOADON] ([MaHD], [MaMH], [DgBan], [SlBan], [ThanhTien], [TrangThai]) VALUES (N'HD001     ', N'MH002     ', 799000.0000, 2, 1598000.0000, 1)
INSERT [dbo].[CTHOADON] ([MaHD], [MaMH], [DgBan], [SlBan], [ThanhTien], [TrangThai]) VALUES (N'HD001     ', N'MH007     ', 150000.0000, 2, 300000.0000, 1)
INSERT [dbo].[CTHOADON] ([MaHD], [MaMH], [DgBan], [SlBan], [ThanhTien], [TrangThai]) VALUES (N'HD001     ', N'MH008     ', 3000000.0000, 2, 6000000.0000, 1)
INSERT [dbo].[CTHOADON] ([MaHD], [MaMH], [DgBan], [SlBan], [ThanhTien], [TrangThai]) VALUES (N'HD001     ', N'MH009     ', 250000.0000, 2, 500000.0000, 1)
INSERT [dbo].[CTHOADON] ([MaHD], [MaMH], [DgBan], [SlBan], [ThanhTien], [TrangThai]) VALUES (N'HD002     ', N'MH001     ', 130000.0000, 10, 1300000.0000, 0)
INSERT [dbo].[CTHOADON] ([MaHD], [MaMH], [DgBan], [SlBan], [ThanhTien], [TrangThai]) VALUES (N'HD002     ', N'MH009     ', 250000.0000, 1, 250000.0000, 0)
INSERT [dbo].[CTHOADON] ([MaHD], [MaMH], [DgBan], [SlBan], [ThanhTien], [TrangThai]) VALUES (N'HD003     ', N'MH007     ', 150000.0000, 1, 150000.0000, 0)
INSERT [dbo].[CTHOADON] ([MaHD], [MaMH], [DgBan], [SlBan], [ThanhTien], [TrangThai]) VALUES (N'HD004     ', N'MH009     ', 250000.0000, 1, 250000.0000, 1)
INSERT [dbo].[CTHOADON] ([MaHD], [MaMH], [DgBan], [SlBan], [ThanhTien], [TrangThai]) VALUES (N'HD005     ', N'MH009     ', 250000.0000, 2, 500000.0000, 0)
INSERT [dbo].[CTHOADON] ([MaHD], [MaMH], [DgBan], [SlBan], [ThanhTien], [TrangThai]) VALUES (N'HD006     ', N'MH002     ', 799000.0000, 2, 1598000.0000, 0)
INSERT [dbo].[CTHOADON] ([MaHD], [MaMH], [DgBan], [SlBan], [ThanhTien], [TrangThai]) VALUES (N'HD007     ', N'MH007     ', 150000.0000, 1, 150000.0000, 0)
INSERT [dbo].[CTHOADON] ([MaHD], [MaMH], [DgBan], [SlBan], [ThanhTien], [TrangThai]) VALUES (N'HD008     ', N'MH007     ', 150000.0000, 3, 450000.0000, 1)
INSERT [dbo].[CTHOADON] ([MaHD], [MaMH], [DgBan], [SlBan], [ThanhTien], [TrangThai]) VALUES (N'HD009     ', N'MH008     ', 3000000.0000, 1, 3000000.0000, 1)
INSERT [dbo].[CTHOADON] ([MaHD], [MaMH], [DgBan], [SlBan], [ThanhTien], [TrangThai]) VALUES (N'HD010     ', N'MH002     ', 799000.0000, 1, 799000.0000, 1)
INSERT [dbo].[CTHOADON] ([MaHD], [MaMH], [DgBan], [SlBan], [ThanhTien], [TrangThai]) VALUES (N'HD011     ', N'MH005     ', 219000.0000, 2, 438000.0000, 1)
INSERT [dbo].[CTHOADON] ([MaHD], [MaMH], [DgBan], [SlBan], [ThanhTien], [TrangThai]) VALUES (N'HD012     ', N'MH008     ', 3000000.0000, 1, 3000000.0000, 1)
INSERT [dbo].[CTHOADON] ([MaHD], [MaMH], [DgBan], [SlBan], [ThanhTien], [TrangThai]) VALUES (N'HD013     ', N'MH007     ', 150000.0000, 4, 600000.0000, 1)
INSERT [dbo].[CTHOADON] ([MaHD], [MaMH], [DgBan], [SlBan], [ThanhTien], [TrangThai]) VALUES (N'HD014     ', N'MH006     ', 479000.0000, 2, 958000.0000, 1)
INSERT [dbo].[CTHOADON] ([MaHD], [MaMH], [DgBan], [SlBan], [ThanhTien], [TrangThai]) VALUES (N'HD015     ', N'MH002     ', 799000.0000, 2, 1598000.0000, 1)
INSERT [dbo].[CTHOADON] ([MaHD], [MaMH], [DgBan], [SlBan], [ThanhTien], [TrangThai]) VALUES (N'HD015     ', N'MH006     ', 479000.0000, 2, 958000.0000, 0)
INSERT [dbo].[CTHOADON] ([MaHD], [MaMH], [DgBan], [SlBan], [ThanhTien], [TrangThai]) VALUES (N'HD015     ', N'MH007     ', 150000.0000, 2, 300000.0000, 1)
INSERT [dbo].[CTHOADON] ([MaHD], [MaMH], [DgBan], [SlBan], [ThanhTien], [TrangThai]) VALUES (N'HD016     ', N'MH005     ', 219000.0000, 4, 876000.0000, 1)
INSERT [dbo].[CTHOADON] ([MaHD], [MaMH], [DgBan], [SlBan], [ThanhTien], [TrangThai]) VALUES (N'HD016     ', N'MH008     ', 3000000.0000, 1, 3000000.0000, 1)
GO
INSERT [dbo].[HOADON] ([MaHD], [Ngaylap], [MaNV], [MaKH]) VALUES (N'HD001     ', CAST(N'2022-01-15T07:00:00.000' AS DateTime), N'NV001     ', N'KH001     ')
INSERT [dbo].[HOADON] ([MaHD], [Ngaylap], [MaNV], [MaKH]) VALUES (N'HD002     ', CAST(N'2022-02-12T16:20:00.000' AS DateTime), N'NV001     ', N'KH002     ')
INSERT [dbo].[HOADON] ([MaHD], [Ngaylap], [MaNV], [MaKH]) VALUES (N'HD003     ', CAST(N'2022-03-01T21:30:00.000' AS DateTime), N'NV004     ', N'KH003     ')
INSERT [dbo].[HOADON] ([MaHD], [Ngaylap], [MaNV], [MaKH]) VALUES (N'HD004     ', CAST(N'2022-04-01T23:08:00.000' AS DateTime), N'NV004     ', N'KH004     ')
INSERT [dbo].[HOADON] ([MaHD], [Ngaylap], [MaNV], [MaKH]) VALUES (N'HD005     ', CAST(N'2022-05-12T16:22:00.000' AS DateTime), N'NV003     ', N'KH005     ')
INSERT [dbo].[HOADON] ([MaHD], [Ngaylap], [MaNV], [MaKH]) VALUES (N'HD006     ', CAST(N'2022-06-12T16:23:00.000' AS DateTime), N'NV001     ', N'KH006     ')
INSERT [dbo].[HOADON] ([MaHD], [Ngaylap], [MaNV], [MaKH]) VALUES (N'HD007     ', CAST(N'2022-07-07T14:00:00.000' AS DateTime), N'NV001     ', N'KH007     ')
INSERT [dbo].[HOADON] ([MaHD], [Ngaylap], [MaNV], [MaKH]) VALUES (N'HD008     ', CAST(N'2022-08-04T22:00:00.000' AS DateTime), N'NV001     ', N'KH008     ')
INSERT [dbo].[HOADON] ([MaHD], [Ngaylap], [MaNV], [MaKH]) VALUES (N'HD009     ', CAST(N'2022-09-12T16:26:00.000' AS DateTime), N'NV004     ', N'KH009     ')
INSERT [dbo].[HOADON] ([MaHD], [Ngaylap], [MaNV], [MaKH]) VALUES (N'HD010     ', CAST(N'2022-10-31T18:00:00.000' AS DateTime), N'NV001     ', N'KH010     ')
INSERT [dbo].[HOADON] ([MaHD], [Ngaylap], [MaNV], [MaKH]) VALUES (N'HD011     ', CAST(N'2022-11-11T10:00:00.000' AS DateTime), N'NV001     ', N'KH007     ')
INSERT [dbo].[HOADON] ([MaHD], [Ngaylap], [MaNV], [MaKH]) VALUES (N'HD012     ', CAST(N'2022-12-30T11:30:00.000' AS DateTime), N'NV001     ', N'KH008     ')
INSERT [dbo].[HOADON] ([MaHD], [Ngaylap], [MaNV], [MaKH]) VALUES (N'HD013     ', CAST(N'2021-12-30T18:20:00.000' AS DateTime), N'NV003     ', N'KH010     ')
INSERT [dbo].[HOADON] ([MaHD], [Ngaylap], [MaNV], [MaKH]) VALUES (N'HD014     ', CAST(N'2021-02-28T18:20:00.000' AS DateTime), N'NV002     ', N'KH010     ')
INSERT [dbo].[HOADON] ([MaHD], [Ngaylap], [MaNV], [MaKH]) VALUES (N'HD015     ', CAST(N'2021-02-28T18:20:00.000' AS DateTime), N'NV008     ', N'KH007     ')
INSERT [dbo].[HOADON] ([MaHD], [Ngaylap], [MaNV], [MaKH]) VALUES (N'HD016     ', CAST(N'2022-05-21T18:20:00.000' AS DateTime), N'NV001     ', N'KH001     ')
GO
INSERT [dbo].[KHACHHANG] ([MaKH], [TenKH], [DiaChi], [SDT]) VALUES (N'KH001     ', N'Đặng Tuấn Kiệt', N'Hà Nội', N'0184674837')
INSERT [dbo].[KHACHHANG] ([MaKH], [TenKH], [DiaChi], [SDT]) VALUES (N'KH002     ', N'Nguyễn Ngọc Thảo', N'Hoàng Mai', N'0186473223')
INSERT [dbo].[KHACHHANG] ([MaKH], [TenKH], [DiaChi], [SDT]) VALUES (N'KH003     ', N'Nguyễn Văn Long', N'Phú Xuyên', N'0826744214')
INSERT [dbo].[KHACHHANG] ([MaKH], [TenKH], [DiaChi], [SDT]) VALUES (N'KH004     ', N'Nguyễn Văn Hạnh', N'Đà Nẵng', N'0973267543')
INSERT [dbo].[KHACHHANG] ([MaKH], [TenKH], [DiaChi], [SDT]) VALUES (N'KH005     ', N'Lê Văn Vũ', N'Hải Phòng', N'0663158182')
INSERT [dbo].[KHACHHANG] ([MaKH], [TenKH], [DiaChi], [SDT]) VALUES (N'KH006     ', N'Vũ Anh Tùng', N'Hải Dương', N'0923562522')
INSERT [dbo].[KHACHHANG] ([MaKH], [TenKH], [DiaChi], [SDT]) VALUES (N'KH007     ', N'Đặng Gia Bảo', N'Thanh Hóa', N'0376536535')
INSERT [dbo].[KHACHHANG] ([MaKH], [TenKH], [DiaChi], [SDT]) VALUES (N'KH008     ', N'Nguyễn Quang Huy', N'Bắc Ninh', N'0147462764')
INSERT [dbo].[KHACHHANG] ([MaKH], [TenKH], [DiaChi], [SDT]) VALUES (N'KH009     ', N'Đỗ Anh Dũng', N'Phú Xuyên', N'0362136762')
INSERT [dbo].[KHACHHANG] ([MaKH], [TenKH], [DiaChi], [SDT]) VALUES (N'KH010     ', N'Đỗ Tiến Cường', N'Nam Định', N'0984782245')
GO
INSERT [dbo].[MATHANG] ([MaMH], [TenMH], [DvTinh], [DgBan], [SlCon], [NgayCapNhat], [FileAnh]) VALUES (N'MH001     ', N'Ấm Siêu Tốc Thái Lan 2.5L', N'Chiếc', 130000.0000, 246, CAST(N'2022-12-04T23:34:18.197' AS DateTime), N'D:\C#\QLBanHang\QLBanHang\imgs\images\amsieutoc.png')
INSERT [dbo].[MATHANG] ([MaMH], [TenMH], [DvTinh], [DgBan], [SlCon], [NgayCapNhat], [FileAnh]) VALUES (N'MH002     ', N'Bộ Nồi 6 Món', N'Bộ', 799000.0000, 148, CAST(N'2022-12-04T23:34:23.000' AS DateTime), N'D:\C#\QLBanHang\QLBanHang\imgs\images\noi_6_mon.png')
INSERT [dbo].[MATHANG] ([MaMH], [TenMH], [DvTinh], [DgBan], [SlCon], [NgayCapNhat], [FileAnh]) VALUES (N'MH003     ', N'Lẩu Điện Vuông Đa Năng', N'Chiếc', 309000.0000, 51, CAST(N'2022-12-04T23:34:27.143' AS DateTime), N'D:\C#\QLBanHang\QLBanHang\imgs\images\bep_dien_vuong.png')
INSERT [dbo].[MATHANG] ([MaMH], [TenMH], [DvTinh], [DgBan], [SlCon], [NgayCapNhat], [FileAnh]) VALUES (N'MH004     ', N'Ấm Dữ Nhiệt Sports 750ml', N'Chiếc', 69000.0000, 51, CAST(N'2022-12-04T23:34:31.793' AS DateTime), N'D:\C#\QLBanHang\QLBanHang\imgs\images\amgiunhiet.png')
INSERT [dbo].[MATHANG] ([MaMH], [TenMH], [DvTinh], [DgBan], [SlCon], [NgayCapNhat], [FileAnh]) VALUES (N'MH005     ', N'Bộ Bát Ăn Gia Đình Bát Tràng 11 Món', N'Bộ', 219000.0000, 51, CAST(N'2022-12-04T23:34:36.750' AS DateTime), N'D:\C#\QLBanHang\QLBanHang\imgs\images\bo_bat.png')
INSERT [dbo].[MATHANG] ([MaMH], [TenMH], [DvTinh], [DgBan], [SlCon], [NgayCapNhat], [FileAnh]) VALUES (N'MH006     ', N'Bếp Hồng Ngoại Fujika', N'Chiếc', 479000.0000, 51, CAST(N'2022-12-04T23:34:50.730' AS DateTime), N'D:\C#\QLBanHang\QLBanHang\imgs\images\bep_hong_ngoai.png')
INSERT [dbo].[MATHANG] ([MaMH], [TenMH], [DvTinh], [DgBan], [SlCon], [NgayCapNhat], [FileAnh]) VALUES (N'MH007     ', N'Máy Hút Bụi', N'Chiếc', 150000.0000, 56, CAST(N'2022-12-04T23:34:56.383' AS DateTime), N'D:\C#\QLBanHang\QLBanHang\imgs\images\may_hut_bui.jpg')
INSERT [dbo].[MATHANG] ([MaMH], [TenMH], [DvTinh], [DgBan], [SlCon], [NgayCapNhat], [FileAnh]) VALUES (N'MH008     ', N'Nồi Chiên Không Dầu', N'Chiếc', 3000000.0000, 46, CAST(N'2022-12-04T23:35:01.113' AS DateTime), N'D:\C#\QLBanHang\QLBanHang\imgs\images\noi_chien_khong_dau.jpg')
INSERT [dbo].[MATHANG] ([MaMH], [TenMH], [DvTinh], [DgBan], [SlCon], [NgayCapNhat], [FileAnh]) VALUES (N'MH009     ', N'Nồi Cơm Điện', N'Chiếc', 250000.0000, 46, CAST(N'2022-12-04T23:35:05.553' AS DateTime), N'D:\C#\QLBanHang\QLBanHang\imgs\images\noi_com_dien.png')
GO
INSERT [dbo].[NHANVIEN] ([MaNV], [TenNV], [CMND], [GioiTinh], [NgaySinh], [DiaChi], [SDT], [Email], [FileAnh], [MaCV]) VALUES (N'NV001     ', N'Nguyễn Xuân Ngát', N'010213859739   ', N'Nam', CAST(N'2002-02-26' AS Date), N'Hà Nội', N'0987654321', N'ngat@gmail.com', N'D:\C#\QLBanHang\QLBanHang\imgs\imgNhanVien\ngat.jpg', N'QL        ')
INSERT [dbo].[NHANVIEN] ([MaNV], [TenNV], [CMND], [GioiTinh], [NgaySinh], [DiaChi], [SDT], [Email], [FileAnh], [MaCV]) VALUES (N'NV002     ', N'Vũ Văn Thuyên', N'019367878643   ', N'Nam', CAST(N'2002-02-14' AS Date), N'Hà Nội', N'0123456789', N'thuyen@gmail.com', N'D:\C#\QLBanHang\QLBanHang\imgs\imgNhanVien\thuyen.jpg', N'BH        ')
INSERT [dbo].[NHANVIEN] ([MaNV], [TenNV], [CMND], [GioiTinh], [NgaySinh], [DiaChi], [SDT], [Email], [FileAnh], [MaCV]) VALUES (N'NV003     ', N'Nguyễn Quang Thiện', N'018946758432   ', N'Nam', CAST(N'2002-04-18' AS Date), N'Hà Nội', N'0192837465', N'thien@gmail.com', N'D:\C#\QLBanHang\QLBanHang\imgs\imgNhanVien\thien.jpg', N'BH        ')
INSERT [dbo].[NHANVIEN] ([MaNV], [TenNV], [CMND], [GioiTinh], [NgaySinh], [DiaChi], [SDT], [Email], [FileAnh], [MaCV]) VALUES (N'NV004     ', N'Lung Thị Linh', N'038748745213   ', N'Nữ', CAST(N'2000-03-03' AS Date), N'Hải Phòng', N'0476578257', N'linh@gmail.com', N'D:\C#\QLBanHang\QLBanHang\imgs\imgNhanVien\linh.jpg', N'BH        ')
INSERT [dbo].[NHANVIEN] ([MaNV], [TenNV], [CMND], [GioiTinh], [NgaySinh], [DiaChi], [SDT], [Email], [FileAnh], [MaCV]) VALUES (N'NV005     ', N'Đặng Mai Ngoc', N'023264264174   ', N'Nữ', CAST(N'1999-04-04' AS Date), N'TPHCM', N'0625362536', N'ngoc@gmail.com', N'D:\C#\QLBanHang\QLBanHang\imgs\imgNhanVien\anh4.jpg', N'KT        ')
INSERT [dbo].[NHANVIEN] ([MaNV], [TenNV], [CMND], [GioiTinh], [NgaySinh], [DiaChi], [SDT], [Email], [FileAnh], [MaCV]) VALUES (N'NV006     ', N'Lê Thị Lan Chi', N'017543647234   ', N'Nữ', CAST(N'1998-06-20' AS Date), N'Đà Nẵng', N'0998877665', N'chi@gmail.com', N'D:\C#\QLBanHang\QLBanHang\imgs\imgNhanVien\ngoc.jpg', N'BH        ')
INSERT [dbo].[NHANVIEN] ([MaNV], [TenNV], [CMND], [GioiTinh], [NgaySinh], [DiaChi], [SDT], [Email], [FileAnh], [MaCV]) VALUES (N'NV007     ', N'Dào Thị Dung', N'038575241424   ', N'Nữ', CAST(N'2001-06-14' AS Date), N'Bắc Ninh', N'08376541645', N'dung@gmail.com', N'D:\C#\QLBanHang\QLBanHang\imgs\imgNhanVien\anh3.jpg', N'BH        ')
INSERT [dbo].[NHANVIEN] ([MaNV], [TenNV], [CMND], [GioiTinh], [NgaySinh], [DiaChi], [SDT], [Email], [FileAnh], [MaCV]) VALUES (N'NV008     ', N'Nguyễn Thị Thảo', N'017546472213   ', N'Nữ', CAST(N'2001-06-14' AS Date), N'Huế', N'0998877665', N'thao@gmail.com', N'D:\C#\QLBanHang\QLBanHang\imgs\imgNhanVien\anh2.jpg', N'BH        ')
INSERT [dbo].[NHANVIEN] ([MaNV], [TenNV], [CMND], [GioiTinh], [NgaySinh], [DiaChi], [SDT], [Email], [FileAnh], [MaCV]) VALUES (N'NV009     ', N'Nguyễn Thị Hồng', N'017215447221   ', N'Nữ', CAST(N'2001-06-14' AS Date), N'Hải Phòng', N'093642142', N'hong@gmail.com', N'D:\C#\QLBanHang\QLBanHang\imgs\imgNhanVien\anh5.jpg', N'BH        ')
GO
INSERT [dbo].[PNHAP] ([SoPN], [Ngaynhap], [DgNhap], [SlNhap], [MaNV], [MaMH]) VALUES (N'001       ', CAST(N'2022-12-15T22:29:00.000' AS DateTime), 90000.0000, 200, N'NV001     ', N'MH001     ')
INSERT [dbo].[PNHAP] ([SoPN], [Ngaynhap], [DgNhap], [SlNhap], [MaNV], [MaMH]) VALUES (N'002       ', CAST(N'2022-12-15T00:42:00.000' AS DateTime), 600000.0000, 100, N'NV001     ', N'MH002     ')
GO
INSERT [dbo].[TAIKHOAN] ([TaiKhoan], [MatKhau], [MaNV]) VALUES (N'linh                ', N'1', N'NV004     ')
INSERT [dbo].[TAIKHOAN] ([TaiKhoan], [MatKhau], [MaNV]) VALUES (N'ngat                ', N'1', N'NV001     ')
INSERT [dbo].[TAIKHOAN] ([TaiKhoan], [MatKhau], [MaNV]) VALUES (N'thao                ', N'1', N'NV008     ')
INSERT [dbo].[TAIKHOAN] ([TaiKhoan], [MatKhau], [MaNV]) VALUES (N'thien               ', N'1', N'NV003     ')
INSERT [dbo].[TAIKHOAN] ([TaiKhoan], [MatKhau], [MaNV]) VALUES (N'thuyen              ', N'1', N'NV002     ')
GO
ALTER TABLE [dbo].[HOADON] ADD  CONSTRAINT [DF_HOADON_Ngaylap]  DEFAULT (getdate()) FOR [Ngaylap]
GO
ALTER TABLE [dbo].[NHANVIEN] ADD  CONSTRAINT [DF_NHANVIEN_Email]  DEFAULT (N'Chưa có') FOR [Email]
GO
ALTER TABLE [dbo].[CTHOADON]  WITH CHECK ADD  CONSTRAINT [FK_CTHOADON_HOADON] FOREIGN KEY([MaHD])
REFERENCES [dbo].[HOADON] ([MaHD])
GO
ALTER TABLE [dbo].[CTHOADON] CHECK CONSTRAINT [FK_CTHOADON_HOADON]
GO
ALTER TABLE [dbo].[CTHOADON]  WITH CHECK ADD  CONSTRAINT [FK_CTHOADON_MATHANG] FOREIGN KEY([MaMH])
REFERENCES [dbo].[MATHANG] ([MaMH])
GO
ALTER TABLE [dbo].[CTHOADON] CHECK CONSTRAINT [FK_CTHOADON_MATHANG]
GO
ALTER TABLE [dbo].[HOADON]  WITH CHECK ADD  CONSTRAINT [FK_HOADON_KHACHHANG] FOREIGN KEY([MaKH])
REFERENCES [dbo].[KHACHHANG] ([MaKH])
GO
ALTER TABLE [dbo].[HOADON] CHECK CONSTRAINT [FK_HOADON_KHACHHANG]
GO
ALTER TABLE [dbo].[HOADON]  WITH CHECK ADD  CONSTRAINT [FK_HOADON_NhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NHANVIEN] ([MaNV])
GO
ALTER TABLE [dbo].[HOADON] CHECK CONSTRAINT [FK_HOADON_NhanVien]
GO
ALTER TABLE [dbo].[NHANVIEN]  WITH CHECK ADD  CONSTRAINT [FK_NHANVIEN_CHUCVU] FOREIGN KEY([MaCV])
REFERENCES [dbo].[CHUCVU] ([MaCV])
GO
ALTER TABLE [dbo].[NHANVIEN] CHECK CONSTRAINT [FK_NHANVIEN_CHUCVU]
GO
ALTER TABLE [dbo].[PNHAP]  WITH CHECK ADD  CONSTRAINT [FK_PNHAP_MATHANG] FOREIGN KEY([MaMH])
REFERENCES [dbo].[MATHANG] ([MaMH])
GO
ALTER TABLE [dbo].[PNHAP] CHECK CONSTRAINT [FK_PNHAP_MATHANG]
GO
ALTER TABLE [dbo].[PNHAP]  WITH CHECK ADD  CONSTRAINT [FK_PNHAP_NHANVIEN] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NHANVIEN] ([MaNV])
GO
ALTER TABLE [dbo].[PNHAP] CHECK CONSTRAINT [FK_PNHAP_NHANVIEN]
GO
ALTER TABLE [dbo].[TAIKHOAN]  WITH CHECK ADD  CONSTRAINT [FK_TAIKHOAN_NHANVIEN] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NHANVIEN] ([MaNV])
GO
ALTER TABLE [dbo].[TAIKHOAN] CHECK CONSTRAINT [FK_TAIKHOAN_NHANVIEN]
GO
/****** Object:  StoredProcedure [dbo].[DoanhThu]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[DoanhThu]
@Nam char(4)
as
	begin
		select FORMAT(SUM(ThanhTien),'#,##0') as'Thang1'from HOADON,CTHOADON where Ngaylap BETWEEN @Nam+'-1-1' and  @Nam+'-2-1' and HOADON.MaHD=CTHOADON.MaHD
		select FORMAT(SUM(ThanhTien),'#,##0') as'Thang2'from HOADON,CTHOADON where Ngaylap BETWEEN  @Nam+'-2-1' and  @Nam+'-3-1' and HOADON.MaHD=CTHOADON.MaHD
		select FORMAT(SUM(ThanhTien),'#,##0') as'Thang3'from HOADON,CTHOADON where Ngaylap BETWEEN  @Nam+'-3-1' and  @Nam+'-4-1' and HOADON.MaHD=CTHOADON.MaHD
		select FORMAT(SUM(ThanhTien),'#,##0') as'Thang4'from HOADON,CTHOADON where Ngaylap BETWEEN  @Nam+'-4-1' and  @Nam+'-5-1' and HOADON.MaHD=CTHOADON.MaHD
		select FORMAT(SUM(ThanhTien),'#,##0') as'Thang5'from HOADON,CTHOADON where Ngaylap BETWEEN  @Nam+'-5-1' and  @Nam+'-6-1' and HOADON.MaHD=CTHOADON.MaHD
		select FORMAT(SUM(ThanhTien),'#,##0') as'Thang6'from HOADON,CTHOADON where Ngaylap BETWEEN  @Nam+'-6-1' and  @Nam+'-7-1' and HOADON.MaHD=CTHOADON.MaHD
		select FORMAT(SUM(ThanhTien),'#,##0') as'Thang7'from HOADON,CTHOADON where Ngaylap BETWEEN  @Nam+'-7-1' and  @Nam+'-8-1' and HOADON.MaHD=CTHOADON.MaHD
		select FORMAT(SUM(ThanhTien),'#,##0') as'Thang8'from HOADON,CTHOADON where Ngaylap BETWEEN  @Nam+'-8-1' and  @Nam+'-9-1' and HOADON.MaHD=CTHOADON.MaHD
		select FORMAT(SUM(ThanhTien),'#,##0') as'Thang9'from HOADON,CTHOADON where Ngaylap BETWEEN  @Nam+'-9-1' and  @Nam+'-10-1' and HOADON.MaHD=CTHOADON.MaHD
		select FORMAT(SUM(ThanhTien),'#,##0') as'Thang10'from HOADON,CTHOADON where Ngaylap BETWEEN  @Nam+'-10-1' and  @Nam+'-11-1' and HOADON.MaHD=CTHOADON.MaHD
		select FORMAT(SUM(ThanhTien),'#,##0') as'Thang11'from HOADON,CTHOADON where Ngaylap BETWEEN  @Nam+'-11-1' and  @Nam+'-12-1' and HOADON.MaHD=CTHOADON.MaHD
		select FORMAT(SUM(ThanhTien),'#,##0') as'Thang12'from HOADON,CTHOADON where Ngaylap BETWEEN  @Nam+'-12-1' and  @Nam+'-12-31 23:59' and HOADON.MaHD=CTHOADON.MaHD
	end
GO
/****** Object:  StoredProcedure [dbo].[DTNhanVien]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[DTNhanVien]
as
	begin		
		select MaNV,SUM(ThanhTien) as 'DTBanHang' from HOADON,CTHOADON where HOADON.MaHD=CTHOADON.MaHD group by MaNV ORDER BY DTBanHang	DESC 
	end
GO
/****** Object:  StoredProcedure [dbo].[HienThiAvatar]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[HienThiAvatar]
@MaNV char(10)
as
	begin 
		select FileAnh from NHANVIEN where @MaNV = MaNV
	end
GO
/****** Object:  StoredProcedure [dbo].[HienThiChucVu]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc  [dbo].[HienThiChucVu]
as
	begin
		select MaCV,TenCV,LuongCB=FORMAT(LuongCB,'#,##0') from CHUCVU
	end
GO
/****** Object:  StoredProcedure [dbo].[HienThiCTHoaDon]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[HienThiCTHoaDon]
as
	begin
		select MaHD,MaMH,DgBan=FORMAT(DgBan,'#,##0'),SlBan,ThanhTien=FORMAT(ThanhTien,'#,##0'),TrangThai from CTHOADON
	end
GO
/****** Object:  StoredProcedure [dbo].[HienThiHoaDon]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[HienThiHoaDon]
as
	begin
		select * from HOADON
	end
GO
/****** Object:  StoredProcedure [dbo].[HienThiItemMH]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[HienThiItemMH]
as
	begin						
		select MaMH, SUM(SlBan) from CTHOADON group by MaMH order by SUM(SlBan) DESC
	end
GO
/****** Object:  StoredProcedure [dbo].[HienThiKH]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[HienThiKH]
as
	begin
		select * from KHACHHANG
	end
GO
/****** Object:  StoredProcedure [dbo].[HienThiListMH]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[HienThiListMH]
@MaMH char(10)
as
	begin		
		select SUM(SlBan) as 'SlBan' from CTHOADON where MaMH=@MaMH	
		select TenMH,DgBan=FORMAT(DgBan,'C0','vi-VN'),SlCon,FileAnh from MATHANG where MaMH=@MaMH	
	end
GO
/****** Object:  StoredProcedure [dbo].[HienThiMatHang]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[HienThiMatHang]
as
	begin
		select MaMH,TenMH,DvTinh,DgBan=Format(DgBan,'#,###'),SlCon,NgayCapNhat,FileAnh from MATHANG
	end
GO
/****** Object:  StoredProcedure [dbo].[HienThiNhanVien]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc  [dbo].[HienThiNhanVien]
as
	begin
		select * from NHANVIEN
	end
GO
/****** Object:  StoredProcedure [dbo].[HienThiPN]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[HienThiPN]
as
	begin
		select SoPN,Ngaynhap,DgNhap=Format(DgNhap,'#,###'),SlNhap,MaNV,MaMH from PNHAP
	end
GO
/****** Object:  StoredProcedure [dbo].[HienThiTK]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[HienThiTK]
as
	begin
		select * from TAIKHOAN
	end
GO
/****** Object:  StoredProcedure [dbo].[HienTopNhanVienDT]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[HienTopNhanVienDT]
@MaNV char(10)
as
	begin
		select TenNV,FileAnh from NHANVIEN where @MaNV=MaNV
	end
GO
/****** Object:  StoredProcedure [dbo].[SuaChucVu]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SuaChucVu]
@MaCV	char(10),
@TenCV	nvarchar(30),
@LuongCB money
as
	begin
		update CHUCVU set TenCV=@TenCV,LuongCB=@LuongCB where MaCV =@MaCV
	end
GO
/****** Object:  StoredProcedure [dbo].[SuaCTHoaDon]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SuaCTHoaDon]
@MaHD	char(10),	
@MaMH	char(10),	
@DgBan	money,	
@SlBan	int,	
@TrangThai	bit
as
	begin
		declare @TG int
		select @TG=SlBan from CTHOADON where MaHD=@MaHD and MaMH=@MaMH
		update CTHOADON set DgBan=@DgBan,SlBan=@SlBan,ThanhTien=@DgBan*@SlBan,TrangThai=@TrangThai
		where MaHD=@MaHD and MaMH=@MaMH		
		update MATHANG set SlCon+=@TG-@SlBan where MaMH=@MaMH
	end
GO
/****** Object:  StoredProcedure [dbo].[SuaHD]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[SuaHD]
@MaHD	char(10),	
@Ngaylap	datetime,	
@MaNV	char(10),	
@MaKH	char(10)	
as
	begin
		update HOADON set Ngaylap= @Ngaylap,MaNV=@MaNV,MaKH=@MaKH
		where MaHD=@MaHD
	end
GO
/****** Object:  StoredProcedure [dbo].[SuaKH]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[SuaKH]
@MaKH	char(10),
@TenKH	nvarchar(50),	
@DiaChi	nvarchar(100),	
@SDT	varchar(20)	
as
	begin
		update KHACHHANG set TenKH=@TenKH,DiaChi=@DiaChi,SDT=@SDT where @MaKH=MaKH
	end
GO
/****** Object:  StoredProcedure [dbo].[SuaMatHang]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SuaMatHang]
@MaMH	char(10),
@TenMH	nvarchar(50),	
@DvTinh	nvarchar(10),	
@DgBan	money,	
@SlCon	int,	
@FileAnh	nvarchar(500)
as
	begin
		update MATHANG set TenMH=@TenMH,DvTinh=@DvTinh,DgBan=@DgBan,SlCon=@SlCon,NgayCapNhat=GETDATE(),FileAnh=@FileAnh
		where @MaMH = MaMH
	end
GO
/****** Object:  StoredProcedure [dbo].[SuaNhanVien]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SuaNhanVien]
@MaNV char(10),
@TenNV nvarchar(50),
@CMND char(15),	
@GioiTinh nvarchar(10),	
@NgaySinh date,	
@DiaChi	nvarchar(200),	
@SDT varchar(20),
@Email nvarchar(100),	
@FileAnh nvarchar(500),
@MaCV char(10)
as
	begin
		update NHANVIEN 
		set TenNV = @TenNV,CMND=@CMND,GioiTinh=@GioiTinh,NgaySinh=@NgaySinh,DiaChi=@DiaChi,SDT=@SDT,Email=@Email,FileAnh=@FileAnh,MaCV=@MaCV
		where MaNV = @MaNV
	end
GO
/****** Object:  StoredProcedure [dbo].[SuaPN]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SuaPN]
@SoPN	char(10),	
@Ngaynhap	datetime,	
@DgNhap	money,	
@SlNhap	int,
@MaNV	char(10),	
@MaMH	char(10)
as
	begin
		declare @TG int;
		select @TG=SlNhap from PNHAP;
		update PNHAP set NgayNhap=@Ngaynhap,DgNhap=@DgNhap,SlNhap=@SlNhap,MaNV=@MaNV,MaMH=@MaMH
		where SoPN=@SoPN;	
		update MATHANG set SlCon+=@SlNhap-@TG
		where MaMH=@MaMH
	end
GO
/****** Object:  StoredProcedure [dbo].[SuaTaiKhoan]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[SuaTaiKhoan]
@TaiKhoan	nchar(20),	
@MatKhau	nvarchar(50),	
@MaNV	char(10)	
as
	begin
		update TAIKHOAN set MatKhau=@MatKhau,MaNV=@MaNV where TaiKhoan =@TaiKhoan
	end
GO
/****** Object:  StoredProcedure [dbo].[test1]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[test1]
@Nam char(4)
as
	begin
		select SUM(ThanhTien) as'Thang1'from HOADON,CTHOADON where Ngaylap BETWEEN @Nam+'-1-1' and @Nam+'-2-1' and HOADON.MaHD=CTHOADON.MaHD
	end
GO
/****** Object:  StoredProcedure [dbo].[ThemChucVu]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ThemChucVu]
@MaCV	char(10),
@TenCV	nvarchar(30),
@LuongCB money
as
	begin
		insert into CHUCVU values(@MaCV,@TenCV,@LuongCB)
	end
GO
/****** Object:  StoredProcedure [dbo].[ThemCTHoaDon]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ThemCTHoaDon]
@MaHD	char(10),	
@MaMH	char(10),	
@DgBan	money,	
@SlBan	int,		
@TrangThai	bit
as
	begin
		insert into CTHOADON values(@MaHD,@MaMH,@DgBan,@SlBan,@DgBan*@SlBan,@TrangThai)
		update MATHANG set SlCon-=@SlBan
	end
GO
/****** Object:  StoredProcedure [dbo].[ThemHD]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[ThemHD]
@MaHD	char(10),	
@Ngaylap	datetime,	
@MaNV	char(10),	
@MaKH	char(10)	
as
	begin
		insert into HOADON values(@MaHD,@Ngaylap,@MaNV,@MaKH)
	end
GO
/****** Object:  StoredProcedure [dbo].[ThemKH]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[ThemKH]
@MaKH	char(10),
@TenKH	nvarchar(50),	
@DiaChi	nvarchar(100),	
@SDT	varchar(20)	
as
	begin
		insert into KHACHHANG values(@MaKH,@TenKH,@DiaChi,@SDT)
	end
GO
/****** Object:  StoredProcedure [dbo].[ThemMatHang]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ThemMatHang]
@MaMH	char(10),	
@TenMH	nvarchar(50),	
@DvTinh	nvarchar(10),	
@DgBan	money,	
@SlCon	int,	
@FileAnh	nvarchar(500)

as
	begin
		insert into MATHANG values(@MaMH,@TenMH,@DvTinh,@DgBan,@SlCon,GETDATE(),@FileAnh)
	end
GO
/****** Object:  StoredProcedure [dbo].[ThemNhanVien]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ThemNhanVien]
@MaNV char(10),
@TenNV nvarchar(50),	
@CMND char(15),	
@GioiTinh nvarchar(10),	
@NgaySinh date,	
@DiaChi	nvarchar(200),	
@SDT varchar(20),
@Email nvarchar(100),	
@FileAnh nvarchar(500),
@MaCV char(10)
as
	begin
		insert into NHANVIEN values(@MaNV,@TenNV,@CMND,@GioiTinh,@NgaySinh,@DiaChi,@SDT,@Email,@FileAnh,@MaCV)
	end
		
GO
/****** Object:  StoredProcedure [dbo].[ThemPN]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ThemPN]
@SoPN	char(10),	
@Ngaynhap	datetime,	
@DgNhap	money,	
@SlNhap	int,
@MaNV	char(10),	
@MaMH	char(10)
as
	begin
		insert into PNHAP values(@SoPN,@Ngaynhap,@DgNhap,@SlNhap,@MaNV,@MaMH)
		update MATHANG set SlCon+=@SlNhap
		where MaMH=@MaMH
	end
GO
/****** Object:  StoredProcedure [dbo].[ThemTaiKhoan]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[ThemTaiKhoan]
@TaiKhoan	nchar(20),	
@MatKhau	nvarchar(50),	
@MaNV	char(10)	
as
	begin
		insert into TAIKHOAN values(@TaiKhoan,@MatKhau,@MaNV)
	end
GO
/****** Object:  StoredProcedure [dbo].[XoaChucVu]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[XoaChucVu]
@MaCV char(10)
as
	begin
		delete from CHUCVU where MaCV =@MaCV
	end
GO
/****** Object:  StoredProcedure [dbo].[XoaCTHD]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[XoaCTHD]
@MaHD char(10),
@MaMH char(10),
@SlBan int
as
	begin
		delete from CTHOADON where @MaMH=MaMH and @MaHD =MaHD
		update MATHANG set SlCon+=@SlBan where @MaMH=MaMH
	end
GO
/****** Object:  StoredProcedure [dbo].[XoaHD]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[XoaHD]
@MaHD char(10)
as
	begin
		delete HOADON where MaHD=@MaHD
		delete CTHOADON where MaHD=@MaHD
	end
GO
/****** Object:  StoredProcedure [dbo].[XoaKH]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[XoaKH]
@MaKH	char(10)
as
	begin
		delete KHACHHANG where MaKH =@MaKH
	end
GO
/****** Object:  StoredProcedure [dbo].[XoaMatHang]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[XoaMatHang]
@MaMH char(10)
as
	begin
		delete MATHANG where MaMH=@MaMH
	end
GO
/****** Object:  StoredProcedure [dbo].[XoaNhanVien]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[XoaNhanVien]
@MaNV char(10)
as
	begin
		Delete from NHANVIEN where @MaNV = MaNV
	end
GO
/****** Object:  StoredProcedure [dbo].[XoaPN]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[XoaPN]
@SoPN char(10)
as
	begin		
		Delete PNHAP where SoPN=@SoPN
	end
GO
/****** Object:  StoredProcedure [dbo].[XoaTaiKhoan]    Script Date: 12/16/2022 7:55:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[XoaTaiKhoan]
@TaiKhoan	nchar(20)
as
	begin
		delete from TAIKHOAN where TaiKhoan=@TaiKhoan		 
	end
GO
USE [master]
GO
ALTER DATABASE [QLBanHang] SET  READ_WRITE 
GO
