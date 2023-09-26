using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers
{
    public class VillaNumberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Update(int villaNumberId)
        {
            return View();
        }
        public IActionResult Delete(int villaNumberId)
        {
            return View();
        }
    }
}
