using Microsoft.AspNetCore.Mvc;

namespace I_FOX_V1.Controllers
{
    public class ResumosController : Controller
    {
        public IActionResult ResumoEscrito()
        {
            return View();
        }

        public IActionResult ResumoAudio()
        {
            return View();
        }

        public IActionResult ResumoFotos()
        {
            return View();
        }

        public IActionResult ResumoFlashCard()
        {
            return View();
        }
    }
}
