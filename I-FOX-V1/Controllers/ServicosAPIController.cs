using I_FOX_V1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace I_FOX_V1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicosAPIController : ControllerBase
    {

        //SERVIÇOS RELACIONADOS AO USUÁRIO
        [HttpPost]
        public IActionResult CadastrarUsuario(string jsonValor)
        {
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(jsonValor);
            return Content(usuario.cadastrarUsuario());
        }

        [HttpPost]
        public IActionResult LogarUsuario(string jsonValor)
        {
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(jsonValor);
            return Content(usuario.logarUsuario());
        }

        //SERVIÇOS RELACIONADOS AO CADERNO
        [HttpPost]
        public IActionResult CadastrarCaderno(string jsonValor)
        {
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(jsonValor);
            return Content(usuario.logarUsuario());
        }

        [HttpGet]
        public IActionResult ListarCaderno(string nomeCaderno)
        {
            return new JsonResult(JsonConvert.SerializeObject(Caderno.listarCaderno(nomeCaderno)));
        }

    }
}
