using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebKhachSan.Models;

namespace WebKhachSan.Controllers
{

    public class ValuesController : ApiController
    {
        QuanLyKhachSanDataContext db = new QuanLyKhachSanDataContext();
        // GET api/values
        //api xem đánh giá
        [HttpGet]
        [Route("dg")]
        public List<tDanhGia> HienDanhGia()
        {
            return db.tDanhGias.ToList();
        }
        [HttpPost]
        [Route("dg/send")]
        public bool GuiDanhGia(string MaDG,string NoiDung,string NguoiDG,string Email,string Diachi,char vitri,char phucvu,char tiennghi,char vesinh,char giaca,char monan)
        {
            try
            {
                tDanhGia dg = new tDanhGia();
                dg.MaDG = MaDG;
                dg.NoiDung = NoiDung;
                dg.NguoiDanhGia = NguoiDG;
                dg.email = Email;
                dg.DiaChi = Diachi;
                dg.ViTri = vitri;
                dg.PhucVu = phucvu;
                dg.TienNghi = tiennghi;
                dg.VeSinh = vesinh;
                dg.MonAn = monan;
                db.tDanhGias.InsertOnSubmit(dg);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [HttpDelete]
        [Route("dg/del")]
        public bool XoaDanhGia(string MaDG)
        {
            try
            {
                tDanhGia dg = db.tDanhGias.Where(n => n.MaDG.Equals(MaDG)).FirstOrDefault();
                if (dg == null) return false;
                db.tDanhGias.DeleteOnSubmit(dg);
                db.SubmitChanges();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }



        //các API về phòng

        //Hiện các loại phòng
        [HttpGet]
        [Route("lp")]
        public List<tLoaiPhong> HienLoaiPhong()
        {
            return db.tLoaiPhongs.ToList();
        }
        //Thêm loại phòng
        [HttpPost]
        [Route("lp/add")]
        public bool AddType(string loai,string mota,int dongia,string hinhanh,string dientich,char sogiuong,char sophongngu,char bontam,char sophongtam,double gia)
        {
            try
            {
                tLoaiPhong lp = new tLoaiPhong();
                lp.LoaiPhong = loai;
                lp.MoTa = mota;
                lp.DonGia = dongia;
                lp.HinhAnh = hinhanh;
                lp.DienTich = dientich;
                lp.SoGiuong = sogiuong;
                lp.SoPhongNgu = sophongngu;
                lp.BonTam = bontam;
                lp.SoPhongTam = sophongtam;
                lp.Gia = gia;
                db.tLoaiPhongs.InsertOnSubmit(lp);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //Cập nhật thông tin về loại phòng
        [HttpPut]
        [Route("lp/update")]
        public bool UpdateType(string loai, string mota, int dongia, string hinhanh, string dientich, char sogiuong, char sophongngu, char bontam, char sophongtam, double gia)
        {
            try
            {
                tLoaiPhong lp = db.tLoaiPhongs.Where(n => n.LoaiPhong.Equals(loai)).FirstOrDefault();
                if (lp == null) return false;
                else
                {
                    lp.LoaiPhong = loai;
                    lp.MoTa = mota;
                    lp.DonGia = dongia;
                    lp.HinhAnh = hinhanh;
                    lp.DienTich = dientich;
                    lp.SoGiuong = sogiuong;
                    lp.SoPhongNgu = sophongngu;
                    lp.BonTam = bontam;
                    lp.SoPhongTam = sophongtam;
                    lp.Gia = gia;
                    db.SubmitChanges();
                    return true;
                }    
            }
            catch
            {
                return false;
            }
        }
        //Xóa 1 loại phòng
        [HttpDelete]
        [Route("lp/delete")]
        public bool DeleteType(string loai)
        {
            try
            {
                tLoaiPhong lp = db.tLoaiPhongs.Where(n => n.LoaiPhong.Equals(loai)).FirstOrDefault();
                if (lp == null) return false;
                else
                {
                    db.tLoaiPhongs.DeleteOnSubmit(lp);
                    db.SubmitChanges();
                    return true;
                }    
            }
            catch
            {
                return false;
            }
        }

        //Phòng
        //Hiện danh sách phòng trống
        [HttpGet]
        [Route("room")]
        public List<tSoPhong_LoaiPhong> DSPhongTrong()
        {
            return db.tSoPhong_LoaiPhongs.Where(n => n.GhiChu.Equals("Empty")).ToList();
        }
        //Hiện danh sách phòng theo loại phòng
        [HttpGet]
        [Route("room/lp")]
        public List<tSoPhong_LoaiPhong> RoomByType(string type)
        {
            return db.tSoPhong_LoaiPhongs.Where(n => n.LoaiPhong.Equals(type)).ToList();
        }
        //Hiện phòng theo tên ( số ) phòng
        [HttpGet]
        [Route("room")]
        public List<tSoPhong_LoaiPhong> RoomByNumb(string name)
        {
            return db.tSoPhong_LoaiPhongs.Where(n => n.SoPhong.Contains(name)).ToList();
        }
        //Thêm 1 phòng
        [HttpPost]
        [Route("room/add")]
        public bool AddRoom(string name, string type, string notice)
        {
            try
            {
                tSoPhong_LoaiPhong r = new tSoPhong_LoaiPhong();
                    r.SoPhong = name;
                    r.LoaiPhong = type;
                    r.GhiChu = notice;
                db.tSoPhong_LoaiPhongs.InsertOnSubmit(r);
                    db.SubmitChanges();
                    return true;
            }
            catch
            {
                return false;
            }
        }
        //Cập nhật thông cho 1 phòng. Khi xóa 1 phòng thì chuyển ghi chú của phòng đó thành "Ngừng kinh doanh"
        [HttpPut]
        [Route("room/update")]
        public bool UpdateRoom(string name,string type,string notice)
        {
            try
            {
                tSoPhong_LoaiPhong r = db.tSoPhong_LoaiPhongs.Where(n => n.SoPhong.Equals(name)).FirstOrDefault();
                if (r == null) return false;
                else
                {
                    r.SoPhong = name;
                    r.LoaiPhong = type;
                    r.GhiChu = notice;
                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        





        //API khách hàng
        //Danh sach khach hang
        [HttpGet]
        [Route("customer")]
        public List<tChiTietKH> GetAllCustomer()
        {
            return db.tChiTietKHs.ToList();
        }
        //Thêm khách hàng
        [HttpPost]
        [Route("customer/add")]
        public bool AddCustomer(string id,string type,string name,DateTime dob,bool sex,string address,string phonenumb,int point,string identify,string pass)
        {
            try
            {
                tChiTietKH kh = new tChiTietKH();
                kh.MaKH = id;
                kh.LoaiKH = type;
                kh.TenKH = name;
                kh.NgaySinh = dob;
                kh.Phai = sex;
                kh.DiaChi = address;
                kh.DienThoai = phonenumb;
                kh.DiemTichLuy = 0;
                kh.TheCanCuoc = identify;
                kh.password = pass;
                db.tChiTietKHs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        // xóa khách hàng
        [HttpDelete]
        [Route("customer/delete")]
        public bool DeleteCustomer(string id)
        {
            try
            {
                tChiTietKH kh = db.tChiTietKHs.Where(n => n.MaKH.Equals(id)).FirstOrDefault();
                if (kh == null) return false;
                else
                {
                    db.tChiTietKHs.DeleteOnSubmit(kh);
                    db.SubmitChanges();
                    return true;
                }    
            }
            catch
            {
                return false;
            }
        }


        //sửa thông tin khách
        [HttpPut]
        [Route("customer/update")]
        public bool UpdateCustomer(string id, string type, string name, DateTime dob, bool sex, string address, string phonenumb, int point, string identify, string pass)
        {
            try
            {
                tChiTietKH kh = db.tChiTietKHs.Where(n => n.MaKH.Equals(id)).FirstOrDefault();
                if (kh == null) return false;
                else
                {
                    kh.MaKH = id;
                    kh.LoaiKH = type;
                    kh.TenKH = name;
                    kh.NgaySinh = dob;
                    kh.Phai = sex;
                    kh.DiaChi = address;
                    kh.DienThoai = phonenumb;
                    kh.DiemTichLuy = 0;
                    kh.TheCanCuoc = identify;
                    kh.password = pass;
                    db.SubmitChanges();
                    return true;
                }    
            }
            catch
            {
                return false;
            }
        }
        //tìm kiếm khách hàng theo mã
        [HttpGet]
        [Route("customer")]
        public List<tChiTietKH> GetCustomerById(string id)
        {
            return db.tChiTietKHs.Where(n => n.MaKH.Contains(id)).ToList();
        }
        //tìm kiếm khách hàng theo tên
        [HttpGet]
        [Route("customer")]
        public List<tChiTietKH> GetCustomerByName(string name)
        {
            return db.tChiTietKHs.Where(n => n.TenKH.Contains(name)).ToList();
        }
        //Danh sách các loại khách hàng
        [HttpGet]
        [Route("custype")]
        public List<tLoaiKhachHang> GetAllCusType()
        {
            return db.tLoaiKhachHangs.ToList();
        }

        //API đặt phòng
        //Danh sách đơn đặt
        [HttpGet]
        [Route("order")]
        public List<tDatPhong> GetAllOrder()
        {
            return db.tDatPhongs.ToList();
        }
        //Tìm kiếm đơn theo mã đăt phòng
        [HttpGet]
        [Route("order/id")]
        public List<tDatPhong> GetOrderById(string id)
        {
            return db.tDatPhongs.Where(n => n.MaDP.Contains(id)).ToList();
        }
        //Tìm đơn theo mã người đặt phòng
        [HttpGet]
        [Route("order/customer")]
        public List<tDatPhong> GetOrderByCustomer(string cusid)
        {
            return db.tDatPhongs.Where(n => n.MaKH.Contains(cusid)).ToList();
        }
        //Thêm đơn
        [HttpPost]
        [Route("order/add")]
        public bool AddOrder(string id,string cusid,string address,string companyName,string code,string companyAddress)
        {
            try
            {
                tDatPhong dp = new tDatPhong();
                dp.MaDP = id;
                dp.MaKH = cusid;
                dp.Diachi = address;
                dp.TenCongTy = companyName;
                dp.MaSoThue = code;
                dp.DiaChiCty = companyAddress;
                db.tDatPhongs.InsertOnSubmit(dp);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //Xóa đơn
        [HttpDelete]
        [Route("order/delete")]
        public bool DeleteOrder(string id)
        {
            try
            {
                tDatPhong dp = db.tDatPhongs.Where(n => n.MaDP.Equals(id)).FirstOrDefault();
                if (dp == null) return false;
                else
                {
                    db.tDatPhongs.DeleteOnSubmit(dp);
                    db.SubmitChanges();
                    return true;
                }    
            }
            catch
            {
                return false;
            }
        }
        //Sửa đơn

        [HttpPut]
        [Route("order/update")]
        public bool UpdateOrder(string id, string cusid, string address, string companyName, string code, string companyAddress)
        {
            try
            {
                tDatPhong dp = db.tDatPhongs.Where(n => n.MaDP.Equals(id)).FirstOrDefault();
                if (dp == null) return false;
                else
                {
                    dp.MaDP = id;
                    dp.MaKH = cusid;
                    dp.Diachi = address;
                    dp.TenCongTy = companyName;
                    dp.MaSoThue = code;
                    dp.DiaChiCty = companyAddress;
                    db.SubmitChanges();
                    return true;
                }    
               
            }
            catch
            {
                return false;
            }
        }
        //Xem chi tiết 1 đơn
        [HttpGet]
        [Route("order/detail")]
        public List<tChiTietDatPhong> DetailOrder(string id)
        {
            return db.tChiTietDatPhongs.Where(n => n.MaDP.Equals(id)).ToList();
        }
        //thêm chi tiết
        [HttpPost]
        [Route("order/detail/add")]
        public bool AddDetailOrder(string id,string type,byte amount)
        {
            try
            {
                tChiTietDatPhong ct = new tChiTietDatPhong();
                ct.MaDP = id;
                ct.LoaiPhong = type;
                ct.SLPhongDat = amount;
                db.tChiTietDatPhongs.InsertOnSubmit(ct);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //Xóa chi tiết
        [HttpDelete]
        [Route("order/detail/delete")]
        public bool DeleteDetailOrder(string id,string type,byte amount)
        {
            try
            {
                tChiTietDatPhong ct = db.tChiTietDatPhongs.Where(n => n.MaDP.Equals(id) && n.LoaiPhong.Equals(type)&& n.SLPhongDat==amount).FirstOrDefault();
                if (ct == null) return false;
                else
                {
                    db.tChiTietDatPhongs.DeleteOnSubmit(ct);
                    db.SubmitChanges();
                    return true;
                }    
            }
            catch
            {
                return false;
            }
        }
        //Sửa chi tiết
        [HttpPut]
        [Route("order/detail/update")]
        public bool UpdateDetailOrder(string id, string type, byte amount)
        {
            try
            {
                tChiTietDatPhong ct = db.tChiTietDatPhongs.Where(n => n.MaDP.Equals(id) && n.LoaiPhong.Equals(type) && n.SLPhongDat == amount).FirstOrDefault();
                if (ct == null) return false;
                else
                {
                    ct.MaDP = id;
                    ct.LoaiPhong = type;
                    ct.SLPhongDat = amount;
                    db.SubmitChanges();
                    return true;
                }    
        
            }
            catch
            {
                return false;
            }
        }
        

        //API Đăng kí phòng (đã ở )
        //Danh sách các đăng kí
        [HttpGet]
        [Route("booking")]
        public List<tDangKy> GetALlBooking()
        {
            return db.tDangKies.ToList();
        }
        //Tìm kiếm theo mã đki
        [HttpGet]
        [Route("booking")]
        public List<tDangKy> GetBookingById(string id)
        {
            return db.tDangKies.Where(n => n.MaDK.Contains(id)).ToList();
        }
        //Tìm kiếm theo tên khách
        [HttpGet]
        [Route("booking")]
        public List<tDangKy> GetBookingByCustomer(string name)
        {
            tChiTietKH sp = db.tChiTietKHs.Where(n=>n.TenKH.Equals(name)).FirstOrDefault();
                        
            var query = from ds in db.tDangKies
                        where ds.MaKH.Equals(sp.MaKH)
                        select ds;
            var data = query.ToList();
            return data;
        }
        //thêm đăng kí
        [HttpPost]
        [Route("booking/add")]
        public bool AddBooking(string id,string cusid,string roomName,DateTime cin,DateTime cout)
        {
            try
            {
                tDangKy dk = new tDangKy();
                dk.MaDK = id;
                dk.MaKH = cusid;
                dk.SoPhong = roomName;
                dk.NgayVao = cin;
                dk.NgayRa = cout;
                db.tDangKies.InsertOnSubmit(dk);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Sửa đăng kí
        [HttpPut]
        [Route("booking/update")]
        public bool UpdateBooking(string id, string cusid, string roomName, DateTime cin, DateTime cout)
        {
            //try
            //{
                tDangKy dk = db.tDangKies.Where(n => n.MaDK.Equals(id)).FirstOrDefault();
                if (dk == null) return false;
                else
                {
                    dk.MaDK = id;
                    dk.MaKH = cusid;
                    dk.SoPhong = roomName;
                    dk.NgayVao = cin;
                    dk.NgayRa = cout;
                    db.SubmitChanges();
                    return true;
                }    
                
            //}
            //catch
            //{
            //    return false;
            //}
        }
        //xóa đăng kí
        [HttpDelete]
        [Route("booking/delete")]
        public bool DeleteBooking(string id)
        {
            try
            {
                tDangKy dk = db.tDangKies.Where(n => n.MaDK.Equals(id)).FirstOrDefault();
                if (dk == null) return false;
                else
                {
                    db.tDangKies.DeleteOnSubmit(dk);
                    db.SubmitChanges();
                    return true;
                }

            }
            catch
            {
                return false;
            }
        }


        //Thống kê doanh thu theo các đơn đăng kí
        //cần viết trigger tự động cập nhật doanh thu khi có các booking
        [HttpGet]
        [Route("report")]
        public List<tDoanhThu> GetReport()
        {
                return db.tDoanhThus.ToList();
        }
        //Thống kê doanh thu theo loại phòng
        public List<tDoanhThu> GetReportByRoomType(string type)
        {
            return db.tDoanhThus.Where(n => n.LoaiPhong.Equals(type)).ToList();
        }
    }
}
