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

        [HttpGet("listar-cartoes")] //api/ServicosApi/listar-cartoes
       
        public IActionResult listaCartoes(int cod_resumo)
        {
            return new JsonResult(
                JsonConvert.SerializeObject(Flashcard.listar(cod_resumo))
                ); //serializar uma lista, objeto, para a outra aplicação entender
        }

    }
}
