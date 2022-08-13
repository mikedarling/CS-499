using AnimalRescue.Security.Roles;
using System.Web.Mvc;

namespace AnimalRescue.Controllers
{

    /// <summary>
    /// Default controller. Handles requests where
    ///    - there is no path
    ///    - the primary segment of the path
    ///        - is /Home
    ///        - doesn't match another controller
    /// This controller is protected by the Authorize attribute.
    /// Unauthenticated requests will be redirected to /Login.
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {

        #region Methods

        /// <summary>
        /// Action to return the root of the site.
        /// </summary>
        /// <returns>The Home page.</returns>
        public ActionResult Index()
        {
            ViewBag.CanEdit = User.IsInRole(RoleConstants.ADMINISTRATOR) || User.IsInRole(RoleConstants.EDITOR);
            return View();
        }

        #endregion

    }
}