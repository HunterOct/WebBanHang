Create Database Model_63131631
go
use Model_63131631
go

Create Table Categories(
	cateID int identity(1,1) primary key,
	cateName Nvarchar(100),
	catePhoto Nvarchar(MAX)
)
go
Create Table ProductTypes(
	typeID int identity(1,1) primary key,
	cateID int FOREIGN KEY REFERENCES Categories(cateID),
	typeName Nvarchar(100)
)
go
Create Table Producers(
	pdcID int identity(1,1) primary key,
	pdcName Nvarchar(2000),
	pdcPhone varchar(20),
	pdcEmail varchar(50),
	pdcAddress Nvarchar(MAX),
	pdcPhoto Nvarchar(MAX),
	pdcInfo Nvarchar(MAX)
)
go
Create Table Products(
	proID varchar(50) primary key,
	pdcID int FOREIGN KEY REFERENCES Producers(pdcID),
	typeID int FOREIGN KEY REFERENCES ProductTypes(typeID),
	proName Nvarchar(2000),
	proGuarantee nvarchar(2000),
	proPrice varchar(100),
	proDiscount int,
	proPhoto Nvarchar(MAX),
	proUpdateDate varchar(50),
	proDescription Nvarchar(MAX)
)
go
Create Table Administrator(
	adAcc varchar(500) primary key,
	adPass varchar(500)
)
go
Create Table Customers(
	cusID varchar(20) primary key,
	cusPhone varchar(20),
	cusFullName Nvarchar(200),
	cusEmail varchar(100),
	cusAddress Nvarchar(500),
	cusIMG nvarchar (500),
	cusPass varchar(50),
	GioiTinh bit DEFAULT(1),
)
go
Create Table Orders(
	orderID varchar(20) primary key,
	cusID varchar(20) FOREIGN KEY REFERENCES Customers(cusID),
	cusPhone varchar(20),
	cusAddress Nvarchar(500),
	orderMessage Nvarchar(MAX),
	orderDateTime varchar(50),
	orderStatus Nvarchar(50)
)
go
Create Table OrderDetails(
	orderID varchar(20) FOREIGN KEY REFERENCES Orders(orderID),
	proID varchar(50) FOREIGN KEY REFERENCES Products(proID),
	ordtsQuantity int,
	ordtsThanhTien varchar(50),
	Constraint PK_OrderDetails Primary key (orderID, proID)
)
go
Create Table Rates(
	proID varchar(50) FOREIGN KEY REFERENCES Products(proID),
	rateStar int
	Constraint PK_Rates Primary key (proID)
)
go

--Insert table Administrator
Insert into Administrator values('admin','admin')


--Insert table Categories
Insert into Categories values(N'Điện Thoại', N'/Images/phone.png')
Insert into Categories values(N'PC - LapTop', N'/Images/laptopgaming.png')
Insert into Categories values(N'Linh Kiện', N'/Images/vga.png')
--Insert table ProductTypes
Insert into ProductTypes values(1, N'IPhone')
Insert into ProductTypes values(1, N'Android')
Insert into ProductTypes values(2, N'PC Gaming')
Insert into ProductTypes values(2, N'Laptop Gaming')
Insert into ProductTypes values(3, N'VGA - Card Đồ Họa')
Insert into ProductTypes values(3, N'CPU')
Insert into ProductTypes values(3, N'RAM')
Insert into ProductTypes values(3, N'Màn Hình')
--Insert table Producers
Insert into Producers values(N'Asus','01212692802','Asus@gmail.com',N'TpHCM',N'/Images/asus.png',N'Trống')
Insert into Producers values(N'Acer','01212692802','Acer@gmail.com',N'TpHCM',N'/Images/acer.png',N'TTrống')
Insert into Producers values(N'Samsung','01212692802','Samsung@gmail.com',N'TpHCM',N'/Images/samsung.png',N'Trống')
Insert into Producers values(N'GiGabyte','01212692802','GiGabyte@gmail.com',N'TpHCM',N'/Images/gigabyte.png',N'Trống')
Insert into Producers values(N'Apple','01212692802','Apple@gmail.com',N'TpHCM',N'/Images/apple.png',N'Trống')
Insert into Producers values(N'LG','01212692802','LG@gmail.com',N'TpHCM',N'/Images/lg.png',N'Trống')
Insert into Producers values(N'Intel','01212692802','Intel@gmail.com',N'TpHCM',N'/Images/intel.png',N'Trống')
Insert into Producers values(N'Nvidia','01212692802','Nvidia@gmail.com',N'TpHCM',N'/Images/nvidia.png',N'Trống')
Insert into Producers values(N'AMD','01212692802','AMD@gmail.com',N'TpHCM',N'/Images/amd.png',N'Trống')

--Insert table Products
--VGA
Insert into Products values('RTX001', 8, 5, N'Card màn hình ASUS ROG Strix GeForce RTX 4060 Ti OC Edition 16GB (ROG-STRIX-RTX4060TI-O16G)', N'36 Tháng', '15990000', 5, N'/Images/4060ti.png', '10/01/2023 6:43:36 AM', N'Không có mô tả')
Insert into Products values('RTX002', 8, 5, N'Card màn hình MSI GeForce RTX 4070 GAMING X SLIM WHITE 12G
', N'36 Tháng', '19390000', 0, N'/Images/4070.png', '10/02/2023 6:43:36 AM', N'Không có mô tả')
Insert into Products values('RTX003', 8, 5, N'Card màn hình ASUS Dual GeForce RTX 4060 OC Edition 8GB (DUAL-RTX4060-O8G)', N'36 Tháng', '8690000', 0, N'/Images/4060.png', '10/03/2023 6:43:36 AM', N'Không có mô tả')
Insert into Products values('RTX004', 8, 5, N'Card màn hình GIGABYTE GeForce GT 1030 OC 2G (GV-N1030OC-2GI)','12 Tháng', '3090000', 10, N'/Images/1030.png', '10/05/2023 6:43:36 AM', N'Không có mô tả')
Insert into Products values('AMD001', 9, 5, N'CARD MÀN HÌNH ASUS ROG STRIX RX 6600 XT-O8G-GAMING','12 Tháng', '10999000', 16, N'/Images/6600xt.png', '10/05/2023 6:43:36 AM', N'Không có mô tả')
Insert into Products values('AMD002', 9, 5, N'CARD MÀN HÌNH ASUS TUF GAMING RX 7900 XTX OC OC EDITION 24GB GDDR6','12 Tháng', '35399000', 10, N'/Images/rx7900.png', '10/05/2023 6:43:36 AM', N'Không có mô tả')
Insert into Products values('AMD003', 9, 5, N'CARD MÀN HÌNH GIGABYTE RX 7900 XT GAMING OC 20G','12 Tháng', '3090000', 20, N'/Images/7900xt20g.png', '10/05/2023 6:43:36 AM', N'Không có mô tả')
--CPU
Insert into Products values('CPU001', 7, 6, N'Bộ vi xử lý Intel Core i7 14700KF / Turbo up to 5.6GHz / 20 Nhân 28 Luồng / 33MB / LGA 1700', N'36 tháng', '11190000', 5, N'/Images/i714700k.png', '10/04/2023 6:43:36 AM', N'Không có mô tả')
Insert into Products values('CPU002', 7, 6, N'Bộ vi xử lý Intel Core i5 12400F / 2.5GHz Turbo 4.4GHz / 6 Nhân 12 Luồng / 18MB / LGA 1700',N'36 tháng','5490000', 30, N'/Images/i512400f.png', '10/04/2023 6:43:36 AM', N'Không có mô tả')
Insert into Products values('CPU003', 7, 6, N'Bộ vi xử lý Intel Core i7 12700 / 2.1GHz Turbo 4.9GHz / 12 Nhân 20 Luồng / 25MB / LGA 1700', N'36 tháng', '10490000', 2, N'/Images/i712700.png', '10/04/2023 6:43:36 AM', N'Không có mô tả')
Insert into Products values('CPU004', 9, 6, N'Bộ vi xử lý AMD Ryzen Threadripper Pro 7995WX/ 2.5GHz Boost 5.1GHz / 96 nhân 192 luồng / 480MB / sTR5', N'36 tháng', '288000000', 10, N'/Images/AMD7995WX.png', '10/04/2023 6:43:36 AM', N'Không có mô tả')
Insert into Products values('CPU005', 9, 6, N'Bộ vi xử lý AMD Ryzen Threadripper Pro 7985WX/ 3.2GHz Boost 5.1GHz / 64 nhân 128 luồng / 320MB / sTR5', N'36 tháng', '209000000', 5, N'/Images/7985WX.png', '10/04/2023 6:43:36 AM', N'Không có mô tả')
--RAM
Insert into Products values('RAM001', 3, 7, N'RAM Laptop Samsung 8GB DDR4 3200MHz', N'36 tháng', '650000', 50, N'/Images/8gbdd4.png', '10/04/2023 6:43:36 AM', N'Không có mô tả')
Insert into Products values('RAM002', 3, 7, N'Ram Laptop Samsung 16GB 5600MHZ DDR5', N'36 tháng', '1390000', 5, N'/Images/ddr5.png', '10/04/2023 6:43:36 AM', N'Không có mô tả')
--MÀn Hình
Insert into Products values('MAN001', 6, 8, N'Màn hình LG 45GR95QE-B UltraGear 45" OLED 2K 240Hz G-Sync', N'36 tháng', '33290000', 8, N'/Images/45GR95QE.png', '10/04/2023 6:43:36 AM', N'Không có mô tả')
Insert into Products values('MAN002', 6, 8, N'Màn hình cong LG 38WN95C-W 38” IPS 2K 144Hz', N'36 tháng', '25490000', 5, N'/Images/38WN95C.png', '10/04/2023 6:43:36 AM', N'Không có mô tả')
Insert into Products values('MAN003', 6, 8, N'Màn hình LG 32UN880-B 32" IPS 4K HDR 10 chuyên đồ họa', N'36 tháng', '20900000', 24, N'/Images/32UN880.png', '10/04/2023 6:43:36 AM', N'Không có mô tả')
--LAPTOP GAMING
Insert into Products values('LAPTOP001', 1, 4, N'Laptop ASUS ProArt Studiobook Pro 16 OLED W7600Z3A L2048W', N'36 tháng', '82990000', 16, N'/Images/W7600Z3A.png', '10/04/2023 6:43:36 AM', N'Không có mô tả')
Insert into Products values('LAPTOP002', 1, 4, N'Laptop ASUS Zenbook 17 Fold OLED UX9702AA MD014W', N'36 tháng', '89990000', 25, N'/Images/UX9702AA.png', '10/04/2023 6:43:36 AM', N'Không có mô tả')
Insert into Products values('LAPTOP003', 1, 4, N'Laptop ASUS VivoBook Pro 16X OLED K6602VV MX077W', N'36 tháng', '48990000', 10, N'/Images/K6602VV.png', '10/04/2023 6:43:36 AM', N'Không có mô tả')
Insert into Products values('LAPTOP004', 2, 4, N'Laptop gaming ACER Predator Helios 18 PH18 71 94SJ', N'36 tháng', '129990000', 20, N'/Images/PH18.png', '10/04/2023 6:43:36 AM', N'Không có mô tả')
Insert into Products values('LAPTOP005', 2, 4, N'Laptop Gaming Acer Aspire 7 A715 42G R05G', N'36 tháng', '63131631', 0, N'/Images/aspire7.png', '10/04/2023 6:43:36 AM', N'Không có mô tả')
--PC GAMING
Insert into Products values('PC001', 4, 3, N'Gigabyte AORUS Model S Gaming PC Computer Desktop (Intel i9-11900K, NVIDIA GeForce RTX 3080 GDDR6X 10GB, 32GB DDR4 RAM, 3TB M.2 SSD) - GB-AMSI9N8I-2051', N'36 tháng', '42373829', 20, N'/Images/AORUSMODELs.png', '10/04/2023 6:43:36 AM', N'Không có mô tả')
Insert into Products values('PC002', 4, 3, N'PC GIGABYTE AORUS MODEL X (I9-12900K/Z690/32GB RAM/2TB SSD/RTX3080/WL+BT/NO OS) (GB-AMXI9N8A-2171)', N'36 tháng', '87999000', 0, N'/Images/63668_pc_gigabyte_aorus_model_x_05.png', '10/04/2023 6:43:36 AM', N'Không có mô tả')
--Điện Thoại
Insert into Products values('IP001', 5, 1, N'iPhone 15 Pro Max 1TB Chính hãng (VN/A)', N'36 tháng', '46990000', 20, N'/Images/ip15.png', '10/04/2023 6:43:36 AM', N'Không có mô tả')
Insert into Products values('IP002', 5, 1, N'iPhone 14 Pro Max 512GB Chính hãng (VN/A)', N'36 tháng', '33790000', 12, N'/Images/ip14.png', '10/04/2023 6:43:36 AM', N'Không có mô tả')
Insert into Products values('IP003', 5, 1, N'iPhone 13 128GB Chính Hãng (VN/A)', N'36 tháng', '22990000', 5, N'/Images/ip13.png', '10/04/2023 6:43:36 AM', N'Không có mô tả')
Insert into Products values('IP004', 5, 1, N'iPhone 12 128GB Chính hãng (VN/A)', N'36 tháng', '17990000', 30, N'/Images/ip12.png', '10/04/2023 6:43:36 AM', N'Không có mô tả')

Insert into Products values('AR001', 3, 2, N'Samsung Galaxy S23 Ultra 12/512GB', N'36 tháng', '36990000', 20, N'/Images/s23ultra.png', '10/04/2023 6:43:36 AM', N'Không có mô tả')
Insert into Products values('AR002', 3, 2, N'Samsung Galaxy Z Fold5 5G 1TB', N'36 tháng', '51990000', 13, N'/Images/Fold5.png', '10/04/2023 6:43:36 AM', N'Không có mô tả')
Insert into Products values('AR003', 3, 2, N'Samsung Galaxy Z Flip3 5G 256GB', N'36 tháng', '26990000', 8, N'/Images/Flip3.png', '10/04/2023 6:43:36 AM', N'Không có mô tả')
Insert into Products values('AR004', 3, 2, N'Samsung Galaxy Z Fold4 5G 256GB', N'36 tháng', '409900000', 24, N'/Images/Fold4.png', '10/04/2023 6:43:36 AM', N'Không có mô tả')
--Insert table Customers
Insert into Customers values('0001','0568973369', N'Nguyễn Thị Hồng', 'hong@gmail.com', N'Đà Nẵng',N'/Images/user.png','User123@','2')
Insert into Customers values('0002','01222222222', N'Trần Văn An', 'an@gmail.com', N'Cần Thơ',N'/Images/user.png','User123@','1')
Insert into Customers values('0003','0568973369', N'Lê Thị Mai', 'mai@gmail.com', N'Nhà A',N'/Images/user.png','User123@','2')
Insert into Customers values('0004','01222222222', N'Phạm Đình Quang', 'quang@gmail.com', N'An Giang',N'/Images/user.png','User123@','1')
Insert into Customers values('0005','0568973369', N'Hoàng Thị Lan', 'lan@gmail.com', N'Bà Rịa-Vũng Tàu',N'/Images/user.png','User123@','2')
Insert into Customers values('0006','01222222222', N'Huỳnh Văn Minh', 'minh@gmail.com', N'Bắc Giang',N'/Images/user.png','User123@','1')
Insert into Customers values('0007','0568973369', N'Đặng Thị Thuận', 'thuan@gmail.com', N'Bắc Kạn',N'/Images/user.png','User123@','2')
Insert into Customers values('0008','01222222222', N'Bùi Đức Tuấn', 'tuan@gmail.com', N'Bạc Liêu',N'/Images/user.png','User123@','1')
Insert into Customers values('0009','0568973369', N'Võ Thị Hương', 'huong@gmail.com', N'Bắc Ninh',N'/Images/user.png','User123@','2')
Insert into Customers values('0010','01222222222', N'Đỗ Anh Dũng', 'dung@gmail.com', N'Bình Định',N'/Images/user.png','User123@','1')
Insert into Customers values('0011','0123456789', N'Phạm Thị Hòa', 'hoa@gmail.com', N'Đà Nẵng',N'/Images/user.png','User123@','1')
Insert into Customers values('0012','0123456789', N'Nguyễn Văn Hoàng', 'hoang@gmail.com', N'Đà Nẵng',N'/Images/user.png','User123@','2')
Insert into Customers values('0013','0123456789', N'Lê Văn An', 'an@gmail.com', N'Đà Nẵng',N'/Images/user.png','User123@','1')
Insert into Customers values('0014','0123456789', N'Trần Thị Mai', 'mai@gmail.com', N'Đà Nẵng',N'/Images/user.png','User123@','2')
Insert into Customers values('0015','0123456789', N'Huỳnh Đình Quang', 'quang@gmail.com', N'Đà Nẵng',N'/Images/user.png','User123@','1')
Insert into Customers values('0016','0123456789', N'Đặng Thị Lan', 'lan@gmail.com', N'Đà Nẵng',N'/Images/user.png','User123@','2')
Insert into Customers values('0017','0123456789', N'Bùi Văn Minh', 'minh@gmail.com', N'Đà Nẵng',N'/Images/user.png','User123@','1')
Insert into Customers values('0018','0123456789', N'Võ Thị Thuận', 'thuan@gmail.com', N'Đà Nẵng',N'/Images/user.png','User123@','2')
Insert into Customers values('0019','0123456789', N'Nguyễn Đức Tuấn', 'tuan@gmail.com', N'Đà Nẵng',N'/Images/user.png','User123@','1')
Insert into Customers values('0020','0123456789', N'Lê Thị Hương', 'huong@gmail.com', N'Đà Nẵng',N'/Images/user.png','User123@','2')
Insert into Customers values('0021','0123456789', N'Nguyễn Thị Dung', 'dung@gmail.com', N'Đà Nẵng',N'/Images/user.png','User123@','1')
Insert into Customers values('0022','0123456789', N'Trần Văn Chí', 'chi@gmail.com', N'Đà Nẵng',N'/Images/user.png','User123@','2')
Insert into Customers values('0023','0123456789', N'Lê Văn Tú', 'tu@gmail.com', N'Đà Nẵng',N'/Images/user.png','User123@','1')
Insert into Customers values('0024','0123456789', N'Phạm Thị Huệ', 'hue@gmail.com', N'Đà Nẵng',N'/Images/user.png','User123@','2')
Insert into Customers values('0025','0123456789', N'Nguyễn Văn Hùng', 'hung@gmail.com', N'Đà Nẵng',N'/Images/user.png','User123@','1')
Insert into Customers values('0026','0123456789', N'Trần Văn Đức', 'duc@gmail.com', N'Đà Nẵng',N'/Images/user.png','User123@','2')
Insert into Customers values('0027','0123456789', N'Đặng Thị Ánh', 'anh@gmail.com', N'Đà Nẵng',N'/Images/user.png','User123@','1')
Insert into Customers values('0028','0123456789', N'Bùi Văn Hiếu', 'hieu@gmail.com', N'Đà Nẵng',N'/Images/user.png','User123@','2')
Insert into Customers values('0029','0123456789', N'Võ Thị Kim', 'kim@gmail.com', N'Đà Nẵng',N'/Images/user.png','User123@','1')
Insert into Customers values('0030','0123456789', N'Nguyễn Văn Hải', 'hai@gmail.com', N'Đà Nẵng',N'/Images/user.png','User123@','2')

--Insert table Orders


--Insert table OrderDetails

--Insert table Rates
Insert into Rates values('RTX001', 2)
Insert into Rates values('RTX002', 5)
Insert into Rates values('RTX003', 3)
Insert into Rates values('RTX004', 4)
Insert into Rates values('AMD001', 5)
Insert into Rates values('AMD002', 4)
Insert into Rates values('AMD003', 3)
Insert into Rates values('CPU001', 3)
Insert into Rates values('CPU002', 4)
Insert into Rates values('CPU003', 5)
Insert into Rates values('CPU004', 2)
Insert into Rates values('CPU005', 3)
Insert into Rates values('RAM001', 4)
Insert into Rates values('RAM002', 4)
Insert into Rates values('LAPTOP001', 5)
Insert into Rates values('LAPTOP002', 2)
Insert into Rates values('LAPTOP003', 3)
Insert into Rates values('LAPTOP004', 4)
Insert into Rates values('LAPTOP005', 4)
Insert into Rates values('PC001', 3)
Insert into Rates values('PC002', 5)
Insert into Rates values('IP001', 4)
Insert into Rates values('IP002', 5)
Insert into Rates values('IP003', 2)
Insert into Rates values('IP004', 3)
Insert into Rates values('AR001', 4)
Insert into Rates values('AR002', 4)
Insert into Rates values('AR003', 3)
Insert into Rates values('AR004', 5)
----Insert table Comments

--Order
Insert into Orders values('HD0001','0001', '0568973369',N'Đà Nẵng', N'Trống', '11/20/2023 10:40:30 AM', N'0')
Insert into Orders values('HD0002','0002', '01222222222',N'Cần Thơ', N'Trống', '11/21/2023 11:50:45 AM', N'0')
Insert into Orders values('HD0003','0003', '0568973369',N'Nhà A', N'Trống', '11/22/2023 09:15:15 AM', N'0') 
Insert into Orders values('HD0004','0004', '01222222222',N'An Giang', N'Trống', '11/23/2023 10:30:45 AM', N'0')
Insert into Orders values('HD0005','0005', '0568973369',N'Bà Rịa-Vũng Tàu', N'Trống', '11/21/2023 02:45:15 PM', N'0')
Insert into Orders values('HD0006','0006', '01222222222',N'Bắc Giang', N'Trống', '11/23/2023 11:15:00 AM', N'0')
Insert into Orders values('HD0007','0007', '0568973369',N'Bắc Kạn', N'Trống', '11/23/2023 08:30:30 AM', N'0')  
Insert into Orders values('HD0008','0008', '01222222222',N'Bạc Liêu', N'Trống', '11/23/2023 01:20:45 PM', N'0')
Insert into Orders values('HD0009','0009', '0568973369',N'Bắc Ninh', N'Trống', '11/23/2023 04:10:20 AM', N'0')
Insert into Orders values('HD0010','0010', '01222222222',N'Bình Định', N'Trống', '11/23/2023 05:45:30 PM', N'0')  
Insert into Orders values('HD0011','0011', '0123456789',N'Đà Nẵng', N'Trống', '11/23/2023 12:15:45 AM', N'0')
Insert into Orders values('HD0012','0012', '0123456789',N'Đà Nẵng',  N'Trống', '11/23/2023 03:20:15 PM', N'0')
Insert into Orders values('HD0013','0013', '0123456789',N'Đà Nẵng',  N'Trống', '11/15/2023 06:05:00 AM', N'0')
Insert into Orders values('HD0014','0014', '0123456789',N'Đà Nẵng',  N'Trống', '11/22/2023 09:45:15 AM', N'0')
Insert into Orders values('HD0015','0015', '0123456789',N'Đà Nẵng',  N'Trống', '11/21/2023 11:35:30 PM', N'0')
Insert into Orders values('HD0016','0016', '0123456789',N'Đà Nẵng',  N'Trống', '11/21/2023 01:25:45 AM', N'0')   
Insert into Orders values('HD0017','0017', '0123456789',N'Đà Nẵng',  N'Trống', '11/21/2023 08:15:00 PM', N'0')
Insert into Orders values('HD0018','0018', '0123456789',N'Đà Nẵng',  N'Trống', '11/23/2023 10:05:30 AM', N'0')  
Insert into Orders values('HD0019','0019', '0123456789',N'Đà Nẵng',  N'Trống', '11/21/2023 11:55:45 PM', N'0')
Insert into Orders values('HD0020','0020', '0123456789',N'Đà Nẵng',  N'Trống', '11/21/2023 01:45:00 PM', N'0')
Insert into Orders values('HD0021','0021', '0123456789',N'Đà Nẵng',  N'Trống', '11/21/2023 03:35:15 AM', N'0') 
Insert into Orders values('HD0022','0022', '0123456789',N'Đà Nẵng',  N'Trống', '11/21/2023 05:25:30 PM', N'0')
Insert into Orders values('HD0023','0023', '0123456789',N'Đà Nẵng',  N'Trống', '11/21/2023 07:15:45 AM', N'0')  
Insert into Orders values('HD0024','0024', '0123456789',N'Đà Nẵng',  N'Trống', '11/22/2023 09:05:00 AM', N'0') 
Insert into Orders values('HD0025','0025', '0123456789',N'Đà Nẵng',  N'Trống', '11/23/2023 10:55:15 PM', N'0')
Insert into Orders values('HD0026','0026', '0123456789',N'Đà Nẵng',  N'Trống', '11/22/2023 12:45:30 AM', N'0')  
Insert into Orders values('HD0027','0027', '0123456789',N'Đà Nẵng',  N'Trống', '11/21/2023 02:35:45 PM', N'0')
Insert into Orders values('HD0028','0028', '0123456789',N'Đà Nẵng',  N'Trống', '11/21/2023 04:25:00 AM', N'0')
Insert into Orders values('HD0029','0029', '0123456789',N'Đà Nẵng',  N'Trống', '11/22/2023 06:15:15 PM', N'0')  
Insert into Orders values('HD0030','0030', '0123456789',N'Đà Nẵng',  N'Trống', '11/20/2023 08:05:30 AM', N'0')
--Insert table OrderDetails
Insert into OrderDetails values('HD0001', 'LAPTOP001', 88, '82990000')
Insert into OrderDetails values('HD0002', 'LAPTOP001', 21, '82990000')
Insert into OrderDetails values('HD0003', 'LAPTOP001', 61, '82990000')
Insert into OrderDetails values('HD0004', 'LAPTOP001', 90, '82990000')
Insert into OrderDetails values('HD0005', 'LAPTOP001', 58, '82990000')
Insert into OrderDetails values('HD0006', 'LAPTOP001', 85, '82990000')  
Insert into OrderDetails values('HD0007', 'LAPTOP001', 68, '82990000')
Insert into OrderDetails values('HD0008', 'LAPTOP001', 18, '82990000')
Insert into OrderDetails values('HD0009', 'LAPTOP001', 84, '82990000')
Insert into OrderDetails values('HD0010', 'LAPTOP001', 56, '82990000')
Insert into OrderDetails values('HD0011', 'LAPTOP001', 78, '82990000')
Insert into OrderDetails values('HD0012', 'LAPTOP001', 64, '82990000')
Insert into OrderDetails values('HD0013', 'LAPTOP001', 36, '82990000')  
Insert into OrderDetails values('HD0014', 'LAPTOP001', 29, '82990000')
Insert into OrderDetails values('HD0015', 'LAPTOP001', 74, '82990000') 
Insert into OrderDetails values('HD0016', 'LAPTOP001', 80, '82990000')
Insert into OrderDetails values('HD0017', 'LAPTOP001', 12, '82990000')
Insert into OrderDetails values('HD0018', 'LAPTOP001', 41, '82990000') 
Insert into OrderDetails values('HD0019', 'LAPTOP001', 33, '82990000')
Insert into OrderDetails values('HD0020', 'LAPTOP001', 96, '82990000')
Insert into OrderDetails values('HD0021', 'LAPTOP001', 87, '82990000')  
Insert into OrderDetails values('HD0022', 'LAPTOP001', 52, '82990000')
Insert into OrderDetails values('HD0023', 'LAPTOP001', 89, '82990000')
Insert into OrderDetails values('HD0024', 'LAPTOP001', 67, '82990000')
Insert into OrderDetails values('HD0025', 'LAPTOP001', 50, '82990000')
Insert into OrderDetails values('HD0026', 'LAPTOP001', 98, '82990000') 
Insert into OrderDetails values('HD0027', 'LAPTOP001', 10, '82990000')
Insert into OrderDetails values('HD0028', 'LAPTOP001', 60, '82990000')
Insert into OrderDetails values('HD0029', 'LAPTOP001', 27, '82990000')  
Insert into OrderDetails values('HD0030', 'LAPTOP001', 13, '82990000')