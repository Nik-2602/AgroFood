using AgroFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using System.Reflection;


namespace AgroFood.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            List<SP_MH_LH_DDH_CTDDH> listBuyKH = Session["getListBuyKH"] as List<SP_MH_LH_DDH_CTDDH>;
            AgrofoodEntities2 db = new AgrofoodEntities2();
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
            return View(listBuyKH);
        }


        [HttpPost]
        public ActionResult CapNhatGioHang(List<CTDonDatHang> listCTDDH)
        {

            AgrofoodEntities2 db = new AgrofoodEntities2();
            List<SP_MH_LH_DDH_CTDDH> listBuyKH = Session["getListBuyKH"] as List<SP_MH_LH_DDH_CTDDH>;
            if (listCTDDH != null)
            {
                foreach (var ctdhData in listCTDDH)
                {
                    
                    // Lấy CTDH từ cơ sở dữ liệu
                    var ctdhUpdateModel = db.CTDonDatHangs.FirstOrDefault(x => x.MaDDH == ctdhData.MaDDH && x.MaSP == ctdhData.MaSP);
                    ctdhUpdateModel.MaDDH = ctdhData.MaDDH;
                    ctdhUpdateModel.MaSP = ctdhData.MaSP;
                    ctdhUpdateModel.SoLuong = ctdhData.SoLuong;
                    ctdhUpdateModel.GiaBan = ctdhData.GiaBan;
                    ctdhUpdateModel.TongTien = ctdhData.TongTien;
                    db.SaveChanges();


                }

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



            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult XoaSanPham(string maDDH = "", string maSP = "")
        {
            AgrofoodEntities2 db = new AgrofoodEntities2();
            List<SP_MH_LH_DDH_CTDDH> listBuyKH = Session["getListBuyKH"] as List<SP_MH_LH_DDH_CTDDH>;
            CTDonDatHang modelDel = db.CTDonDatHangs.FirstOrDefault(m => m.MaDDH == maDDH && m.MaSP == maSP);
            db.CTDonDatHangs.Remove(modelDel);

            for (int i = 0; i < listBuyKH.Count; i++)
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

            return RedirectToAction("Index");
        }



        [HttpPost]
        public ActionResult ThemDDH(DonDatHang ddh)
        {
            AgrofoodEntities2 db = new AgrofoodEntities2();
            List<SP_MH_LH_DDH_CTDDH> listBuyKH = Session["getListBuyKH"] as List<SP_MH_LH_DDH_CTDDH>;
            DateTime today = DateTime.Today;
            DateTime futureDate = today.AddDays(3);
            if (ddh != null)
            {
                DonDatHang model = db.DonDatHangs.FirstOrDefault(m => m.MaDDH == ddh.MaDDH);
                model.MaDDH = ddh.MaDDH;
                model.MaKH = ddh.MaKH;
                model.NgayDH = today;
                model.NgayGH = futureDate;
                model.TenNguoiNhan = ddh.TenNguoiNhan;
                model.DiaChiNhan = ddh.DiaChiNhan;
                model.SDTNhanHang = ddh.SDTNhanHang;
                model.HTThanhToan = ddh.HTThanhToan;
                model.HTGiaoHang = ddh.HTGiaoHang;
                model.TriGia = ddh.TriGia;
                model.TinhTrang = ddh.TinhTrang;
                db.SaveChanges();
            }

            /*if (Session["User"] != null)
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

            }*/

            //send email
            var strSanPham = "";
            var emailKhachHang = "";
            DateTime ngayDH = new DateTime();
            foreach (var item in listBuyKH)
            {
                strSanPham += "<tr>";
                    strSanPham += "<td style=\"color:#636363;border:1px solid #e5e5e5;padding:12px;text-align:left;vertical-align:middle;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;word-wrap:break-word\">"+item.TenSP+"</td>";
                    strSanPham += "<td style=\"color:#636363;border:1px solid #e5e5e5;padding:12px;text-align:left;vertical-align:middle;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif\">"+item.SoLuong+"</td>";
                    strSanPham += "<td style=\"color:#636363;border:1px solid #e5e5e5;padding:12px;text-align:left;vertical-align:middle;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif\"><span>"+item.GiaBanSP +"&nbsp;<span>₫</span></span></td>";
                strSanPham += "</tr>";
                emailKhachHang = item.Email;
                ngayDH = today;
            }

            string formattedDate = ngayDH.ToString("dd-MM-yyyy");

            string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/assets/template/send2.html"));
            contentCustomer = contentCustomer.Replace("{{MaDon}}", ddh.MaDDH);
            contentCustomer = contentCustomer.Replace("{{NgayDat}}", formattedDate);
            contentCustomer = contentCustomer.Replace("{{SanPham}}", strSanPham);
            contentCustomer = contentCustomer.Replace("{{ThanhTien}}",ddh.TriGia.ToString());
            contentCustomer = contentCustomer.Replace("{{PhuongThucTT}}", ddh.HTThanhToan);
            contentCustomer = contentCustomer.Replace("{{TongCong}}", ddh.TriGia.ToString());
            contentCustomer = contentCustomer.Replace("{{TenKhachHang}}", ddh.TenNguoiNhan);
            contentCustomer = contentCustomer.Replace("{{DiaChi}}", ddh.DiaChiNhan);
            contentCustomer = contentCustomer.Replace("{{SoDT}}", ddh.SDTNhanHang);
            contentCustomer = contentCustomer.Replace("{{Email}}", emailKhachHang);

            Common.Common.SendMail("Agrofood", "Đơn hàng #" + ddh.MaDDH, contentCustomer.ToString(), emailKhachHang);




            listBuyKH.Clear();
            Session["getListBuyKH"] = listBuyKH;


            return RedirectToAction("Index");
        }
    }
}