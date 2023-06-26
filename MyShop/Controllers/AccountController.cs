using System;
using System.Linq;
using System.Web.Mvc;
using DataLayer;
using DataLayer.ViewModels;
using System.Web.Security;

namespace MyShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly MyEShop_DBEntities _dbEntities = new MyEShop_DBEntities();

        // GET: Account
        [Route("Register")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Register")]
        public ActionResult Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                if (!_dbEntities.Users.Any(u => u.Email == register.Email.Trim().ToLower()))
                {
                    Users user = new Users()
                    {
                        Email = register.Email.Trim().ToLower(),
                        Password = FormsAuthentication.HashPasswordForStoringInConfigFile(register.Password, "MD5"),
                        ActiveCode = Guid.NewGuid().ToString(),
                        RegisterDate = DateTime.Now,
                        IsActive = false,
                        RoleId = 1,
                        UserName = register.UserName
                    };
                    _dbEntities.Users.Add(user);
                    _dbEntities.SaveChanges();
                    //send Active Email
                    string body = PartialToStringClass.RenderPartialView("ManegeEmails", "ActiveEmail", user);
                    SendEmail.Send(user.Email, "ایمیل فعال سازی", body);
                    //End Active Email
                    return View("RegisterSuccess", user);
                }
                else
                {
                    ModelState.AddModelError("Email", "ایمیل وارد شده تکراری است");
                }
            }

            return View(register);
        }

        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }


        [Route("Login")]
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel, string returnurl = "/")
        {
            if (ModelState.IsValid)
            {
                var HashPassword =
                    FormsAuthentication.HashPasswordForStoringInConfigFile(loginViewModel.Password, "MD5");
                var user = _dbEntities.Users.SingleOrDefault(u =>
                    u.Email == loginViewModel.Email && u.Password == HashPassword);
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        FormsAuthentication.SetAuthCookie(user.UserName, loginViewModel.Remember);
                        return Redirect(returnurl);
                    }
                    else
                    {
                        ModelState.AddModelError("Email",
                            "لطفا  با ورود به ایمیل" + @user.Email + " حساب کاربری خود را فعال کنید");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "کابری با اطلاعات وارد شده یافت نشد");
                }
            }

            return View();
        }

        public ActionResult ActiveUser(string id)
        {
            var user = _dbEntities.Users.SingleOrDefault(u => u.ActiveCode == id);
            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.UserName = user.UserName;
            user.IsActive = true;
            user.ActiveCode = Guid.NewGuid().ToString();
            _dbEntities.SaveChanges();
            return View();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

        [Route("ForgotPassword")]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public ActionResult ForgotPassword(ForgotPassWordViewModel forgotViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _dbEntities.Users.SingleOrDefault(u => u.Email == forgotViewModel.Email);
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        string bodyEmail =
                            PartialToStringClass.RenderPartialView("ManegeEmails", "RecoveryPassword", user);
                        SendEmail.Send(user.Email, "بازیابی کلمه عبور", bodyEmail);
                        return View("ForgotSuccess", user);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "حساب کاربری شما فعال نیست");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "کاربری با این ایمیل یافت نشد");
                }
            }

            return View();
        }

        public ActionResult RecoveryPassword(string id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult RecoveryPassword(string id,RecoveryPassWordViewModel recoveryPassWordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _dbEntities.Users.SingleOrDefault(u => u.ActiveCode == id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.Password =
                    FormsAuthentication.HashPasswordForStoringInConfigFile(recoveryPassWordViewModel.Password, "MD5");
                user.ActiveCode = Guid.NewGuid().ToString();
                _dbEntities.SaveChanges();
                return Redirect("/Login?recovery=true");
            }
            return View();
        }
    }
}