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
    
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            this.CTDonDatHangs = new HashSet<CTDonDatHang>();
            this.CTHoaDons = new HashSet<CTHoaDon>();
            this.CTPhieuNhaps = new HashSet<CTPhieuNhap>();
        }
    
        public string MaMH { get; set; }
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public int GiaBanSP { get; set; }
        public string MaNCC { get; set; }
        public string TrongLuong { get; set; }
        public string MoTaSP { get; set; }
        public int SoLuongT { get; set; }
        public string HinhAnh { get; set; }
        public string HinhAnh1 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTDonDatHang> CTDonDatHangs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHoaDon> CTHoaDons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTPhieuNhap> CTPhieuNhaps { get; set; }
        public virtual MatHang MatHang { get; set; }
        public virtual NhaCungCap NhaCungCap { get; set; }
    }
}