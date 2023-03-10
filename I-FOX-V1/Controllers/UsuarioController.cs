using Microsoft.AspNetCore.Mvc;

namespace I_FOX_V1.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }

        public IActionResult CriarCaderno()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CriarCaderno(string titulo, string descricao)
        {
            return View();
        }
        public IActionResult Materia()
        {
            return View();
        }

        public IActionResult ResumoEscrito()
        {
            return View();
        }

        public IActionResult ResumoAudio()
        {
            return View();
        }

    }
}
