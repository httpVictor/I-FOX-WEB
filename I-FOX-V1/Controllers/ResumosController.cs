using I_FOX_V1.Models;
using Microsoft.AspNetCore.Mvc;

namespace I_FOX_V1.Controllers
{
    public class ResumosController : Controller
    {
        public IActionResult ResumoEscrito()
        {
            
            return View();
        }

        [HttpGet]
        public IActionResult ResumoEscrito(string titulo, string data, string texto, int id)
        {
            //Formatando a data que o usuário inseriu
            String dataCaractere = data.Replace("-", "");

            Resumo resumo = new Resumo(dataCaractere,"escrito",titulo,texto, null, null, id);
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
