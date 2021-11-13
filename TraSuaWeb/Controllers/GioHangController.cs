using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraSuaWeb.Extension;
using TraSuaWeb.Models;
using TraSuaWeb.ModelView;
namespace TraSuaWeb.Controllers
{
    public class GioHangController : Controller
    {
        private readonly DBtrasuaContext _context;
        public INotyfService _notifyService { get; }
        public GioHangController(DBtrasuaContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }


        public List<GiohangViewModel> GioHang
        {
            get
            {
                var gh = HttpContext.Session.Get<List<GiohangViewModel>>("GioHang");
                if (gh == default(List<GiohangViewModel>))
                {
                    gh = new List<GiohangViewModel>();
                }
                return gh;
            }
        }

        [HttpPost]
        [Route("api/cart/add")]
        public IActionResult AddToCart(int SanphamID, int? Soluong)
        {
            List<GiohangViewModel> giohang = GioHang;
            try
            {
                GiohangViewModel item = GioHang.SingleOrDefault(p => p.sanpham.MaSp == SanphamID);
                if (item != null)
                {
                    if (Soluong.HasValue)
                    {
                        item.Soluong = Soluong.Value;
                    }
                    else
                    {
                        item.Soluong++;
                    }
                }
                else
                {
                    SanPham sp = _context.SanPhams.SingleOrDefault(p => p.MaSp == SanphamID);
                    item = new GiohangViewModel
                    {
                        Soluong = Soluong.HasValue ? Soluong.Value : 1,
                        sanpham = sp
                    };
                    giohang.Add(item);
                }
                HttpContext.Session.Set<List<GiohangViewModel>>("GioHang", giohang);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
            // Thêm sản phẩm vào giỏ hàng

        }
        [HttpPost]
        [Route("api/cart/add")]
        public ActionResult Remove(int SanphamID)
        {
            try
            {
                List<GiohangViewModel> giohang = GioHang;
                GiohangViewModel item = giohang.SingleOrDefault(p => p.sanpham.MaSp == SanphamID);
                if (item != null)
                {
                    giohang.Remove(item);
                }
                // Lưu lại Session
                HttpContext.Session.Set<List<GiohangViewModel>>("GioHang", giohang);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }

        }
        [Route("gioahang.html", Name = "giohang")]
        public IActionResult Index()
        {
            var lsGioHang = GioHang;
            return View(GioHang);
        }
    }
}
