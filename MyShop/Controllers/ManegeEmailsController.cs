using System.Web.Mvc;

namespace MyShop.Controllers
{
    public class ManegeEmailsController : Controller
    {
        // GET: ManegeEmails
        public ActionResult ActiveEmail()
        {
            return PartialView();
        }
        public ActionResult RecoveryPassword()
        {
            return PartialView();
        }
    }
}