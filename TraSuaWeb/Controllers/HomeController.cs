using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TraSuaWeb.Models;
using TraSuaWeb.ModelView;

namespace TraSuaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DBtrasuaContext _context;

        public HomeController(ILogger<HomeController> logger, DBtrasuaContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM model = new HomeVM();
            var lssangpham = _context.SanPhams.AsNoTracking()
            .Where(x => x.TinhTrang == true && x.AnHien==true)
            .OrderByDescending(x => x.NgayTao)
            .ToList();

            List<ProductHomeVM> lssanphamView = new List<ProductHomeVM>();

            var lsloais = _context.LoaiSps.AsNoTracking().ToList();

            foreach(var item in lsloais)
            {

                ProductHomeVM producthome = new ProductHomeVM();
                producthome.loaisps= item;
                producthome.lssanpham = lssangpham.Where(x => x.MaLoai == item.MaLoai).ToList();
                lssanphamView.Add(producthome);
            }

            var tintuc = _context.Tintucs.AsNoTracking().Take(3).ToList();
            model.sanphams = lssanphamView;
            model.tintucs = tintuc;
            ViewBag.AllProduct = lssangpham; 
            return View(model);
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
