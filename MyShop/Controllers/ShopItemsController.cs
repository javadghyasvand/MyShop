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
        MyEShop_DBEntities _dbEntities = new MyEShop_DBEntities();

        // GET: ShopItems
        public ActionResult ShowCart()
        {
            List<ShopCartItemViewModel> sales = new List<ShopCartItemViewModel>();
            if (Session["ShopCart"] != null)
            {
                List<ShopCartItem> shopCartItems = (List<ShopCartItem>)Session["ShopCart"];
                foreach (var item in shopCartItems)
                {
                    var product = _dbEntities.Products.Where(p => p.ProductId == item.ProductId).Select(p => new
                    {
                        p.ImageName, p.Title
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

        public ActionResult Index()
        {
            return View();
        }

        List<ShowOrderViewModel> GetListOrder()
        {
            List<ShowOrderViewModel> showOrders = new List<ShowOrderViewModel>();
            if (Session["ShopCart"] != null)
            {
                List<ShopCartItem> listShopCart = Session["ShopCart"] as List<ShopCartItem>;
                foreach (var item in listShopCart)
                {
                    var product = _dbEntities.Products.Single(P => P.ProductId == item.ProductId);
                    showOrders.Add(new ShowOrderViewModel()
                    {
                        ProductId = item.ProductId,
                        Count = item.Count,
                        ImageName = product.ImageName,
                        Title = product.Title,
                        Price = product.Price,
                        Sum = item.Count * product.Price
                    });
                }
            }

            return showOrders;
        }

        public ActionResult Order()
        {
            return PartialView(GetListOrder());
        }

        public ActionResult CommandOrder(int id, int count)
        {
            List<ShopCartItem> listShopCart = Session["ShopCart"] as List<ShopCartItem>;
            int indx = listShopCart.FindIndex(p => p.ProductId == id);
            if (count == 0)
            {
                listShopCart.RemoveAt(indx);
            }
            else
            {
                listShopCart[indx].Count = count;
            }

            Session["ShopCart"] = listShopCart;
            return PartialView("Order", GetListOrder());
        }

        [Authorize]
        public ActionResult Payment()
        {
            int userId=_dbEntities.Users.Single(u=>u.UserName==User.Identity.Name).UserId;
            Orders orders = new Orders()
            {
                UserID = userId,
                OrderDate = DateTime.Now,
                IsFinaly = false,
            };
            _dbEntities.Orders.Add(orders);
            var detailList = GetListOrder();
            foreach (var item in detailList)
            {
                _dbEntities.OrderDetails.Add(new OrderDetails()
                {
                    Count = item.Count,
                    OrderId = orders.OrderID,
                    ProductID = item.ProductId,
                    Price = item.Price,
                });
            }
            //Online Payment
            _dbEntities.SaveChanges();
            return null;
        }
    }
}