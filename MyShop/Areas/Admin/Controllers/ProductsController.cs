using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using InsertShowImage;
using KooyWebApp_MVC.Classes;
// ReSharper disable All

namespace MyShop.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private MyEShop_DBEntities _dbEntities = new MyEShop_DBEntities();

        #region Product

        // GET: Admin/Products_MetaData
        public ActionResult Index()
        {
            return View(_dbEntities.Products.ToList());
        }

        // GET: Admin/Products_MetaData/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Products products = _dbEntities.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }

            return View(products);
        }

        // GET: Admin/Products_MetaData/Create
        public ActionResult Create()
        {
            ViewBag.Groups = _dbEntities.Product_Groups.ToList();
            return View();
        }

        // POST: Admin/Products_MetaData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "ProductId,Title,ShortDescription,Text,Price,ImageName,CreateDate")]
            Products products,
            List<int> selectedgroups, HttpPostedFileBase ImageProduct, String tags)
        {
            if (ModelState.IsValid)
            {
                if (selectedgroups == null)
                {
                    ViewBag.ErorrSelectedGroups = true;
                    ViewBag.Groups = _dbEntities.Product_Groups.ToList();
                    return View(products);
                }

                products.ImageName = "images.png";
                if (ImageProduct != null && ImageProduct.IsImage())
                {
                    products.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImageProduct.FileName);
                    ImageProduct.SaveAs(Server.MapPath("/Images/ProductImages/" + products.ImageName));
                    ImageResizer imageResizer = new ImageResizer();
                    imageResizer.Resize(Server.MapPath("/Images/ProductImages/" + products.ImageName),
                        Server.MapPath("/Images/ProductImages/Thumbnail/" + products.ImageName));
                }
                else
                {
                    ViewBag.ErorrIsImage = true;
                    ViewBag.Groups = _dbEntities.Product_Groups.ToList();
                    return View(products);
                }

                products.CreateDate = DateTime.Now;
                _dbEntities.Products.Add(products);
                foreach (int group in selectedgroups)
                {
                    _dbEntities.Product_Select_Groups.Add(new Product_Select_Groups()
                    {
                        ProductID = products.ProductId,
                        GroupID = group
                    });
                }

                if (!string.IsNullOrEmpty(tags))
                {
                    string[] tagsArray = tags.Split('#');
                    foreach (string tag in tagsArray)
                    {
                        _dbEntities.Product_Tag.Add(new Product_Tag()
                        {
                            ProductID = products.ProductId,
                            Tag = tag.Trim(),
                        });
                    }
                }

                _dbEntities.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(products);
        }


        // GET: Admin/Products_MetaData/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Products products = _dbEntities.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }

            ViewBag.SelectedGroups = products.Product_Select_Groups.ToList();
            ViewBag.Groups = _dbEntities.Product_Groups.ToList();
            ViewBag.Tags = string.Join("#", products.Product_Tag.Select(t => t.Tag).ToList());
            return View(products);
        }

        // POST: Admin/Products_MetaData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "ProductId,Title,ShortDescription,Text,Price,ImageName,CreateDate")]
            Products products,
            List<int> selectedgroups, HttpPostedFileBase ImageProduct, String tags)
        {
            if (ModelState.IsValid)
            {
                if (ImageProduct != null && ImageProduct.IsImage())
                {
                    if (ImageProduct.FileName == "images.png")
                    {
                    }
                    else
                    {
                        System.IO.File.Delete(Server.MapPath("/Images/ProductImages/" + products.ImageName));
                        System.IO.File.Delete(Server.MapPath("/Images/ProductImages/Thumbnail/" + products.ImageName));
                        products.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImageProduct.FileName);
                        ImageProduct.SaveAs(Server.MapPath("/Images/ProductImages/" + products.ImageName));
                        ImageResizer imageResizer = new ImageResizer();
                        imageResizer.Resize(Server.MapPath("/Images/ProductImages/" + products.ImageName),
                            Server.MapPath("/Images/ProductImages/Thumbnail/" + products.ImageName));
                    }
                }

                _dbEntities.Entry(products).State = EntityState.Modified;


                _dbEntities.Product_Tag.Where(t => t.ProductID == products.ProductId).ToList()
                    .ForEach(t => _dbEntities.Product_Tag.Remove(t));
                if (!string.IsNullOrEmpty(tags))
                {
                    string[] tagsArray = tags.Split('#');
                    foreach (string tag in tagsArray)
                    {
                        _dbEntities.Product_Tag.Add(new Product_Tag()
                        {
                            ProductID = products.ProductId,
                            Tag = tag.Trim(),
                        });
                    }
                }

                _dbEntities.Product_Select_Groups.Where(g => g.ProductID == products.ProductId).ToList()
                    .ForEach(groups => _dbEntities.Product_Select_Groups.Remove(groups));
                if (selectedgroups != null && selectedgroups.Any())
                {
                    foreach (int group in selectedgroups)
                    {
                        _dbEntities.Product_Select_Groups.Add(new Product_Select_Groups()
                        {
                            ProductID = products.ProductId,
                            GroupID = group
                        });
                    }
                }
                _dbEntities.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SelectedGroups = selectedgroups;
            ViewBag.Groups = _dbEntities.Product_Groups.ToList();
            ViewBag.Tags = tags;
            return View(products);
        }

        // GET: Admin/Products_MetaData/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Products products = _dbEntities.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }

            return View(products);
        }

        // POST: Admin/Products_MetaData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = _dbEntities.Products.Find(id);
            _dbEntities.Products.Remove(products);
            _dbEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbEntities.Dispose();
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Gallery

        public ActionResult Gallery(int id)
        {
            ViewBag.Gallery = _dbEntities.Product_Galleries.Where(g => g.ProductID == id).ToList();
            return View(new Product_Galleries()
            {
                ProductID = id
            });
        }

        [HttpPost]
        public ActionResult Gallery(Product_Galleries productGalleries, HttpPostedFileBase ImgUP)
        {
            if (ModelState.IsValid)
            {
                if (ImgUP != null && ImgUP.IsImage())
                {
                    productGalleries.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUP.FileName);
                    ImgUP.SaveAs(Server.MapPath("/Images/ProductImages/" + productGalleries.ImageName));
                    ImageResizer imgResizer = new ImageResizer();
                    imgResizer.Resize(Server.MapPath("/Images/ProductImages/" + productGalleries.ImageName),
                        Server.MapPath("/Images/ProductImages/Thumbnail/" + productGalleries.ImageName));
                    _dbEntities.Product_Galleries.Add(productGalleries);
                    _dbEntities.SaveChanges();
                    return RedirectToAction("Gallery", new { id = productGalleries.ProductID });
                }
            }

            return RedirectToAction("Gallery", new { id = productGalleries.ProductID });
        }

        public ActionResult DeleteGallery(int id)
        {
            var gallery = _dbEntities.Product_Galleries.Find(id);
            System.IO.File.Delete(Server.MapPath("/Images/ProductImages/" + gallery.ImageName));
            System.IO.File.Delete(Server.MapPath("/Images/ProductImages/Thumbnail/" + gallery.ImageName));
            _dbEntities.Product_Galleries.Remove(gallery);
            _dbEntities.SaveChanges();
            return RedirectToAction("Gallery", new { id = gallery.ProductID });
        }

        #endregion

        #region Featurs

        public ActionResult ProductFeature(int id)
        {
            ViewBag.Featuers = _dbEntities.Product_Featuers.Where(f => f.Product_Id == id);
            ViewBag.FeatuersID = _dbEntities.Features.ToList();
            return View(new Product_Featuers()
            {
                Product_Id = id
            });
        }

        [HttpPost]
        public ActionResult ProductFeature(Product_Featuers feature)
        {
            if (ModelState.IsValid)
            {
                _dbEntities.Product_Featuers.Add(feature);
                _dbEntities.SaveChanges();
            }
            return RedirectToAction("ProductFeature", new { id = feature.Product_Id });
        }

        public void DeleteFeature(int id)
        {
            var feature = _dbEntities.Product_Featuers.Find(id);
            if (feature != null) _dbEntities.Product_Featuers.Remove(feature);
            _dbEntities.SaveChanges();

        }
        #endregion


    }
}