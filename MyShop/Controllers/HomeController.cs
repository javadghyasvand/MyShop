using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace MyShop.Controllers
{
    public class HomeController : Controller
    {
        MyEShop_DBEntities _dbEntities = new MyEShop_DBEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Slider()
        {
            DateTime dt = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,0,0,0);
           var slider= _dbEntities.Slider.Where(s => s.IsActive&&s.StartDate<=dt&&s.EndDate>=dt);
            return PartialView(slider);
        }

        public ActionResult AboutUs()
        {
            return View();
        }
    }
}