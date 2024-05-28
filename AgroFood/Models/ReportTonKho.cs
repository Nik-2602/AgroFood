using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroFood.Models
{
    public class ReportTonKho
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuongT { get; set; }
        public int SoLuongBan { get; set; }
        public int TonKho { get; set; }

        public ReportTonKho() { }

        public ReportTonKho(string maSP, string tenSP, int soLuongT, int soLuongBan, int tonKho)
        {
            MaSP = maSP;
            TenSP = tenSP;
            SoLuongT = soLuongT;
            SoLuongBan = soLuongBan;
            TonKho = tonKho;
        }

    }
}