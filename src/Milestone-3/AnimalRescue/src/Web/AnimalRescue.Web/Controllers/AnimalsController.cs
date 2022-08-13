using AnimalRescue.Services.Animals;
using System;
using System.Linq;
using System.Web.Mvc;

namespace AnimalRescue.Controllers
{
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

        // [GET] /Animals[/Index]
        public JsonResult Index()
        {
            var models = this._animalService.GetAnimals();
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