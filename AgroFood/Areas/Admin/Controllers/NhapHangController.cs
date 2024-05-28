using AgroFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroFood.Areas.Admin.Controllers
{
    public class NhapHangController : Controller
    {

        AgrofoodEntities2 db = new AgrofoodEntities2();
        // GET: Admin/NhapHang
        public ActionResult Index()
        {
            List<PhieuNhap> dsPN = db.PhieuNhaps.ToList();
            return View(dsPN);
        }


        public ActionResult XemPN(string id)
        {
            var query = db.Database.SqlQuery < CTPhieuNhap>("select * from CTPhieuNhap where MaN ='" + id + "'");
            List<CTPhieuNhap> dsNhapHang = query.ToList();

            return View(dsNhapHang);
        }
    }
}