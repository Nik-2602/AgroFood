using AgroFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroFood.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(KhachHang model)
        {
            AgrofoodEntities2 db = new AgrofoodEntities2();
            var taiKhoanUser = model.TaiKhoan;
            var matKhauUser = model.MatKhau;
            var adminCheck  = db.NhanViens.SingleOrDefault(x => x.TaiKhoan.Equals(taiKhoanUser) && x.MatKhau.Equals(matKhauUser));
            if (adminCheck != null) {
                Session["Admin"] = adminCheck;

                return RedirectToAction("Index","HomeAdmin", new { area = "Admin" });
            } else
            {
                var userCheck = db.KhachHangs.SingleOrDefault(x => x.TaiKhoan.Equals(taiKhoanUser) && x.MatKhau.Equals(matKhauUser));
                if (userCheck != null)
                {

                    Session["User"] = userCheck;

                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ModelState.AddModelError("", "Tài Khoản hoặc mật khẩu không chính xác");
                    RedirectToAction("Index");
                }
            }

            /*            var userCheck = db.KhachHangs.SingleOrDefault(x=>x.TaiKhoan.Equals(taiKhoanUser) && x.MatKhau.Equals(matKhauUser));
                        if (userCheck != null)
                        {

                            Session["User"] = userCheck;
                            return RedirectToAction("Index","Home/Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Tài Khoản hoặc mật khẩu không chính xác");
                            RedirectToAction("Index");
                        }*/
            return View();
        }
    }
}