using AnimalRescue.Services.AnimalTypes;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AnimalRescue.Controllers
{

    /// <summary>
    /// MVC Controller that handles traffic routed to {siteroot}/AnimalTypes[/*].
    /// The controller requires the user to be Authenticated by way of the Authorize attribute.
    /// All actions are configured to behave asynchronously.
    /// </summary>
    [Authorize]
    public class AnimalTypesController : Controller
    {

        #region Constructors

        public AnimalTypesController(IAnimalTypeService animalTypeService)
        {
            this._animalTypeService = animalTypeService;
        }

        #endregion

        #region Local Variables

        private readonly IAnimalTypeService _animalTypeService;

        #endregion

        #region Actions

        // [GET] /AnimalTypes[/Index]
        /// <summary>
        /// The controller's default action - provides a list of animal types.
        /// </summary>
        /// <returns>A list of animal types as a JSON array.</returns>
        public async Task<JsonResult> Index()
        {
            var models = await this._animalTypeService.GetAnimalTypes();
            if (models == null || !models.Any())
            {
                return Json(new { error = "No data found." }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { models = models }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Helpers

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }

        #endregion

    }
}