using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebOnlineStore.Models;
using System.Data.SqlClient;
using PagedList;
using PagedList.Mvc;

namespace WebOnlineStore.Controllers
{
    
    public class HomeController : Controller
    {
        private MyDB db = new MyDB();
        public ActionResult Index(string keyword, int? page)
        {
            int pageNumber = (page ?? 1);
            int itemsPerPage = 12;

            var products = db.SANPHAMs.ToList();
            var pageSize = products.Count / itemsPerPage;
            // Tất cả sp 
            var spall = products
                         .Where(p => p.ID_SP != null )
                         .OrderBy(p => p.tensanpham)
                         .ToList();
            // sản phẩm bán chạy
            var sp = spall.Where(p => p.hot == 1);

            if (!string.IsNullOrEmpty(keyword) && !string.IsNullOrWhiteSpace(keyword))
            {
                sp = sp.Where(p => !string.IsNullOrEmpty(p.tensanpham) && p.tensanpham.ToLower().Contains(keyword.ToLower()))
                    .ToList();
            }
            return View(sp.OrderBy(n => n.ID_SP).ToPagedList(pageNumber, itemsPerPage));
        }
    }
}