using AgroFood.Models;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace AgroFood.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            AgrofoodEntities2 db = new AgrofoodEntities2();
            List<KhachHang> danhSachKH = db.KhachHangs.ToList();
            List<NhanVien> danhSachNV = db.NhanViens.ToList();
            List<MatHang> danhMuc = db.MatHangs.ToList();
            List<SanPham> danhSachSP = db.SanPhams.ToList();
            List<NhaCungCap> danhSachNCC = db.NhaCungCaps.ToList();
            List<PhieuNhap> danhSachPN = db.PhieuNhaps.ToList();

            var query1 = db.Database.SqlQuery<DonDatHang>(
                "select * " +
                "from DonDatHang "+
                "where TinhTrang = N'Hoàn thành'");
            List<CTDonDatHang> danhSachCTDDH = db.CTDonDatHangs.ToList();
            List<DonDatHang> danhSachDDH = query1.ToList();
            var doanhThu = 0;
            var doanhSo = 0;
            foreach(var item in danhSachDDH)
            {
                doanhThu += item.TriGia;
            }

            foreach(var item in danhSachCTDDH)
            {
                doanhSo += item.SoLuong;
            }

            string Format(int so, string format)
            {
                var soCoDauPhay = so.ToString(format);

                return soCoDauPhay;
            }

            var query2 = db.Database.SqlQuery<string>(
                "select distinct MaKH " +
                "from DonDatHang " +
                "where TinhTrang = N'Hoàn thành'");

            var luopngMuaKH = query2.Count();
            var luongDM = danhMuc.Count;
            var luongNCC = danhSachNCC.Count;
            var luongSP = danhSachSP.Count;
            var luongPN = danhSachPN.Count;
            var luongDDH = danhSachDDH.Count;
            var luongNV = danhSachNV.Count;
            var luongKH = danhSachKH.Count;

            ViewBag.luongDM = luongDM;
            ViewBag.luongNCC = luongNCC;
            ViewBag.luongSP = luongSP;
            ViewBag.luongPN = luongPN;
            ViewBag.luongDDH = luongDDH;
            ViewBag.doanhThu = Format(doanhThu,"0,000");
            ViewBag.doanhSo = doanhSo;
            ViewBag.luongNV = luongNV;
            ViewBag.luongKH = luongKH;
            ViewBag.luongKHMuaHang = luopngMuaKH;

            return View();
        }


        public ActionResult DangXuat()
        {
            var admin = Session["Admin"] as AgroFood.Models.NhanVien;
            if (admin != null)
            {
                Session.Remove("Admin");
            }
            return RedirectToAction("Index","Home", new { area = "" });
        }




    }
}