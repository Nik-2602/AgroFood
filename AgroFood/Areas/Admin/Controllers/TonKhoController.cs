using AgroFood.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroFood.Areas.Admin.Controllers
{
    public class TonKhoController : Controller
    {
        // GET: Admin/TonKho
        public ActionResult Index()
        {
            AgrofoodEntities2 db = new AgrofoodEntities2();
            var query1 = db.Database.SqlQuery<ReportTonKho>(
               "select * " +
                "from BaoCaoTonKho ");
            List<ReportTonKho> list = query1.ToList();
            Session["FinalDataReport"] = list;
            return View(list);
        }


        [HttpPost]
        public ActionResult Index(string searchMaSP, string searchTenSP)
        {
            if (!searchMaSP.Equals(""))
            {
                AgrofoodEntities2 db = new AgrofoodEntities2();
                var query1 = db.Database.SqlQuery<ReportTonKho>(
                   "select * " +
                    "from BaoCaoTonKho " +
                    "Where MaSP = '" + searchMaSP + "'");
                List<ReportTonKho> list = query1.ToList();
                Session["FinalDataReport"] = list;
                return View(list);
            }
            if (!searchTenSP.Equals(""))
            {
                AgrofoodEntities2 db = new AgrofoodEntities2();
                var query1 = db.Database.SqlQuery<ReportTonKho>(
                   "select * " +
                    "from BaoCaoTonKho " +
                    "Where TenSP LIKE N'%" + searchTenSP + "%'");
                List<ReportTonKho> list = query1.ToList();
                Session["FinalDataReport"] = list;
                return View(list);
            }
            return View();
        }


        public ActionResult Reports(string ReportType)
        {
            AgrofoodEntities2 db = new AgrofoodEntities2();
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Areas/Admin/Report/ReportTonKho.rdlc");
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "TonKhoDataSet";
            List<ReportTonKho> listFinalReport = Session["FinalDataReport"] as List<ReportTonKho>;
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
            Response.AddHeader("content-disposition", "attachment;filename=tonkho_report." + fileNameExtension);
            return File(renderedByte, fileNameExtension);
        }

    }
}