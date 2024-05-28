using AgroFood.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace AgroFood.Controllers
{

    

    public class RegisterController : Controller
    {
        public bool IsValidEmail(string email)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }


        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        

        [HttpPost]
        public ActionResult Index(KhachHang model)
        {
            AgrofoodEntities2 db = new AgrofoodEntities2();
            List<KhachHang> danhSachKH = db.KhachHangs.ToList();
            KhachHang khachHangLast = danhSachKH.Last();
            String maKh = "";
            String splitMaKH = "";
            long newMaKHInt = 0;
            String newMaKHString = "";
            maKh = khachHangLast.MaKH;
            splitMaKH = maKh.Substring(2);
            if (Int64.TryParse(splitMaKH, out newMaKHInt))
            {
                newMaKHInt++; // Thực hiện tăng giá trị
            }
            if (newMaKHInt < 10)
            {
                newMaKHString = "KH00" + Convert.ToString(newMaKHInt);
            } else if (newMaKHInt > 10)
            {
                newMaKHString = "KH0" + Convert.ToString(newMaKHInt);
            }

            if (model.HoTen == null || model.HoTen.Equals(""))
            {
                ModelState.AddModelError("", "Vui lòng điền họ tên");
                return View(model);
            }

            if (model.DiaChi == null || model.DiaChi.Equals(""))
            {
                ModelState.AddModelError("", "Vui lòng điền địa chỉ");
                return View(model);
            }

            if (model.SoDT == null || model.SoDT.Equals(""))
            {
                ModelState.AddModelError("", "Vui lòng điền số điện thoại");
                return View(model);

            }

            if (model.Email == null || model.Email.Equals("") ) 
            {
                ModelState.AddModelError("", "Vui lòng nhập email");
                return View(model);
            }
            if (!IsValidEmail(model.Email))
            {
                ModelState.AddModelError("", "Vui lòng nhập email hợp lệ");
                return View(model);
            }

            if (model.TaiKhoan == null || model.TaiKhoan.Equals(""))
            {
                ModelState.AddModelError("", "Vui lòng điền tài khoản");
                return View(model);
            }

            if (model.MatKhau == null || model.MatKhau.Equals(""))
            {
                ModelState.AddModelError("", "Vui lòng điền mật khẩu");
                return View(model);
            }

            if (model.HinhAnhKH == null)
            {
                model.HinhAnhKH = "https://anhdep123.com/wp-content/uploads/2020/11/avatar-facebook-mac-dinh-nam.jpeg";
            }

            model.MaKH = newMaKHString;
            db.KhachHangs.Add(model);
            db.SaveChanges();

            ViewBag.successMS = 1;

            return RedirectToAction("Index", new { success = 1 });
        }
    }
}