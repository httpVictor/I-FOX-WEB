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
            //gerando a data atual do dia
            var anoData = DateTime.Now.Year;
            var mesData = string.Format("{0:MM}", DateTime.Now);
            var diaData = string.Format("{0:dd}", DateTime.Now);
            string dataAtual = anoData + "-" + mesData + "-" + diaData;

            ViewBag.DataAtual = dataAtual;

            ViewBag.Id = id;
            return View();
        }
        public IActionResult ResumoEscrito()
        {
            //gerando a data atual do dia
            var anoData = DateTime.Now.Year;
            var mesData = string.Format("{0:MM}", DateTime.Now);
            var diaData = DateTime.Now.Day;
            string dataAtual = anoData + "-" + mesData + "-" + diaData;
            
            ViewBag.DataAtual = dataAtual;
            return View();
        }

        public IActionResult ResumoFoto(string id)
        {
            ViewBag.Id_foto = id;
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

        public IActionResult VisualizarFlashcard(int id)
        {
            ViewBag.Id_Card = id;
            return View(Flashcard.listar(id));
        }

        public IActionResult RevisaoFlashcard(int id)
        {
            ViewBag.Id_cards = id;
            return View(Flashcard.listar(id));
        }

        public IActionResult EditarFlash(int id)
        {
            return View(Flashcard.listar(id));
        }







        //MÉTODOS QUE VÃO RELACIONAR MODELO E TELA

        //Cadastro
        [HttpPost]
        public IActionResult CadastrarResumo(string id, string titulo, string data)
        {
            //Validando o tamanho do titulo
            if(titulo.Length > 0 && titulo.Length < 80)
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
                return Redirect("../Resumo" + id);
            }
            else
            {
                ViewBag.Info = "Não utilize títulos maiores que 80 caracteres";
                return View();
            }
           
        }
        
        [HttpPost]
        public IActionResult ResumoFotos(string id)
        {
            //Pegando o id do caderno que o resumo será salvo
            Caderno caderno = JsonConvert.DeserializeObject<Caderno>(HttpContext.Session.GetString("cadernoAcessado"));
            int codigoCaderno = caderno.Codigo;

            int id_ofc = int.Parse(id);

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
                    string titulo = HttpContext.Session.GetString("tituloResumo");

                    if (titulo != null || titulo != null)
                    {
                       int codigoResumo = Arquivo.buscarResumo(titulo, codigoCaderno);
                       id_ofc = codigoResumo;
                    }
                    
                    Arquivo arq = new Arquivo(bytesDoArquivo, titulo, codigoCaderno, id_ofc);
                    arq.cadastrar();
                    
                    TempData["msg"] = "Salvo com sucesso!";
                }
                else
                {
                    TempData["msg"] = "Erro de cadastro, coloque apenas imagens";
                }
            }

            string redirecionamento = "~/Usuario/Materia/" + codigoCaderno;
            return Redirect(redirecionamento);
        }

        [HttpPost]
        public IActionResult ResumoAudio(string id)
        {
            //Pegando o id do caderno que o resumo será salvo
            Caderno caderno = JsonConvert.DeserializeObject<Caderno>(HttpContext.Session.GetString("cadernoAcessado"));
            int codigoCaderno = caderno.Codigo;

            int id_ofc = int.Parse(id);

            //Procurando arquivos...
            foreach (IFormFile arquivo in Request.Form.Files)
            {
                string tipoArquivo = arquivo.ContentType;

                //se for imagem...
                if (tipoArquivo.Contains("audio"))
                {

                    MemoryStream s = new MemoryStream();
                    arquivo.CopyTo(s);
                    byte[] bytesDoArquivo = s.ToArray(); //transformar em cadeia de byte
                    string titulo = HttpContext.Session.GetString("tituloResumo");

                    if (titulo != null || titulo != null)
                    {
                        int codigoResumo = Arquivo.buscarResumo(titulo, codigoCaderno);
                        id_ofc = codigoResumo;
                    }

                    Arquivo arq = new Arquivo(bytesDoArquivo, titulo, codigoCaderno, id_ofc);
                    arq.cadastrar();

                    TempData["msg"] = "Salvo com sucesso!";
                }
                else
                {
                    TempData["msg"] = "Erro de cadastro, coloque apenas imagens";
                }
            }

            return Redirect("~/Resumo/ResumoFotos/" + id);
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

                Flashcard flsCard = new Flashcard(null, listaFrente, listaVerso, codigoCaderno, titulo);
                string cadastro = flsCard.cadastrar();
                return Redirect("../Usuario/Materia/" + codigoCaderno);
            }

            return View();
        }






        //update
        [HttpPost]
        public IActionResult VisualizarEscrito(string titulo, string texto, int id)
        {
            string sitUpdate = Resumo.editarResumo(titulo, texto, id);
            //Pegando o id do caderno que o resumo será salvo
            Caderno caderno = JsonConvert.DeserializeObject<Caderno>(HttpContext.Session.GetString("cadernoAcessado"));
            int codigoCaderno = caderno.Codigo;
            return Redirect("~/Usuario/Materia/" + codigoCaderno);
        }

        [HttpPost]
        public IActionResult EditarFlashcard(int id)
        {
            //Pegando o id do caderno que o resumo será salvo
            Caderno caderno = JsonConvert.DeserializeObject<Caderno>(HttpContext.Session.GetString("cadernoAcessado"));
            int codigoCaderno = caderno.Codigo;

            List<string> listaFrente = new List<string>();
            List<string> listaVerso = new List<string>();
            List<int> listaCodigo = new List<int>();

            //Atualizando os cartões já existentes no banco
            for (int i = 0; i < id; i++)
            {
                string frente = Request.Form["frente-" + (i + 1).ToString()];
                string verso = Request.Form["verso-" + (i + 1).ToString()];
                int codigo = int.Parse(Request.Form["codigo-" + (i + 1).ToString()]);

                if (frente != null && verso != null)
                {
                    listaFrente.Add(frente);
                    listaVerso.Add(verso);
                    listaCodigo.Add(codigo);
                }
                
            }
            string sit = Flashcard.editar(listaFrente, listaVerso, listaCodigo);

            //Cadastrando os cards que não estavam na lista
            List<string> listaFrenteNovos = new List<string>();
            List<string> listaVersoNovos = new List<string>();

            for (int i = 0; i < 0; i++)
            {
                string frente = Request.Form["frente-" + (i + 1).ToString()];
                string verso = Request.Form["verso-" + (i + 1).ToString()];

                if (frente != null && verso != null)
                {
                    listaFrenteNovos.Add(frente);
                    listaVersoNovos.Add(verso);
                }
            }

            if (listaFrenteNovos != null && listaVersoNovos != null)
            {
                //Pegando o id do caderno que o resumo será salvo
                string titulo = HttpContext.Session.GetString("tituloResumo");

                Flashcard flsCard = new Flashcard(null, listaFrenteNovos, listaVersoNovos, codigoCaderno, titulo);
                string cadastro = flsCard.cadastrar();
            }


            //Devolvendo na página anterior
            return Redirect("~/Usuario/Materia/" + codigoCaderno);

        }




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

        public IActionResult DeletarArquivo(int id)
        {
            //Pegando o id do caderno que o resumo será salvo
            Caderno caderno = JsonConvert.DeserializeObject<Caderno>(HttpContext.Session.GetString("cadernoAcessado"));
            int codigoCaderno = caderno.Codigo;

            TempData["Situacao_deletar_resumo"] = Arquivo.deletarAquivo(id);
            string redirecionamento = "~/Usuario/Materia/" + codigoCaderno;
            return Redirect(redirecionamento);
        }

        public IActionResult ExcluirCartao(int id)
        {
            Flashcard.deletarCartao(id);
            return Redirect("../Resumos/EditarFlash/" + 38);
        }
    }
}


