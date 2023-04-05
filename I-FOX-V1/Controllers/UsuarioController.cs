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
                return View(Caderno.listarCaderno(usuario.Nome));
            }
            return View();
        }

        public IActionResult Perfil()
        {
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("usuario"));
            string nomeUsuario = usuario.Nome;
            return View(usuario.listarUsuario(nomeUsuario));

            return View();
        }

        public IActionResult CriarCaderno()
        {
            return View();
        }

        public IActionResult EditarCaderno()
        {
            return View("./CriarCaderno");
        }

        public IActionResult Pomodoro()
        {
            return View();
        }

        //MÉTODOS que realizam o crud
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
                return Redirect("./Home");
            }
            
            return View();
        }

        public IActionResult Materia(int id)
        {
            Caderno caderno = Caderno.CadernoSelecionado(id);
            //Criar uma sessão para armazenar os dados do usuário
            HttpContext.Session.SetString("cadernoAcessado", JsonConvert.SerializeObject(caderno));
            @TempData["descricaocaderno"] = caderno.Descricao;
            @TempData["tituloCaderno"] = caderno.Titulo;

            if (HttpContext.Session.GetString("cadernoAcessado") != "")
            {
                return View(Resumo.listarResumo(caderno.Codigo));
            }

            return View();
        }

       
        public IActionResult DeletarCaderno(int id)
        {
            Caderno.deletarCaderno(id);
            return Redirect("../Home");
        }

        [HttpPut]
        public IActionResult editarNomeCaderno(string nome)
        {
            Caderno.editarNome(nome);
            return Redirect("../Home");
        }

        [HttpPut]
        public IActionResult editarDescicaoCaderno(string descricao)
        {
            Caderno.editarNome(descricao);
            return Redirect("../Home");
        }
    }
}
