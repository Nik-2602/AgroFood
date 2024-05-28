using AgroFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroFood.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // GET: Category
        [HttpPost]
        public ActionResult Index(string search)
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
                "Where mh.TenMH = N'" + search + "'");

            List<SP_LH_MH_NCC> danhSachSP = query.ToList();
            Session["getListSPSearch"] = danhSachSP;
            // Thêm kết quả tìm kiếm mới vào danh sách
            return View();
        }
    }
}