using I_FOX_V1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace I_FOX_V1.Controllers
{
    public class UsuarioController : Controller
    {

        public IActionResult Home()
        {
            if (HttpContext.Session.GetString("usuario") != "")
            {
                Usuario usuario = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("usuario"));
                TempData["nomeUsuario"] = usuario.Nome;
            }
            return View();
        }

        public IActionResult CriarCaderno()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CriarCaderno(string titulo, string descricao)
        {
            if (HttpContext.Session.GetString("usuario") != "")
            {
                Usuario usuario = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("usuario"));
                string nomeUser = usuario.Nome;
                //Criando o objeto de caderno
                Caderno caderno = new Caderno(descricao, titulo, nomeUser);
                TempData["Testes"] = caderno.cadastrarCaderno();
            }
            
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
