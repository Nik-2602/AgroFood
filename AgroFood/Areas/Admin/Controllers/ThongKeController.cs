using AgroFood.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroFood.Areas.Admin.Controllers
{
    public class ThongKeController : Controller
    {
        // GET: Admin/ThongKe
        public ActionResult Index()
        {
            AgrofoodEntities2 db = new AgrofoodEntities2();
            var query1 = db.Database.SqlQuery<HoaDon>(
               "select * " +
                "from HoaDon ");
            List<HoaDon> list = query1.ToList();
            Session["FinalDataReport"] = list;
            return View(list);
        }

        [HttpPost]
        public ActionResult Index(DateTime NgayBD,DateTime NgayKT, string searchMaKH,string searchMaHD)
        {
            /*AgrofoodEntities2 db = new AgrofoodEntities2();
            var query1 = db.Database.SqlQuery<HoaDon>(
               "select * " +
                "from HoaDon ");
            List<HoaDon> list = query1.ToList();*/

            DateTime ngayNull = new DateTime(1991,1,1);
            if (NgayBD != ngayNull && NgayKT != ngayNull)
            {
                AgrofoodEntities2 db = new AgrofoodEntities2();
                var query1 = db.Database.SqlQuery<HoaDon>(
                   "select * " +
                    "from HoaDon " +
                    "Where NgayBan between '" + NgayBD + "' AND '" + NgayKT + "'");
                List<HoaDon> list = query1.ToList();
                Session["FinalDataReport"] = list;
                return View(list);
            }

            if (!searchMaKH.Equals(""))
            {
                AgrofoodEntities2 db = new AgrofoodEntities2();
                var query1 = db.Database.SqlQuery<HoaDon>(
                   "select * " +
                    "from HoaDon " +
                    "Where MaKH = '" + searchMaKH + "'");
                List<HoaDon> list = query1.ToList();
                Session["FinalDataReport"] = list;
                return View(list);
            }

            if (!searchMaHD.Equals(""))
            {
                AgrofoodEntities2 db = new AgrofoodEntities2();
                var query1 = db.Database.SqlQuery<HoaDon>(
                   "select * " +
                    "from HoaDon " +
                    "Where MaHD = '" + searchMaHD + "'");
                List<HoaDon> list = query1.ToList();
                Session["FinalDataReport"] = list;
                return View(list);
            }

            return View();
        }

        public ActionResult Reports(string ReportType)
        {
            AgrofoodEntities2 db = new AgrofoodEntities2();
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Areas/Admin/Report/ReportDoanhThu.rdlc");
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DoanhThuDataSet";
            /*reportDataSource.Value = db.HoaDons.ToList();*/
            List<HoaDon> listFinalReport = Session["FinalDataReport"] as List<HoaDon>;
            reportDataSource.Value = listFinalReport.ToList();
            localReport.DataSources.Add(reportDataSource);
            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;
            if (reportType == "Excel")
            {
                mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                encoding = "UTF-8";
                fileNameExtension = "xlsx";
            }
            if (reportType == "Word")
            {
                mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                encoding = "UTF-8";
                fileNameExtension = "docx";
            }
            if (reportType == "PDF")
            {
                mimeType = "application/pdf";
                encoding = "UTF-8";
                fileNameExtension = "pdf";
            }

            else
            {
                    mimeType = "image/jpg";
                    encoding = "UTF-8";
                    fileNameExtension = "jpg";
            }

            string[] streams;
            Warning[] warnings;
            byte[] renderedByte;
            renderedByte = localReport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment;filename=doanhthu_report." + fileNameExtension);
            return File(renderedByte, fileNameExtension);
        }

    }
}