using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace MyShop.Controllers
{
    public class SearchController : Controller
    {
        MyEShop_DBEntities _dbEntities = new MyEShop_DBEntities();
        // GET: Search
        public ActionResult Index(string q)
        {
            List<Products> products = new List<Products>();
            products.AddRange(_dbEntities.Product_Tag.Where(t => t.Tag == q).Select(p => p.Products).ToList());
            products.AddRange(_dbEntities.Products.Where(p => p.Title.Contains(q) || p.Text.Contains(q)||p.ShortDescription.Contains(q)).ToList());
            ViewBag.Search = q;
            return View(products.Distinct());
        }
    }
}