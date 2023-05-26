using I_FOX_V1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace I_FOX_V1.Controllers
{
    public class HomeController : Controller
    {
        //Métodos que retirnam tela

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
            
            string[] numeros = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};
            string[] caractereEspecial = {"@", "#", "!", ".", "_", "%", "*", " ", "/", "+"};

            //validando a senha
            bool validaSenha = false;

            //Validando o tamanho da senha
            if (senha.Length >= 8 && senha.Length <= 30 && senha != null)
            {
                //Validando se a senha possui números
                for (int i = 0; i < numeros.Length; i++)
                {
                    if (senha.Contains(numeros[i]))
                    {
                        
                        validaSenha = true;
                        break;
                        
                    }
                }

                ////Agora validando se ela tem caracteres especiais
                //for (int i = 0; i < caractereEspecial.Length; i++)
                //{
                //    if (senha.Contains(caractereEspecial[i]))
                //    {
                //        validaSenha = true;
                //        break;
                //    }
                //    else
                //    {
                //        validaSenha = false;
                //    }
                //}
            }

            string nomeNoEspaco ="";
            if (nome != null)
            {
                nomeNoEspaco = nome.Replace(" ", "");
            }

            //Validação para não entrar nomes de usuários sem espaços

            //validação do campo nome (Espaço e qantidade de caracteres
            if (nome == nomeNoEspaco && nome.Length >= 5 && nome.Length <= 15 && validaSenha && nome != null)
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
                TempData["validacaoUsuario"] = "Preencha os campos corretamente";
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
            TempData["SituacaoLogin"] = "";
            return View("../Home/Index");
        }




        //Automatico do sistema

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}