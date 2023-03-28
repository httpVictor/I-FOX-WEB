using I_FOX_V1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;

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

        public IActionResult ResumoFotos(string titulo, string data)
        {
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

                    Resumo resumo = new Resumo(data, "foto", titulo, null, null, null, codigoCaderno);
                    resumo.cadastrarResumo("foto");
                    TempData["msg"] = "Salvo com sucesso!";
                }
                else
                {
                    TempData["msg"] = "Erro de cadastro, coloque apenas imagens";
                }
            }
            return View();
        }

        public IActionResult ResumoFlashCard()
        {
            return View();
        }
    }
}
