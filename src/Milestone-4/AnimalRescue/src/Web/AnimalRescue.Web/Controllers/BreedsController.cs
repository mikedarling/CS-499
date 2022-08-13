using AnimalRescue.Services.Breeds;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AnimalRescue.Web.Controllers
{

    /// <summary>
    /// MVC Controller that handles traffic routed to {siteroot}/Breeds[/*].
    /// The controller requires the user to be Authenticated by way of the Authorize attribute.
    /// All actions are configured to behave asynchronously.
    /// </summary>
    public class BreedsController : Controller
    {

        #region Constructors

        public BreedsController(IBreedService breedService)
        {
            this._breedService = breedService;
        }

        #endregion

        #region Local Variables

        private readonly IBreedService _breedService;

        #endregion

        #region Actions

        // GET: Breeds[/Index]
        /// <summary>
        /// The controller's default action - provides a list of breeds.
        /// </summary>
        /// <returns>A list of breeds as a JSON array.</returns>
        public async Task<JsonResult> Index()
        {
            var models = await this._breedService.GetBreeds();
            if (models == null || !models.Any())
            {
                return Json(new { error = "No data found." }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { models = models }, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}