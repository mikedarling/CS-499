using AnimalRescue.Security.Authentication;
using AnimalRescue.Web.ViewModels;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AnimalRescue.Web.Controllers
{

    /// <summary>
    /// Controller that handles login/logoff requests.
    /// </summary>
    public class LoginController : Controller
    {

        #region Constructors

        public LoginController(IUserService userService)
        {
            this._userService = userService;
        }

        #endregion

        #region Local Variables

        private IUserService _userService;

        #endregion

        #region Actions

        // GET: Login
        /// <summary>
        /// Action that returns the login page. This action is open to
        /// anonymous/non-authenticated traffic.
        /// </summary>
        /// <returns>The Login page, unless the user is authenticated, in which case it redirects to the home page.</returns>
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            return View();
        }

        // POST: Login
        /// <summary>
        /// This action handles login attempts. This action is open to 
        /// anonymous/non-authenticated traffic
        /// </summary>
        /// <param name="viewModel">Object containing the submitted username and password.</param>
        /// <returns>The Login page with errors, or a redirect to the home page.</returns>
        [HttpPost]
        public async Task<ActionResult> Index(LoginViewModel viewModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                return null;
            }

            if (!ModelState.IsValid)
            {
                ViewBag.HasErrors = true;
                return View(viewModel);
            }

            var found = await this._userService.AttemptLogin(viewModel.Username, viewModel.Password);
            if (!found)
            {
                ModelState.AddModelError("username", "Invalid Username/Password combination.");
                return View();
            }

            
            return Redirect("/");
        }

        // GET: Login/Logoff
        /// <summary>
        /// Ends the current user session.
        /// </summary>
        /// <returns>A redirect to the login page.</returns>
        [Authorize]
        public ActionResult Logoff()
        {
            this._userService.Logout();
            return Redirect("/Login");
        }

        #endregion

    }
}