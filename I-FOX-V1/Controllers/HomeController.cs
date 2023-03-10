using I_FOX_V1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace I_FOX_V1.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(string nome, string email, string senha, string data)
        {
            //Formatando a data que o usuário inseriu
            String dataCaractere = data.Replace("-", "");
            
            //criando objeto de usuário para cadastrá-lo
            Usuario usuario = new Usuario(nome, email, senha, dataCaractere);

            TempData["SitCadastro"] = usuario.cadastrarUsuario();

            if (TempData["SitCadastro"] == "cadastrado")
            {
                return Redirect("/Home/Login");
            }

            return View();
        }

        //MÉTODOS PARA SE COMUNICAR COM O BANCO
        [HttpPost]
        public IActionResult Login(string nome, string senha)
        {
            //CRIANDO OBJETO DO USUÁRIO PARA CONFERIR NO BANCO SE ELE EXISTE E SE A SENHA BATE
            Usuario usuario = new Usuario(nome, senha);
            string loginEstado = usuario.logarUsuario();
            TempData["SituacaoLogin"] = loginEstado;

            //CASO ELE CONSIGA LOGAR, SER DIRECIONADO PARA A PÁGINA INICIAL
            if (loginEstado == "logado")
            {
                return Redirect("../Usuario/Home");
            }
            else
            {
                return View();
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}