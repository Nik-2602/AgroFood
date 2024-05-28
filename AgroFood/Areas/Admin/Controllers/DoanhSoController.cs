using AgroFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroFood.Areas.Admin.Controllers
{
    public class DoanhSoController : Controller
    {
        // GET: Admin/DoanhSo
        public ActionResult Index()
        {
            AgrofoodEntities2 db = new AgrofoodEntities2();
            return View();
        }



    }
}