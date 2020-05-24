using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Cubes3DIntersection.Api.Views;

namespace Cubes3DIntersection.Api.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public RedirectResult Index()
        {
            return Redirect("/swagger/");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
