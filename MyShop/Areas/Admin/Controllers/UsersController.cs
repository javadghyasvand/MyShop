using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using DataLayer;

namespace MyShop.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private MyEShop_DBEntities db = new MyEShop_DBEntities();

        // GET: Admin/Users
        public ActionResult Index()
        {
            var users = db.Users.ToList();
            return View(users.ToList());
        }

        // GET: Admin/Users/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.Roles, "RoleID", "RoleTitle");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,RoleId,UserName,Email,Password,ActiveCode,IsActive,RegisterDate")] Users users)
        {
            if (ModelState.IsValid)
            {
                users.RegisterDate=DateTime.Now;
                users.ActiveCode = Guid.NewGuid().ToString();
                users.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(users.Password, "MD5");
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.Roles, "RoleID", "RoleTitle", users.RoleId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", users.UserId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", users.UserId);
            return View(users);
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.Roles, "RoleID", "RoleTitle", users.RoleId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", users.UserId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", users.UserId);
            return View(users);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,RoleId,UserName,Email,Password,ActiveCode,IsActive,RegisterDate")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.Roles, "RoleID", "RoleTitle", users.RoleId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", users.UserId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", users.UserId);
            return View(users);
        }

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
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
    }
}
