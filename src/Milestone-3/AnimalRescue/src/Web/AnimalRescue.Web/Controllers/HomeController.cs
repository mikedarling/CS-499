using System.Web.Mvc;

namespace AnimalRescue.Controllers
{
    public class HomeController : Controller
    {

        #region Methods

        public ActionResult Index()
        {
            return View();
        }

        #endregion

    }
}