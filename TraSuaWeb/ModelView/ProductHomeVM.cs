using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraSuaWeb.Models;

namespace TraSuaWeb.ModelView
{
    public class ProductHomeVM
    {
        public LoaiSp loaisps { get; set; }
        public List<SanPham> lssanpham { get; set; }
    }
}
