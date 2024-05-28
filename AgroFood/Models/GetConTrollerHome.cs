using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroFood.Models
{
    public class GetConTrollerHome
    {
        public List<string> SearchList { get; set; }
        public List<SP_LH_MH_NCC> DanhSachSanPham { get; set; }



        public GetConTrollerHome() { }

        public GetConTrollerHome(List<string> searchList, List<SP_LH_MH_NCC> danhSachSanPham)
        {
            SearchList = searchList;
            DanhSachSanPham = danhSachSanPham;
        }
    }
}