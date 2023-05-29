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

        [HttpGet]
        [Route("api/cards/listar-cartoes")]
        public List<Flashcard> listaCartoes(int cod_resumo)
        {
            return Flashcard.listar(cod_resumo);
        }

    }
}
