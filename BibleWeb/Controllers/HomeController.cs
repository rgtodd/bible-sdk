using BibleWeb.Models;

using Microsoft.AspNetCore.Mvc;

namespace BibleWeb.Controllers
{
    public class HomeController() : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var model = new HomeModel();

            return View("Index", model);
        }
    }
}
