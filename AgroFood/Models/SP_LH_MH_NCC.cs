using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroFood.Models
{
    public class SP_LH_MH_NCC
    {
        public string MaMH { get; set; }
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public int GiaBanSP { get; set; }
        public string MaNCC { get; set; }
        public string TrongLuong { get; set; }
        public string MoTaSP { get; set; }
        public int SoLuongT {  get; set; }
        public string HinhAnh { get; set; }
        public string HinhAnh1 { get; set; }
        public string MaLH {  get; set; }
        public string TenMH { get; set; }
        public string TenLH { get; set; }
        public string TenNCC { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }

        public SP_LH_MH_NCC() { }

        public SP_LH_MH_NCC(string maMH, string maSP, string tenSP, int giaBan, string maNCC, string trongLuong, string moTaSP, int soLuongT, string hinhAnh, string hinhAnh1, string maLH, string tenMH, string tenLH, string tenNCC, string diaChi, string dienThoai)
        {
            MaMH = maMH;
            MaSP = maSP;
            TenSP = tenSP;
            GiaBanSP = giaBan;
            MaNCC = maNCC;
            TrongLuong = trongLuong;
            MoTaSP = moTaSP;
            SoLuongT = soLuongT;
            HinhAnh = hinhAnh;
            HinhAnh1 = hinhAnh1;
            MaLH = maLH;
            TenMH = tenMH;
            TenLH = tenLH;
            TenNCC = tenNCC;
            DiaChi = diaChi;
            DienThoai = dienThoai;
        }
    }
}