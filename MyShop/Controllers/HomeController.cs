using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.ViewModels;

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

        public ActionResult VisitSite()
        {
            DateTime today = DateTime.Now.Date;
            DateTime yesterday = today.AddDays(-1);
            VisitViewModel visitSite = new VisitViewModel();
            visitSite.VisitSum = _dbEntities.SiteVisit.Count();
            visitSite.VisitToday = _dbEntities.SiteVisit.Count(v => v.DateTime == today);
            visitSite.VisitYesterday=_dbEntities.SiteVisit.Count(v=>v.DateTime == yesterday);
            visitSite.Online= int.Parse(System.Web.HttpContext.Current.Application["Online"].ToString());
            return PartialView(visitSite);
        }
        public ActionResult AboutUs()
        {
            return View();
        }
    }
}