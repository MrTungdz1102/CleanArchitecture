using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers
{
    public class VillaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Update(int villaId)
        {
            return View();
        }
        public IActionResult Delete(int villaId)
        {
            return View();
        }
    }
}
