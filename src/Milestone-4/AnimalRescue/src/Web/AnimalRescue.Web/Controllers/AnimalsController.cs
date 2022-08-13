using AnimalRescue.Data.Models.DomainModels;
using AnimalRescue.Security.Roles;
using AnimalRescue.Services.Animals;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AnimalRescue.Controllers
{

    /// <summary>
    /// MVC Controller that handles traffic routed to {siteroot}/Animals[/*].
    /// The controller requires the user to be Authenticated by way of the Authorize attribute.
    /// All actions are configured to behave asynchronously.
    /// </summary>
    [Authorize]
    public class AnimalsController : Controller
    {

        #region Constructors

        public AnimalsController(IAnimalService animalService)
        {
            this._animalService = animalService;
        }

        #endregion

        #region Local Variables

        private readonly IAnimalService _animalService;

        #endregion

        #region Actions

        // [GET] Animals[/Index]
        /// <summary>
        /// The controller's default action - provides a list of animals.
        /// </summary>
        /// <returns>A list of animals as a JSON array.</returns>
        public async Task<JsonResult> Index()
        {
            var models = await this._animalService.GetAnimals();
            if (models == null || !models.Any())
            {
                return Json(new { error = "No data found." }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { models = models }, JsonRequestBehavior.AllowGet);
        }

        // [GET] Animals/Details/{id}
        /// <summary>
        /// Action that provides editable details for a given animal.
        /// identified naimal.
        /// </summary>
        /// <param name="id">The ID of the requested animal.</param>
        /// <returns>A JSON object with the editable animal.</returns>
        public async Task<JsonResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Json(new { error = "No data found." }, JsonRequestBehavior.AllowGet);
            }

            var model = await this._animalService.GetAnimalDetailByAnimalId(id);
            if (model == null)
            {
                return Json(new { error = "No data found." }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { model = model }, JsonRequestBehavior.AllowGet);
        }

        // [POST] Animals/Details
        /// <summary>
        /// Action that updates the editable details of an animal. Restricted to 
        /// users belonging to the Administrator or Editor role.
        /// </summary>
        /// <param name="viewModel">Model returned by the client with the updated animal information.</param>
        /// <returns>The updated animal.</returns>
        [HttpPost]
        [Authorize(Roles = (RoleConstants.ADMINISTRATOR + ", " + RoleConstants.EDITOR))]
        public async Task<JsonResult> Details(AnimalDetailModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { error = "There was an error processing your request." }, JsonRequestBehavior.AllowGet);
            }

            var model = await this._animalService.UpdateAnimal(viewModel);
            if (model == null)
            {
                return Json(new { error = "No data found." }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { model = model }, JsonRequestBehavior.AllowGet);
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