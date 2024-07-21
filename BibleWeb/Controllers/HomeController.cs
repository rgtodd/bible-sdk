using BibleWebApi.Models;

using Microsoft.AspNetCore.Mvc;

namespace BibleWebApi.Controllers
{
    public class HomeController() : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var model = new HomeModel();

            return View("Index", model);
        }
    }
}
