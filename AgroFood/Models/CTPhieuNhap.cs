//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AgroFood.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CTPhieuNhap
    {
        public string MaN { get; set; }
        public string MaSP { get; set; }
        public int SoLuong { get; set; }
        public int GiaNhap { get; set; }
        public int ThanhTien { get; set; }
        public string GhiChu { get; set; }
    
        public virtual PhieuNhap PhieuNhap { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
