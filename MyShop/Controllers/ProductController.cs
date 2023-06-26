using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DataLayer;
using DataLayer.ViewModels;

namespace MyShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly MyEShop_DBEntities _dbEntities = new MyEShop_DBEntities();

        // GET: Product
        public ActionResult ShowGroups()
        {
            return PartialView(_dbEntities.Product_Groups.ToList());
        }

        public ActionResult LastProduct()
        {
            var products = _dbEntities.Products.OrderByDescending(p => p.CreateDate).Take(10);
            return PartialView(products);
        }

        [Route("ShowProduct/{id}")]
        public ActionResult ShowProduct(int id)
        {
            var product = _dbEntities.Products.Find(id);
            ViewBag.ProductFeatuer = product.Product_Featuers.DistinctBy(f => f.Featuers_Id).Select(f =>
                new ShowproductFeatuerViewModel()
                {
                    FeatuerTitle = f.Features.FeaturesTitle,
                    Values = _dbEntities.Product_Featuers.Where(fe => fe.Featuers_Id == f.Featuers_Id)
                        .Select(fe => fe.Value).ToList()
                }).ToList();
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        public ActionResult ShowComments(int id)
        {
            return PartialView(_dbEntities.Product_Comment.Where(p => p.ProductID == id));
        }

        public ActionResult CreateComment(int id)
        {
            return PartialView(new Product_Comment()
            {
                ProductID = id
            });
        }

        [HttpPost]
        public ActionResult CreateComment(Product_Comment productComment)
        {
            if (ModelState.IsValid)
            {
                productComment.CommentDate = DateTime.Now;
                _dbEntities.Product_Comment.Add(productComment);
                _dbEntities.SaveChanges();
                return PartialView("ShowComments",_dbEntities.Product_Comment.Where(p => p.ProductID == productComment.ProductID));
            }

            return PartialView(productComment);
        }

        [Route("Archive")]
        public ActionResult ArchiveProduct(int pageId=1,string title="",int minPrice=0,int maxPrice=0,List<int> selectGroup=null)
        {
            ViewBag.groups = _dbEntities.Product_Groups.ToList();
            ViewBag.productTitle = title;
            ViewBag.minPrice = minPrice;
            ViewBag.maxPrice = maxPrice;
            ViewBag.selectGroup = selectGroup;
            ViewBag.pageId=pageId;
            List<Products> list = new List<Products>();
            if (selectGroup != null && selectGroup.Any())
            {
                foreach (int group in selectGroup)
                {
                    list.AddRange(_dbEntities.Product_Select_Groups.Where(g=>g.GroupID==group).Select(p=>p.Products).ToList());
                }
                list= list.Distinct().ToList();
            }
            else
            {
              list.AddRange(_dbEntities.Products.ToList());
            }
            if (title != "")
            {
                list = list.Where(p => p.Title.Contains(title)).ToList();
            }
            if (minPrice > 0)
            {
                list = list.Where(p => p.Price >= minPrice).ToList();
            }
            if (maxPrice > 0)
            {
                list = list.Where(p => p.Price <= maxPrice).ToList();
            }
            //paging
            int take = 9;
            int skip = (pageId - 1) * take;
            ViewBag.pagecount = list.Count/take;
            return View(list.OrderByDescending(p=>p.CreateDate).Skip(skip).Take(take).ToList());
        }
    }
}