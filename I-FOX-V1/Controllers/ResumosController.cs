using I_FOX_V1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace I_FOX_V1.Controllers
{
    public class ResumosController : Controller
    {
        public IActionResult ResumoEscrito()
        {

            return View();
        }

        [HttpPost]
        public IActionResult ResumoEscrito(string titulo, string data, string texto)
        {
            //Formatando a data que o usuário inseriu
            String dataCaractere = data.Replace("-", "");

            //Pegando o id do caderno que o resumo será salvo
            Caderno caderno = JsonConvert.DeserializeObject<Caderno>(HttpContext.Session.GetString("cadernoAcessado"));

            int codigoCaderno = caderno.Codigo;

            Resumo resumo = new Resumo(dataCaractere,"escrito",titulo,texto, " ", " ", codigoCaderno);
            TempData["Sit_Cad_Resumo"] = resumo.cadastrarResumo("escrito");
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
