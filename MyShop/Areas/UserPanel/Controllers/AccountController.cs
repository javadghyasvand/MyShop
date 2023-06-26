using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using DataLayer;
using DataLayer.ViewModels;

namespace MyShop.Areas.UserPanel.Controllers
{
    public class AccountController : Controller
    {
        readonly MyEShop_DBEntities _dbEntities = new MyEShop_DBEntities();

        // GET: UserPanel/Account
        public ActionResult ChangPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangPassword(ChangPasswordViewModel changPassword)
        {
            if (ModelState.IsValid)
            {
                var user = _dbEntities.Users.Single(u => u.UserName == User.Identity.Name);
                string oldpassword =
                    FormsAuthentication.HashPasswordForStoringInConfigFile(changPassword.OldPassword, "MD5");
                if (user.Password == oldpassword)
                {
                    string hashNewPassword =
                        FormsAuthentication.HashPasswordForStoringInConfigFile(changPassword.Password, "MD5");
                    user.Password = hashNewPassword;
                    _dbEntities.SaveChanges();
                    ViewBag.Success = true;
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "کلمه عبور فعلی درست نمی باشد");
                }
            }

            return View();
        }
    }
}