using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.ViewModels;

namespace MyShop.Controllers
{
    public class ShopItemsController : Controller
    {
        MyEShop_DBEntities _dbEntities =new MyEShop_DBEntities();
        // GET: ShopItems
        public ActionResult ShowCart()
        {
            List<ShopCartItemViewModel> sales = new List<ShopCartItemViewModel>();
            if (Session["ShopCart"] != null)
            {
                List<ShopCartItem> shopCartItems= (List<ShopCartItem>)Session["ShopCart"];
                foreach (var item in shopCartItems)
                {
                    var product = _dbEntities.Products.Where(p => p.ProductId == item.ProductId).Select(p => new
                    {
                        p.ImageName,p.Title
                    }).Single();
                    sales.Add(new ShopCartItemViewModel()
                    {
                        ProductId = item.ProductId,
                        Count = item.Count,
                        Title = product.Title,
                        ImageName = product.ImageName

                    });
                }
            }
            return PartialView(sales);
        }
    }
}