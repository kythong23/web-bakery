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
	MaKH char (4) NOT NULL,
	HoTen nvarchar (100) NOT NULL,
	DiachiKH nvarchar (100) NOT NULL,
	GioiTinh nvarchar(10) NOT NULL,
	SDT int NOT NULL,
	Email nvarchar (100) NOT NULL,
	MatKhau nvarchar (100) NOT NULL,
	PRIMARY KEY (MaKH)
)

create table SANPHAM (
	MaSP char (4) NOT NULL,
	TenSP nvarchar (100) NOT NULL,
	NgaySX date NOT NULL,
	Gia int NOT NULL,
	HinhSP varchar (50) NOT NULL,
	MaLoai char(4) NOT NULL,
	PRIMARY KEY (MaSP),
	FOREIGN KEY (MaLoai) REFERENCES LOAISANPHAM (MaLoai)

)
create table LOAISANPHAM(
	MaLoai char(4) NOT NULL,
	TenLoai nvarchar (100) NOT NULL,
	PRIMARY KEY (MaLoai)
)
create table DONHANG (
	MaDH char (4) NOT NULL,
	NgayDat date NOT NULL,
	DiaChiNhanHang nvarchar(100) NOT NULL,
	HTThanhToan Bit Default 0,
	MaKH char(4) NOT NULL,
	PRIMARY KEY (MADH),
	FOREIGN KEY (MaKH) REFERENCES KHACHHANG (MaKH)
)
create table CHITIETDONHANG (
	MaDH char (4) NOT NULL,
	MaSP char(4) NOT NULL,
	SoLuong int NOT NULL,
	ThanhTien int NOT NULL,
	PRIMARY KEY (MaDH,MaSP),
	FOREIGN KEY (MaDH) REFERENCES DONHANG (MaDH),
	FOREIGN KEY (MaSP) REFERENCES SANPHAM (MaSP)
)
Alter table SANPHAM add RateSP float;
Alter table SANPHAM add ReviewSP int;
Alter table SANPHAM add MotaSP nvarchar(max);
Alter table SANPHAM drop column MotaSP;
INSERT INTO KHACHHANG
VALUES ('KH01', N'Nguyễn Văn A', N'123 Nguyễn Văn Chổi', N'Nam', 0901112356, 'abc@gmail.com', 'Nguyenvana123');

Delete from SANPHAM
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
		('SP05','Enchanting Pink Rose Cake','2023-09-26', 749000,'product5.jpg','L002',3.9,400,N'Một chiếc bánh hoa hồng cuốn hút');


DELETE From LOAISANPHAM
INSERT INTO LOAISANPHAM (MaLoai, TenLoai)
VALUES ('L001', N'Sản phẩm đặc trưng'),
('L002',N'Bánh sinh nhật'),
('L003',N'Bánh tươi'),
('L004',N'Bánh quy'),
('L005',N'Bánh mì');


INSERT INTO DONHANG (MaDH, NgayDat, DiaChiNhanHang, HTThanhToan, MaKH)
VALUES ('DH01', '2022-02-01', '456 XYZ Street', 1, 'KH01');

INSERT INTO CHITIETDONHANG (MaDH, MaSP, SoLuong, ThanhTien)
VALUES ('DH01', 'SP01', 2, 20000);