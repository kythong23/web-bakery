use master
go 
if exists (select * from sysdatabases where name = 'bakery')
	drop database bakery
go 
create database bakery 
go 
use bakery
go

create table KHACHHANG (
	MaKH int identity(1,1),
	HoTen nvarchar (100) NOT NULL,
	Email varchar (100) unique,
	SDT char(12),
	DiaChi nvarchar(100) not null,
	MatKhau varchar (100) NOT NULL,
	PRIMARY KEY (MaKH)
)
create table LOAISANPHAM(
	MaLoai char(4) NOT NULL,
	TenLoai nvarchar (100) NOT NULL,
	PRIMARY KEY (MaLoai)
)
create table SANPHAM (
	MaSP char (4) NOT NULL,
	TenSP nvarchar (100) NOT NULL,
	NgaySX date NOT NULL,
	Gia int NOT NULL,
	HinhSP varchar (50) NOT NULL,
	MaLoai char(4) NOT NULL,
	RateSP float,
	ReviewSP int,
	MotaSP nvarchar(max),
	PRIMARY KEY (MaSP),
	FOREIGN KEY (MaLoai) REFERENCES LOAISANPHAM (MaLoai)

)
create table HINHTHUCGIAOHANG 
(
	MaHT int identity(1,1),
	TenHT nvarchar (50),
	PRIMARY KEY (MaHT)
)
create table TINHTRANGDONHANG 
(
	MaTT int identity(1,1),
	LoaiTT nvarchar (50),
	PRIMARY KEY (MaTT)
)
create table DONHANG (
	MaDH int identity(1,1),
	NgayDat date NOT NULL,
	TongGia int,
	TenNN nvarchar (100),
	SDT char(12),
	DiaChiNhanHang nvarchar(100) NOT NULL,
	MaKH int NOT NULL,
	MaHT int NOT NULL,
	MaTT int NOT NULL,
	PRIMARY KEY (MADH),
	FOREIGN KEY (MaKH) REFERENCES KHACHHANG (MaKH),
	FOREIGN KEY (MaTT) REFERENCES TINHTRANGDONHANG (MaTT),
	FOREIGN KEY (MaHT) REFERENCES HINHTHUCGIAOHANG (MaHT)
)
create table CHITIETDIKEM(
	MACTSPK INT NOT NULL IDENTITY(1,1),
	TenSPK nvarchar (50) not null,
	Soluong int,
	PRIMARY KEY (MACTSPK)
)
CREATE TABLE CHITIETDONHANG (
	MACTDH INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    MaDH int NOT NULL,
    MaSP char(4) NOT NULL,
    MASPK INT, 
    SoLuong int NOT NULL,
    ThanhTien int NOT NULL,
    FOREIGN KEY (MaDH) REFERENCES DONHANG (MaDH),
    FOREIGN KEY (MaSP) REFERENCES SANPHAM (MaSP)
)
CREATE TABLE SANPHAMDIKEM (
	MASPK INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    MaCTSPK INT,
	MACTDH INT,
	FOREIGN KEY (MACTDH) REFERENCES CHITIETDONHANG(MACTDH),
	FOREIGN KEY (MaCTSPK) REFERENCES CHITIETDIKEM (MACTSPK)
)
create table Admin 
(
	UserAd varchar (100) primary key,
	PassAd varchar (100),
	HoTen nvarchar (50),
	VaiTro varchar(30)
)
insert into admin values ('admin','admin123456','Le Tran Ky Nhong','ADMIN')

INSERT INTO LOAISANPHAM (MaLoai, TenLoai)
VALUES ('L001', N'Sản phẩm đặc trưng'),
('L002',N'Bánh sinh nhật'),
('L003',N'Bánh tươi'),
('L004',N'Bánh quy'),
('L005',N'Bánh mì');

INSERT INTO SANPHAM 
VALUES 
		('SP01','Heart Shape Fruit Nut Cake','2023-09-26', 200000,'product1.jpg','L002',3.9,400,N'Chiếc bánh trái tim hình trái tim thơm ngon này chắc chắn sẽ là
		tâm điểm thu hút trong mỗi dịp đặc biệt của bạn. Được trang trí bằng các loại trái cây nhiệt đới mới cắt lát, chiếc bánh này có hương vị kem vani mới
		đánh bông. Và, để làm nổi bật giữa đám đông, chiếc bánh này đã được trang trí bằng các loại hạt giòn ở hai bên. Xin lưu ý rằng các loại trái cây có thể 
		thay đổi tùy theo tình trạng sẵn có theo mùa.'),
		('SP02', 'Belgian Chocolate Cake', '2023-09-26', 500000, 'product2.jpg', 'L003',4.5,800,N'Một bản giao hưởng của hương vị sang trọng đang chờ đợi những
		người hâm mộ bánh sành điệu, mang đến một dư vị mượt mà kéo dài trong các giác quan. Căn cứ bọt biển sô cô la ẩm hoàn hảo hợp tác với sự phủ sương choco mousse
		đầy đủ, tạo ra một cảm giác suy đồi dễ dàng tan chảy trên vòm miệng. Bắt tay vào một hành trình tinh tế ngon lành khi bạn thưởng thức từng miếng cắn tinh tế, 
		được nâng cao hơn nữa bởi lớp phủ kem đường ống thanh lịch và lớp phủ xịt sô cô la xa hoa. Bánh này là một biểu hiện của nghệ thuật ẩm thực, được thiết kế để
		nuông chiều các khẩu vị tinh tế nhất và đắm mình trong cõi cuối cùng của sự nuông chiều. Thưởng thức trong mẫu mực của người sành ăn thích thú với bánh 
		mousse sô cô la Bỉ tinh tế của chúng tôi.'),
		('SP03','Dripping Butterscotch Cream Cake','2023-09-26', 2000000,'product3.jpg','L003',4.2,1230,N'Hòa mình vào bản giao hưởng ngọt ngào với Bánh kem Butterscotch
		này! Mỗi lớp mở ra một sự pha trộn đầy mê hoặc của vị béo ngậy của bơ và lớp vỏ caramel vàng giòn. Khi lớp kem mềm mại đáp ứng vị giác của bạn, hương vị
		bánh bơ hấp dẫn nhảy múa trong một sự hài hòa đầy mê hoặc, hứa hẹn một cảm giác sảng khoái trong từng miếng cắn. Được trang trí với những đường xoáy kem
		thơm ngon và những giọt caramel bóng loáng, chiếc bánh này không chỉ là một món ăn ngon cho khẩu vị mà còn là một bữa tiệc cho đôi mắt. Hãy thưởng thức,
		vì chiếc bánh này là nơi hương vị mơ màng cuộn xoáy và cảm giác thèm ăn suy đồi được thỏa mãn một cách tuyệt vời. Một mảnh trời, quả thực không thể cưỡng 
		lại!'),
		('SP04','Kit Kat Chocolate Pull Me Up Cake','2023-09-26', 1500000,'product4.jpg','L001',4.9,1500,N'Một chiếc bánh sô cô la kitkat kéo tôi lên, được trang trí bằng
		một thanh sô cô la Kitkat ở trên cùng của chiếc bánh. Món ăn ngọt ngào và sô cô la này chắc chắn sẽ kéo bạn lên trong niềm hạnh phúc ngọt ngào. 
		Một món ăn ngon hoặc một miếng cắn nhanh, chiếc bánh này sẽ khiến bạn quay trở lại để ăn thêm.'),
		('SP05','Enchanting Pink Rose Cake','2023-09-26', 749000,'product5.jpg','L002',3.9,400,N'Một chiếc bánh hoa hồng cuốn hút'),
		('SP06', 'Fudgy Walnut Choco Brownie', '2023-09-26', 109999, 'product6.jpg', 'L003',4.5,800,N'Món bánh hạnh nhân óc chó thơm ngon đậm đà và thơm ngon này rất phù hợp 
		để thỏa mãn cơn thèm của một người hoặc dùng làm quà tặng. Là món ăn kèm hoàn hảo cho trà, cà phê và các đồ uống nóng/lạnh khác, bộ bánh hạnh nhân óc chó cổ điển
		này chắc chắn sẽ khiến người ta chảy nước miếng với hương vị đậm đà, kết cấu và sức hấp dẫn tuyệt vời.'),
		('SP07','Delicious Happy Birthday Brownie','2023-09-26', 299999,'product7.jpg','L003',4.2,1230,N'Những chiếc bánh hạnh nhân sô cô la chúc mừng sinh nhật này là một 
		cách tuyệt vời để thêm vào như một món ngọt. Được cả trẻ em và người lớn yêu thích, loại bánh hạnh nhân sô cô la đậm đà này kết hợp hương vị tinh tế của sô cô la 
		và các loại hạt. Với dòng chữ Chúc mừng sinh nhật trên đó, những chiếc bánh hạnh nhân này sẽ là món quà nhỏ hoàn hảo.'),
		('SP08','Yummy And Adorable Cocomelon Cake','2023-09-26', 509999,'product8.jpg','L001',4.9,1500,N'Thỏa mãn cơn thèm ngọt cùng với việc làm hài lòng đôi mắt của những
		đứa trẻ đáng yêu của bạn. Hãy mang về nhà chiếc bánh Cocomelon thơm ngon và dễ thương này và khiến họ ngạc nhiên đến tận xương tủy. Làm cho lễ kỷ niệm thêm vui vẻ với
		chiếc bánh dễ thương nhất trong thị trấn này. Hãy đặt hàng ngay món ngon này nhé!'),
		('SP09', 'Happy First Birthday Cake', '2023-09-26', 500000, 'product9.jpg', 'L003',4.5,800,N'Bánh Kem Tròn Chúc Mừng Sinh Nhật Màu Hồng'),
		('SP10','Free Fire Birthday Cake','2023-09-26', 2000000,'product10.jpg','L003',4.2,1230,N'Bánh chủ đề sinh nhật Free Fire Fondant'),
		('SP11','Red Velvet Single Jar Cake','2023-09-26', 1500000,'product11.jpg','L001',4.9,1500,N'Không gì có thể sánh bằng sự ngon lành của một chiếc bánh hũ nhung đỏ thơm
		ngon và khi được dùng làm một chiếc bánh hũ, chắc chắn nó sẽ lan tỏa hạnh phúc và những cảm xúc tốt đẹp khác đến những người thân yêu của bạn. Vì vậy, những gì chờ đợi? 
		Dù vậy, bất kỳ dịp đặc biệt nào cũng hãy biến nó thành một dịp tuyệt vời nhất có thể bằng cách tổ chức nó trên một chiếc bánh hũ béo ngậy, mịn như nhung, như thế này.'),
		('SP12', 'Choco-Chips And Fruit Jar Cake Combo', '2023-09-26', 500000, 'product12.jpg', 'L003',4.5,800,N'Người đặc biệt của bạn có phải là người yêu thích món tráng 
		miệng thực sự không? Nếu đúng như vậy, đã đến lúc thưởng thức cho họ món kết hợp siêu xa hoa này gồm bánh choco-chip tan chảy và giòn cùng với hương vị của bánh trái cây
		tươi. Họ sẽ không thốt nên lời khi khen ngợi chiếc bánh này.'),
		('SP13','Heavenly Red Velvet Cupcake','2023-09-26', 2000000,'product13.jpg','L003',4.2,1230,N'Bánh cupcake nhung đỏ không trứng mềm phủ lớp kem phủ trắng mịn và rắc nhung đỏ.'),
		('SP14','Moist and Fluffy Marble Cake','2023-09-26', 1500000,'product14.jpg','L001',4.9,1500,N'Với hương vị vani và sô cô la mềm mại, chiếc bánh đá cẩm thạch ẩm, mềm và mịn 
		này sẽ làm hài lòng vị giác của bạn. Đó là một loại bánh trà tuyệt vời có thể dùng như một món ăn nhẹ hoàn hảo với trà hoặc cà phê. Nó có thể được mọi lứa tuổi thưởng thức
		và là một món quà hoàn hảo cho bất kỳ dịp nào.'),
		('SP15', 'Three Tier Animal Kingdom Cake', '2023-09-26', 678666, 'product15.jpg', 'L003',4.5,800,N'Hãy thỏa mãn và khơi dậy sự tò mò của bạn với chiếc bánh tầng có chủ đề động
		vật tuyệt đẹp của chúng tôi. Kiệt tác ba tầng này được trang trí bằng kẹo cao su, voi, sư tử, hổ và ngựa vằn, tạo nên khung cảnh động vật hoang dã quyến rũ. Chiếc bánh còn có 
		thiết kế hình chiếc lá, vô cùng phù hợp với khung cảnh rừng rậm! Hoàn hảo cho những người đam mê rừng rậm, chiếc bánh này kết hợp tính nghệ thuật và sự độc đáo để tạo ra một món
		ăn trung tâm thực sự đẹp mắt. Hãy để bản thân và những đứa con cưng của bạn khám phá những điều kỳ diệu của vương quốc động vật khi chúng thưởng thức từng miếng ngon lành.'),
		('SP16','Groot Fondant Cake','2023-09-26', 111399,'product16.jpg','L003',4.2,1230,N'Nhân vật hư cấu Groot của Marvel là một nhân vật đáng yêu. Vì vậy, hãy yêu mến những người 
		thân yêu của bạn trong ngày sinh nhật của họ bằng cách tặng họ chiếc bánh I Am Groot do nhà thiết kế của chúng tôi thiết kế. Chiếc bánh được phủ một số lá kẹo mềm màu xanh 
		lá cây và phần hoàn thiện thô để tạo ra một cái nhìn gần giống như một cái cây.');

INSERT INTO HINHTHUCGIAOHANG 
Values (N'Thanh toán tại nhà'),
		(N'Thanh toán bằng ví điện tử');

INSERT INTO TINHTRANGDONHANG
Values (N'Chưa giao'),
		(N'Đã giao');
INSERT INTO CHITIETDIKEM
Values	(N'Set muỗng, đĩa, dao cắt bánh',10),
		(N'Nến',10),
		(N'Kẹo cốm',10);
