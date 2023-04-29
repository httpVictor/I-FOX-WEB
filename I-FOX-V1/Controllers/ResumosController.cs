using I_FOX_V1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Globalization;

namespace I_FOX_V1.Controllers
{
    public class ResumosController : Controller
    {
        //RETORNANDO AS TELAS
        public IActionResult CadastrarResumo(string id)
        {
            var anoData = DateTime.Now.Year;
            var mesData = string.Format("{0:MM}", DateTime.Now);
            var diaData = DateTime.Now.Day;
            string dataAtual = anoData + "-" + mesData + "-" + diaData;

            ViewBag.DataAtual = dataAtual;

            ViewBag.Id = id;
            return View();
        }
        public IActionResult ResumoEscrito()
        {
            var anoData = DateTime.Now.Year;
            var mesData = string.Format("{0:MM}", DateTime.Now);
            var diaData = DateTime.Now.Day;
            string dataAtual = anoData + "-" + mesData + "-" + diaData;
            
            ViewBag.DataAtual = dataAtual;
            return View();
        }

        public IActionResult ResumoFoto(string id)
        {
            return View(Arquivo.listar(id));
        }

        public IActionResult ResumoAudio()
        {
            return View();
        }

        public IActionResult ResumoFlashCard()
        {

            return View();
        }






        //TELAS DE VISUALIZAÇÃO DOS RESUMOS
        public IActionResult VisualizarEscrito(int id)
        {
            return View(Resumo.acessarResumo(id));
        }

        public IActionResult VisualizarFlashcard()
        {
            return View();
        }







        //MÉTODOS QUE VÃO RELACIONAR MODELO E TELA
        //Cadastro
        [HttpPost]
        public IActionResult CadastrarResumo(string id, string titulo, string data)
        {
            //Formatando a data que o usuário inseriu
            string dataCaractere = data.Replace("-", "");
            
            //Pegando o id do caderno que o resumo será salvo
            Caderno caderno = JsonConvert.DeserializeObject<Caderno>(HttpContext.Session.GetString("cadernoAcessado"));
            int codigoCaderno = caderno.Codigo;

            //Cadastrando no banco de dados
            Resumo resumo = new Resumo(dataCaractere, id, titulo, null, codigoCaderno);
            TempData["Sit_Cad_Resumo"] = resumo.cadastrarResumo();

            //guardando numa sessão
            HttpContext.Session.SetString("tituloResumo", titulo);
            return Redirect("../Resumo"+ id);
        }
        
        [HttpPost]
        public IActionResult ResumoFotos()
        {
            
            //Procurando arquivos...
            foreach (IFormFile arquivo in Request.Form.Files)
            {
                string tipoArquivo = arquivo.ContentType;

                //se for imagem...
                if (tipoArquivo.Contains("image"))
                {
                    
                    MemoryStream s = new MemoryStream();
                    arquivo.CopyTo(s);
                    byte[] bytesDoArquivo = s.ToArray(); //transformar em cadeia de byte

                    //Pegando o id do caderno que o resumo será salvo
                    Caderno caderno = JsonConvert.DeserializeObject<Caderno>(HttpContext.Session.GetString("cadernoAcessado"));
                    int codigoCaderno = caderno.Codigo;

                    string titulo = HttpContext.Session.GetString("tituloResumo");
                    Arquivo arq = new Arquivo(bytesDoArquivo, titulo, codigoCaderno);
                    arq.cadastrar();
                    TempData["msg"] = "Salvo com sucesso!";
                }
                else
                {
                    TempData["msg"] = "Erro de cadastro, coloque apenas imagens";
                }
            }

            return Redirect("../Usuario/Materia");
        }

        [HttpPost]
        public IActionResult ResumoEscrito(string titulo, string data, string texto)
        {
            //Formatando a data que o usuário inseriu
            String dataCaractere = data.Replace("-", "");

            //Pegando o id do caderno que o resumo será salvo
            Caderno caderno = JsonConvert.DeserializeObject<Caderno>(HttpContext.Session.GetString("cadernoAcessado"));
            int codigoCaderno = caderno.Codigo;

            Resumo resumo = new Resumo(dataCaractere, "Escrito", titulo, texto, codigoCaderno);
            TempData["Sit_Cad_Resumo"] = resumo.cadastrarResumo();

            return Redirect("../Usuario/Materia/"+ codigoCaderno);
        }

        [HttpPost]
        public IActionResult ResumoFlashcard(int qntdCartoes)
        {
            List<string> listaFrente = new List<string>();
            List<string> listaVerso = new List<string>();

            for (int i = 0; i < qntdCartoes; i++)
            {
                string frente = Request.Form["frente-" + (i + 1).ToString()];
                string verso = Request.Form["verso-" + (i + 1).ToString()];

                if(frente != null && verso != null)
                {
                    listaFrente.Add(frente);
                    listaVerso.Add(verso);
                }
            }

            if(listaFrente != null && listaVerso != null)
            {
                //Pegando o id do caderno que o resumo será salvo
                Caderno caderno = JsonConvert.DeserializeObject<Caderno>(HttpContext.Session.GetString("cadernoAcessado"));
                int codigoCaderno = caderno.Codigo;
                string titulo = HttpContext.Session.GetString("tituloResumo");

                Flashcard flsCard = new Flashcard(listaFrente, listaVerso, codigoCaderno, titulo);
               string cadastro = flsCard.cadastrar();
                Redirect("../Usuario/Materia/" + codigoCaderno);
            }

            return View();
        }

        //update

        //delete
        public IActionResult deletarResumo(int id)
        {
            //Pegando o id do caderno que o resumo será salvo
            Caderno caderno = JsonConvert.DeserializeObject<Caderno>(HttpContext.Session.GetString("cadernoAcessado"));
            int codigoCaderno = caderno.Codigo;

            TempData["Situacao_deletar_resumo"] = Resumo.deletarResumo(id);
            string redirecionamento = "~/Usuario/Materia/" + codigoCaderno;
            return Redirect(redirecionamento);
        }

    }
}


