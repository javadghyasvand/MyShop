using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DataLayer;


namespace MyShop.Areas.Admin.Controllers
{
    public class Product_GroupsController : Controller
    {
        private MyEShop_DBEntities db = new MyEShop_DBEntities();

        #region MyRegion

        // GET: Admin/Product_Groups
        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult ListGroups()
        {
            var product_Groups = db.Product_Groups.Include(p => p.Product_Groups2);
            return PartialView(product_Groups.ToList());
        }

        // GET: Admin/Product_Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Groups product_Groups = db.Product_Groups.Find(id);
            if (product_Groups == null)
            {
                return HttpNotFound();
            }
            return View(product_Groups);
        }

        // GET: Admin/Product_Groups/Create
        public ActionResult Create(int ? id)
        {
            return PartialView(new Product_Groups()
            {
                ParentId = id
            });
        }

        // POST: Admin/Product_Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupId,GroupTitle,ParentId")] Product_Groups product_Groups)
        {
            if (ModelState.IsValid)
            {
                db.Product_Groups.Add(product_Groups);
                db.SaveChanges();
                return PartialView("ListGroups", db.Product_Groups.Include(p => p.Product_Groups2));
            }

            ViewBag.ParentId = new SelectList(db.Product_Groups, "GroupId", "GroupTitle", product_Groups.ParentId);
            return View(product_Groups);
        }

        // GET: Admin/Product_Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            }
            Product_Groups product_Groups = db.Product_Groups.Find(id);
            if (product_Groups == null)
            {
                return HttpNotFound();
            }
            
            return PartialView(product_Groups);
        }

        // POST: Admin/Product_Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupId,GroupTitle,ParentId")] Product_Groups product_Groups)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_Groups).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("ListGroups", db.Product_Groups.Include(p => p.Product_Groups1));
            }
            ViewBag.ParentId = new SelectList(db.Product_Groups, "GroupId", "GroupTitle", product_Groups.ParentId);
            return View(product_Groups);
        }

        // GET: Admin/Product_Groups/Delete/5
        public void Delete(int id)
        {
            var group=db.Product_Groups.Find(id);
            if (group.Product_Groups1.Any())
            {
                foreach (var subGrouop in db.Product_Groups.Where(g=>g.ParentId==id))
                {
                    var subGrouopGroupId1 = subGrouop.GroupId;
                    if (db.Product_Groups.Any(g => g.ParentId == subGrouopGroupId1))
                    {
                        foreach (var sub2 in db.Product_Groups.Where(g=>g.ParentId== subGrouopGroupId1) )
                        {
                            db.Product_Groups.Remove(sub2);
                        }
                    }
                    db.Product_Groups.Remove(subGrouop);

                    // db.Entry(subGrouop).State= EntityState.Deleted;
                }
            }
            db.Product_Groups.Remove(group);
            db.SaveChanges();

            // if (id == null)
            // {
            //     return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            // }
            // Product_Groups product_Groups = db.Product_Groups.Find(id);
            // if (product_Groups == null)
            // {
            //     return HttpNotFound();
            // }
            // return View(product_Groups);
        }

        // POST: Admin/Product_Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_Groups product_Groups = db.Product_Groups.Find(id);
            db.Product_Groups.Remove(product_Groups);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
