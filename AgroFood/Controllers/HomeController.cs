using AgroFood.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace AgroFood.Controllers
{
    public class HomeController : Controller
    {

        public List<SP_MH_LH_DDH_CTDDH> LayGioHang()
        {
            List<SP_MH_LH_DDH_CTDDH> lstGioHang = Session["getListBuyKH"] as List<SP_MH_LH_DDH_CTDDH>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<SP_MH_LH_DDH_CTDDH> ();
                Session["getListBuyKH"] = lstGioHang;
            }
            return lstGioHang;
        }


        public ActionResult Index()
        {
            List<string> listSearch = Session["SearchList"] as List<string>;
            if (listSearch == null)
            {
                listSearch = new List<string>();
            }

            AgrofoodEntities2 db = new AgrofoodEntities2();
            var query = db.Database.SqlQuery<SP_LH_MH_NCC>(
                "SELECT lh.TenLH, sp.TenSP, sp.GiaBanSP, sp.HinhAnh " +
                "FROM SanPham sp " +
                "JOIN MatHang mh ON sp.MaMH = mh.MaMH " +
                "JOIN LoaiHang lh ON lh.MaLH = mh.MaLH " +
                "join NhaCungCap ncc on ncc.MaNCC = sp.MaNCC");

            List<SP_LH_MH_NCC> danhSachSP = query.ToList();


            GetConTrollerHome viewModelAdd = new GetConTrollerHome(listSearch, danhSachSP);
            ViewBag.getControllerHome = viewModelAdd;

            if (Session["User"] != null)
            {
                var user = Session["User"] as AgroFood.Models.KhachHang;
                var query1 = db.Database.SqlQuery<DonDatHang>(
                "select *" +
                "from DonDatHang " +
                "Where TinhTrang = N'Đang mua hàng' and MaKH = '" + user.MaKH + "'");
                List<DonDatHang> listDDH = query1.ToList();
                List<SP_MH_LH_DDH_CTDDH> listBuyKH = Session["getListBuyKH"] as List<SP_MH_LH_DDH_CTDDH>;
                /*                List<SP_MH_LH_DDH_CTDDH> listBuyKH = query1.ToList();*/
                if (listDDH.Count == 0)
                {
                    List<DonDatHang> dsDDH = db.DonDatHangs.ToList();
                    DonDatHang donDatHangLast = dsDDH.Last();
                    string maDDH = "";
                    string splitMaDDH = "";
                    long newMaDDHInt = 0;
                    string newMaDDHString = "";
                    maDDH = donDatHangLast.MaDDH;
                    splitMaDDH = maDDH.Substring(3);
                    if (Int64.TryParse(splitMaDDH, out newMaDDHInt))
                    {
                        newMaDDHInt++; // Thực hiện tăng giá trị
                    }
                    if (newMaDDHInt < 10)
                    {
                        newMaDDHString = "DDH0" + Convert.ToString(newMaDDHInt);
                    }
                    else if (newMaDDHInt > 10)
                    {
                        newMaDDHString = "DDH" + Convert.ToString(newMaDDHInt);
                    }
                    string maKH = user.MaKH;
                    DateTime ngayDH = DateTime.Now;
                    DateTime dateNgayDH = DateTime.Parse(ngayDH.ToString("yyyy-MM-dd"));
                    DateTime ngayGH = ngayDH.AddDays(3);
                    DateTime dateNgayGH = DateTime.Parse(ngayGH.ToString("yyyy-MM-dd"));
                    string tenNN = user.HoTen;
                    string DiaChiNhan = user.DiaChi;
                    string SDTN = user.SoDT;
                    string HTThanhToan = "Thanh toán bằng tiền mặt";
                    string HTGiaoHang = "Giao hàng tận nơi";
                    int triGia = 0;
                    string tinhTrang = "Đang mua hàng";
                    DonDatHang newDDH = new DonDatHang();
                    newDDH.MaDDH = newMaDDHString;
                    newDDH.MaKH = maKH;
                    newDDH.NgayDH = dateNgayDH;
                    newDDH.NgayGH = dateNgayGH;
                    newDDH.TenNguoiNhan = tenNN;
                    newDDH.DiaChiNhan = DiaChiNhan;
                    newDDH.SDTNhanHang = SDTN;
                    newDDH.HTThanhToan = HTThanhToan;
                    newDDH.HTGiaoHang = HTGiaoHang;
                    newDDH.TriGia = triGia;
                    newDDH.TinhTrang = tinhTrang;
                    db.DonDatHangs.Add(newDDH);
                    db.SaveChanges();
                    var query2 = db.Database.SqlQuery<SP_MH_LH_DDH_CTDDH>(
                   "SELECT *" +
                   "from KhachHang kh join DonDatHang ddh on kh.MaKH = ddh.MaKH join " +
                   " CTDonDatHang ctddh on ctddh.MaDDH = ddh.MaDDH join SanPham sp on sp.MaSP = ctddh.MaSP join MatHang mh on mh.MaMH = sp.MaMH  " +
                   "join LoaiHang lh on lh.MaLH = mh.MaLH  " +
                   "Where ddh.TinhTrang = N'Đang mua hàng' and kh.MaKH = '" + user.MaKH + "'");
                    listBuyKH = query2.ToList();
                    Session["getListBuyDDH"] = listDDH;
                    Session["getListBuyKH"] = listBuyKH;

                }else
                {
                    var query2 = db.Database.SqlQuery<SP_MH_LH_DDH_CTDDH>(
                                   "SELECT *" +
                                   "from KhachHang kh join DonDatHang ddh on kh.MaKH = ddh.MaKH join " +
                                   " CTDonDatHang ctddh on ctddh.MaDDH = ddh.MaDDH join SanPham sp on sp.MaSP = ctddh.MaSP join MatHang mh on mh.MaMH = sp.MaMH  " +
                                   "join LoaiHang lh on lh.MaLH = mh.MaLH  " +
                                   "Where ddh.TinhTrang = N'Đang mua hàng' and kh.MaKH = '" + user.MaKH + "'");
                    listBuyKH = query2.ToList();
                    Session["getListBuyKH"] = listBuyKH;
                    Session["getListBuyDDH"] = listDDH;
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult SearchAction(String search)
        {
            List<string> listSearch = Session["SearchList"] as List<string>;
            if (listSearch == null)
            {
                listSearch = new List<string>();
            }

            AgrofoodEntities2 db = new AgrofoodEntities2();
            var query = db.Database.SqlQuery<SP_LH_MH_NCC>(
                "SELECT lh.TenLH, sp.TenSP, sp.GiaBanSP, sp.HinhAnh " +
                "FROM SanPham sp " +
                "JOIN MatHang mh ON sp.MaMH = mh.MaMH " +
                "JOIN LoaiHang lh ON lh.MaLH = mh.MaLH " +
                "join NhaCungCap ncc on ncc.MaNCC = sp.MaNCC " +
                "Where sp.TenSP LIKE N'%" + search + "%'");

            List<SP_LH_MH_NCC> danhSachSP = query.ToList();
            GetConTrollerHome viewModelAdd = new GetConTrollerHome(listSearch, danhSachSP);
            listSearch.Add(search);
            Session["SearchList"] = listSearch;
            // Thêm kết quả tìm kiếm mới vào danh sách
            ViewBag.getControllerHome = viewModelAdd;

            return View();
        }

        /*[HttpPost]
        public ActionResult CategoryList(string search)
        {
            AgrofoodEntities2 db = new AgrofoodEntities2();
            var query = db.Database.SqlQuery<SP_LH_MH_NCC>(
                "SELECT lh.TenLH, sp.TenSP, sp.GiaBanSP, sp.HinhAnh " +
                "FROM SanPham sp " +
                "JOIN MatHang mh ON sp.MaMH = mh.MaMH " +
                "JOIN LoaiHang lh ON lh.MaLH = mh.MaLH " +
                "join NhaCungCap ncc on ncc.MaNCC = sp.MaNCC " +
                "Where mh.TenMH = N'" + search + "'");

            List<SP_LH_MH_NCC> danhSachSP = query.ToList();
            GetConTrollerHome viewModelAdd = new GetConTrollerHome(null, danhSachSP);
            // Thêm kết quả tìm kiếm mới vào danh sách
            ViewBag.getControllerHome = viewModelAdd;
            return View();
        }*/


        public ActionResult UserInfor()
        {
            var user = Session["User"] as AgroFood.Models.KhachHang;

            return View(user);
        }

        [HttpPost]
        public ActionResult UserInfor(KhachHang model, HttpPostedFileBase HinhAnhKH)
        {
            var user = Session["User"] as AgroFood.Models.KhachHang;
            AgrofoodEntities2 db = new AgrofoodEntities2();
            var updateModel = db.KhachHangs.Find(user.MaKH);

            updateModel.Email = model.Email;
            updateModel.SoDT = model.SoDT;
            updateModel.GioiTinh = model.GioiTinh;
            updateModel.NgaySinh = model.NgaySinh;
            updateModel.DiaChi = model.DiaChi;

            /*if (model.HinhAnh != null)
            {
                string rootFolder = Server.MapPath("/assets/img/img_user/");
                string fileName = Path.GetFileName(fileAnh.FileName);
                string pathEmail = Path.Combine(rootFolder, fileName);
                model.HinhAnh.SaveAs(pathEmail);
                updateModel.HinhAnh = "/assets/img/img_user/" + fileName;
            }*/

            if (HinhAnhKH != null)
            {
                if (HinhAnhKH.ContentLength > 0)
                {
                    string rootFolder = Server.MapPath("/assets/img/img_user/");
                    string pathEmail = rootFolder + HinhAnhKH.FileName;
                    HinhAnhKH.SaveAs(pathEmail);
                    //Luu thuoc tinh url hinhanh
                    model.HinhAnhKH = "/assets/img/img_user/" + HinhAnhKH.FileName;
                }
            }
            updateModel.HinhAnhKH =model.HinhAnhKH;

            /*            string rootFolder = Server.MapPath("/assets/img/img_user/");
                        string pathEmail = rootFolder + model.HinhAnh;
                        model.HinhAnh.SaveAs(pathEmail);
                        model.HinhAnh = "/assets/img/img_user/" + fileAnh.FileName;
                        updateModel.HinhAnh = model.HinhAnh;*/
            db.SaveChanges();
            Session["User"] = updateModel;
            return RedirectToAction("UserInfor", new { success = 1 });

        }

        public ActionResult PurchaseOrder(string id)
        {
            AgrofoodEntities2 db = new AgrofoodEntities2();
            var query = db.Database.SqlQuery<SP_MH_LH_DDH_CTDDH>(" SELECT *" +
                " from KhachHang kh join DonDatHang ddh on kh.MaKH = ddh.MaKH join" +
                "  CTDonDatHang ctddh on ctddh.MaDDH = ddh.MaDDH join SanPham sp on sp.MaSP = ctddh.MaSP join MatHang mh on mh.MaMH = sp.MaMH" +
                " join LoaiHang lh on lh.MaLH = mh.MaLH  " +
                "Where ddh.TinhTrang = N'Đã đặt hàng' and kh.MaKH = '"+id+"'");
            List<SP_MH_LH_DDH_CTDDH> dsDangMuaHang = query.ToList();
            ViewBag.DsMuaHang = dsDangMuaHang;

            var query1 = db.Database.SqlQuery<SP_MH_LH_DDH_CTDDH>("select * " +
                "from DonDatHang " +
                "where TinhTrang = N'Đã đặt hàng' and MaKH = '" + id+"'");
            List<SP_MH_LH_DDH_CTDDH> dsDDHDangMuaHang = query1.ToList();
            ViewBag.DsDDHMuaHang = dsDDHDangMuaHang;




            var query2 = db.Database.SqlQuery<SP_MH_LH_DDH_CTDDH>(" SELECT *" +
                " from KhachHang kh join DonDatHang ddh on kh.MaKH = ddh.MaKH join" +
                "  CTDonDatHang ctddh on ctddh.MaDDH = ddh.MaDDH join SanPham sp on sp.MaSP = ctddh.MaSP join MatHang mh on mh.MaMH = sp.MaMH" +
                " join LoaiHang lh on lh.MaLH = mh.MaLH  " +
                "Where ddh.TinhTrang = N'Đang giao hàng' and kh.MaKH = '" + id + "'");
            List<SP_MH_LH_DDH_CTDDH> dsGiaoHang = query2.ToList();
            ViewBag.DsGiaoHang = dsGiaoHang;

            var query3 = db.Database.SqlQuery<SP_MH_LH_DDH_CTDDH>("select * " +
                "from DonDatHang " +
                "where TinhTrang = N'Đang giao hàng' and MaKH = '" + id + "'");
            List<SP_MH_LH_DDH_CTDDH> dsDDHGiaoHang = query3.ToList();
            ViewBag.DsDDHGiaoHang = dsDDHGiaoHang;

            var query4 = db.Database.SqlQuery<SP_MH_LH_DDH_CTDDH>(" SELECT *" +
                " from KhachHang kh join DonDatHang ddh on kh.MaKH = ddh.MaKH join" +
                "  CTDonDatHang ctddh on ctddh.MaDDH = ddh.MaDDH join SanPham sp on sp.MaSP = ctddh.MaSP join MatHang mh on mh.MaMH = sp.MaMH" +
                " join LoaiHang lh on lh.MaLH = mh.MaLH  " +
                "Where ddh.TinhTrang = N'Hoàn thành' and kh.MaKH = '" + id + "'");
            List<SP_MH_LH_DDH_CTDDH> dsHoanThanh = query4.ToList();
            ViewBag.DsHoanThanh = dsHoanThanh;


            var query5 = db.Database.SqlQuery<SP_MH_LH_DDH_CTDDH>("select * " +
                "from DonDatHang " +
                "where TinhTrang = N'Hoàn thành' and MaKH = '" + id + "'");
            List<SP_MH_LH_DDH_CTDDH> dsDDHHoanThanh = query5.ToList();

           

            ViewBag.DsDDHHoanThanh = dsDDHHoanThanh;

            return View();
        }

        public ActionResult NhanHang(string id)
        {
            AgrofoodEntities2 db = new AgrofoodEntities2();
            var updateModel = db.DonDatHangs.Find(id);
            updateModel.TinhTrang = "Hoàn thành";
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult RankUser()
        {

            var user = Session["User"] as AgroFood.Models.KhachHang;
            return View();
        }

        public ActionResult WalletUser()
        {
            var user = Session["User"] as AgroFood.Models.KhachHang;
            return View();
        }

        public ActionResult SupportUser()
        {
            return View();
        }

        public ActionResult Policy()
        {
            return View();
        }

        public ActionResult DangXuat()
        {
            var user = Session["User"] as AgroFood.Models.KhachHang;
            if (user != null)
            {
                Session.Remove("User");
            }
            return RedirectToAction("Index");
        }

        public ActionResult DetailProduct(string TenSP ="")
        {
            AgrofoodEntities2 db = new AgrofoodEntities2();
            var query = db.Database.SqlQuery<SP_LH_MH_NCC>(
                "SELECT * " +
                "FROM SanPham sp " +
                "JOIN MatHang mh ON sp.MaMH = mh.MaMH " +
                "JOIN LoaiHang lh ON lh.MaLH = mh.MaLH " +
                "join NhaCungCap ncc on ncc.MaNCC = sp.MaNCC " +
                "WHERE sp.TenSP = N'" +TenSP +"'");
            SP_LH_MH_NCC sp = query.FirstOrDefault();

            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            var query1 = db.Database.SqlQuery<SP_LH_MH_NCC>(
                "SELECT * " +
                "FROM SanPham sp " +
                "JOIN MatHang mh ON sp.MaMH = mh.MaMH " +
                "JOIN LoaiHang lh ON lh.MaLH = mh.MaLH " +
                "join NhaCungCap ncc on ncc.MaNCC = sp.MaNCC " +
                "WHERE lh.TenLH = N'" + sp.TenLH + "'");
            List<SP_LH_MH_NCC> ListSPTuongTu = query1.ToList();

            ViewBag.listProductTT = ListSPTuongTu;


            if (Session["User"] != null)
            {
                var user = Session["User"] as AgroFood.Models.KhachHang;
                var query2 = db.Database.SqlQuery<DonDatHang>(
                "select *" +
                "from DonDatHang " +
                "Where TinhTrang = N'Đang mua hàng' and MaKH = '" + user.MaKH + "'");
                List<DonDatHang> listDDH = query2.ToList();
                Session["getListBuyDDH"] = listDDH;
               
            }

            return View(sp);
        }

        [HttpPost]
        public ActionResult ThemSanPham(SP_MH_LH_DDH_CTDDH SanPham)
        {
            List<SP_MH_LH_DDH_CTDDH> listBuyKH = Session["getListBuyKH"] as List<SP_MH_LH_DDH_CTDDH>;
            // Xử lý thêm sản phẩm vào cơ sở dữ liệu ở đây
            if (SanPham != null)
            {
                AgrofoodEntities2 db = new AgrofoodEntities2();
                CTDonDatHang ctDonHang = new CTDonDatHang();
                ctDonHang.MaDDH = SanPham.MaDDH;
                ctDonHang.MaSP = SanPham.MaSP;
                ctDonHang.SoLuong = SanPham.SoLuong;
                ctDonHang.GiaBan = SanPham.GiaBanSP;
                ctDonHang.TongTien = SanPham.TongTien;
                db.CTDonDatHangs.Add(ctDonHang);
                db.SaveChanges();
                if (Session["User"] != null)
                {
                    var user = Session["User"] as AgroFood.Models.KhachHang;
                    var query1 = db.Database.SqlQuery<SP_MH_LH_DDH_CTDDH>(
                    "SELECT *" +
                    "from KhachHang kh join DonDatHang ddh on kh.MaKH = ddh.MaKH join " +
                    " CTDonDatHang ctddh on ctddh.MaDDH = ddh.MaDDH join SanPham sp on sp.MaSP = ctddh.MaSP join MatHang mh on mh.MaMH = sp.MaMH  " +
                    "join LoaiHang lh on lh.MaLH = mh.MaLH  " +
                    "Where ddh.TinhTrang = N'Đang mua hàng' and kh.MaKH = '" + user.MaKH + "'");
                    listBuyKH = query1.ToList();
                    Session["getListBuyKH"] = listBuyKH;

                }

            }
            return PartialView("ThemSanPham");
        }

        [HttpPost]
        public ActionResult MuaSanPham(SP_MH_LH_DDH_CTDDH SanPham)
        {
            List<SP_MH_LH_DDH_CTDDH> listBuyKH = Session["getListBuyKH"] as List<SP_MH_LH_DDH_CTDDH>;
            // Xử lý thêm sản phẩm vào cơ sở dữ liệu ở đây
            if (SanPham != null)
            {
                AgrofoodEntities2 db = new AgrofoodEntities2();
                CTDonDatHang ctDonHang = new CTDonDatHang();
                ctDonHang.MaDDH = SanPham.MaDDH;
                ctDonHang.MaSP = SanPham.MaSP;
                ctDonHang.SoLuong = SanPham.SoLuong;
                ctDonHang.GiaBan = SanPham.GiaBanSP;
                ctDonHang.TongTien = SanPham.TongTien;
                db.CTDonDatHangs.Add(ctDonHang);
                db.SaveChanges();
                if (Session["User"] != null)
                {
                    var user = Session["User"] as AgroFood.Models.KhachHang;
                    var query1 = db.Database.SqlQuery<SP_MH_LH_DDH_CTDDH>(
                    "SELECT *" +
                    "from KhachHang kh join DonDatHang ddh on kh.MaKH = ddh.MaKH join " +
                    " CTDonDatHang ctddh on ctddh.MaDDH = ddh.MaDDH join SanPham sp on sp.MaSP = ctddh.MaSP join MatHang mh on mh.MaMH = sp.MaMH  " +
                    "join LoaiHang lh on lh.MaLH = mh.MaLH  " +
                    "Where ddh.TinhTrang = N'Đang mua hàng' and kh.MaKH = '" + user.MaKH + "'");
                    listBuyKH = query1.ToList();
                    Session["getListBuyKH"] = listBuyKH;

                }

            }
            return RedirectToAction("Index", "Cart");
        }


        [HttpPost]
        public ActionResult XoaSanPham(string maDDH = "",string maSP = "")
        {
            AgrofoodEntities2 db = new AgrofoodEntities2();
            List<SP_MH_LH_DDH_CTDDH> listBuyKH = Session["getListBuyKH"] as List<SP_MH_LH_DDH_CTDDH>;
            CTDonDatHang modelDel = db.CTDonDatHangs.FirstOrDefault(m=> m.MaDDH == maDDH && m.MaSP == maSP);
            db.CTDonDatHangs.Remove(modelDel);

            for(int i = 0; i < listBuyKH.Count; i++)
                {
                var item = listBuyKH[i];
                if (item.MaDDH == maDDH && item.MaSP == maSP)
                {
                    // Bước 2: Xóa sản phẩm khỏi danh sách
                    listBuyKH.Remove(item);
                }
            }

            // Sau khi xóa xong, cần cập nhật lại Session
            Session["getListBuyKh"] = listBuyKH;
            db.SaveChanges();
            
            return View();
        }





        /*public ActionResult SearchAction(String search)
        {
            listSearch = new List<String>();
            listSearch.Add(search);
            ViewBag.search = listSearch;
            return View();
        }*/


    }
}