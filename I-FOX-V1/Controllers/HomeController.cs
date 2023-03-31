using I_FOX_V1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        //MÉTODOS que realizam o CRUD
        [HttpPost]
        public IActionResult Cadastrar(string nome, string email, string senha, string data)
        {
            //Validação para não entrar nomes de usuários sem espaços
            string nomeNoEspaco = nome.Replace(" ", "");

            if(nome == nomeNoEspaco)
            {
                //Formatando a data que o usuário inseriu
                string dataCaractere = data.Replace("-", "");

                //criando objeto de usuário para cadastrá-lo
                Usuario usuario = new Usuario(nome, email, senha, dataCaractere);

                TempData["SitCadastro"] = usuario.cadastrarUsuario();

                if (TempData["SitCadastro"] == "cadastrado")
                {
                    return Redirect("/Home/Login");
                }

            } else
            {
                TempData["validacaoUsuario"] = "Não insira espaços em branco!";
            }

            return View();
        }

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
                //Criar uma sessão para armazenar os dados do usuário
                HttpContext.Session.SetString("usuario", JsonConvert.SerializeObject(usuario));
                return Redirect("../Usuario/Home");
            }


            else
            {
                return View();
            }
        }

        public IActionResult Sair()
        {
            HttpContext.Session.Remove("usuario");
            return View("../Home/Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}