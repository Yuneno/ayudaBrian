using ArchivosTxt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArchivosTxt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuardarTxTController : ControllerBase
    {

        [HttpPost]
        public IActionResult GuardarRegistros(List<Registro> registros)
        {
            return Ok(registros);
        }
    }
}
