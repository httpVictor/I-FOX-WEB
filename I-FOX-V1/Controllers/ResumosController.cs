using I_FOX_V1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;

namespace I_FOX_V1.Controllers
{
    public class ResumosController : Controller
    {
        //RETORNANDO AS TELAS
        public IActionResult CadastrarResumo(string id)
        {
            ViewBag.Id = id;
            return View();
        }
        public IActionResult ResumoEscrito()
        {
            return View();
        }
        public IActionResult ResumoFlashCard()
        {

            return View();
        }

        public IActionResult ResumoFotos()
        {
            return View();
        }

        public IActionResult ResumoAudio()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarResumo(string id, string titulo, string data)
        {
            //Formatando a data que o usuário inseriu
            string dataCaractere = data.Replace("-", "");
            

            //Pegando o id do caderno que o resumo será salvo
            Caderno caderno = JsonConvert.DeserializeObject<Caderno>(HttpContext.Session.GetString("cadernoAcessado"));

            int codigoCaderno = caderno.Codigo;

            Resumo resumo = new Resumo(dataCaractere, id, titulo, null, null, null, codigoCaderno);
            TempData["Sit_Cad_Resumo"] = resumo.cadastrarResumo();

            return Redirect("../Resumo"+ id);
            
        }

        //MÉTODOS QUE VÃO RELACIONAR MODELO E TELA
        [HttpPost]
        public IActionResult ResumoFotos(string titulo, string data)
        {
            string dataCaractere = data.Replace("-", "");
            //Procurando a imagem
            foreach (IFormFile arquivo in Request.Form.Files)
            {
                string tipoArquivo = arquivo.ContentType;
                
                if (tipoArquivo.Contains("image"))
                {//se for imagem gravar no banco
                    MemoryStream s = new MemoryStream();
                    arquivo.CopyTo(s);
                    byte[] bytesDoArquivo = s.ToArray(); //transformar em cadeia de byte


                    //Pegando o id do caderno que o resumo será salvo
                    Caderno caderno = JsonConvert.DeserializeObject<Caderno>(HttpContext.Session.GetString("cadernoAcessado"));
                    int codigoCaderno = caderno.Codigo;

                    Resumo resumo = new Resumo(dataCaractere, "foto", titulo, null, null, null, codigoCaderno);
                    resumo.cadastrarResumo();
                    resumo.cadastrarArquivos(bytesDoArquivo);
                    TempData["msg"] = "Salvo com sucesso!";
                }
                else
                {
                    TempData["msg"] = "Erro de cadastro, coloque apenas imagens";
                }
            }

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

            Resumo resumo = new Resumo(dataCaractere, "escrito", titulo, texto, null, null, codigoCaderno);
            TempData["Sit_Cad_Resumo"] = resumo.cadastrarResumo();

            return View("..Usuario/Materia/");
        }

    }
}

