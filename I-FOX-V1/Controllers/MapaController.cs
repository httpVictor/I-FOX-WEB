using Microsoft.AspNetCore.Mvc;

namespace I_FOX_V1.Controllers
{
    public class MapaController : Controller
    {
        public IActionResult Fases()
        {
            return View();
        }

        public IActionResult Mapa()
        {
            return View();
        }

        public IActionResult Historia()
        {
            return View();
        }
    }
}
