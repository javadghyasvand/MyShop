using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DataLayer;
using DataLayer.ViewModels;

namespace MyShop.Areas.Admin.Controllers
{
    public class CompareController : Controller
    {
        MyEShop_DBEntities _dbEntities = new MyEShop_DBEntities();
        // GET: Admin/Compare
        public ActionResult Index()
        {
            List<CompareItem> compareItems = new List<CompareItem>();
            if (Session["Compare"] != null)
            {
                compareItems = Session["Compare"] as List<CompareItem>;
            }





            List<Features> featuresList = new List<Features>();
            List<Product_Featuers> productFeatuersList = new List<Product_Featuers>();

            foreach (var item in compareItems)
            {
                featuresList.AddRange(_dbEntities.Product_Featuers.Where(p => p.Product_Id == item.ProductId).Select(f => f.Features).ToList());
                productFeatuersList.AddRange(_dbEntities.Product_Featuers.Where(p => p.Product_Id == item.ProductId).ToList());
            }

            ViewBag.featuers = featuresList.Distinct().ToList();
            ViewBag.productFeatuersList = productFeatuersList.Distinct().ToList();
            return View(compareItems);
        }

        public ActionResult AddToCompare(int id)
        {
            List<CompareItem> compareItems = new List<CompareItem>();
            if (Session["Compare"]!= null)
            {
                compareItems = Session["Compare"]as List<CompareItem>;
            }

            if (!compareItems.Any(P => P.ProductId == id))
            {
                var product = _dbEntities.Products.Where(p => p.ProductId == id).Select(p=>new
                {
                    p.Title,
                    p.ImageName
                }).Single();
                compareItems.Add(new CompareItem()
                {
                    ProductId = id,
                    ImageName =product.ImageName,
                    ProductTitle = product.Title

                });
            }
            Session["Compare"]=compareItems;
            return PartialView("ListCompare",compareItems);
        }

        public ActionResult ListCompare()
        {
            List<CompareItem> compareItems = new List<CompareItem>();
            if (Session["Compare"] != null)
            {
                compareItems = Session["Compare"] as List<CompareItem>;
            }

            return PartialView(compareItems);
        }

        public ActionResult DeleteFormCompare(int id)
        {
            List<CompareItem> compareItems = new List<CompareItem>();
            if (Session["Compare"] != null)
            {
                compareItems = Session["Compare"] as List<CompareItem>;
                int index = compareItems.FindIndex(p => p.ProductId == id);
                compareItems.RemoveAt(index);
                Session["Compare"] = compareItems;
            }
            return PartialView("ListCompare", compareItems);
        }
    }
}