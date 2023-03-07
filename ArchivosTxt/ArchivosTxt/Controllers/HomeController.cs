using ArchivosTxt.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using System.IO;

namespace ArchivosTxt.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
            
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormFile Archivo)
        {
            List<Registro> Registros = new();
            if (Archivo != null)
            {
                using (StreamReader reader = new StreamReader(Archivo.OpenReadStream()))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Registros.Add(GetRegistro(line));

                    }
                }
            }
            return View(Registros);
        }

        [HttpPost]
        public IActionResult GetPersonList(IFormFile ArchivoTxT)
        {
            List<Registro> Registros = new();
            if (ArchivoTxT != null)
            {
                using (StreamReader reader = new StreamReader(ArchivoTxT.OpenReadStream()))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Registros.Add(GetRegistro(line));

                    }
                }
            }

            return Ok(Registros);
        }

        [HttpPost]
        public IActionResult GuardarRegistros([FromBody]List<Registro> registros)
        {
            return Ok(registros);
        }

        public IActionResult Privacy()
        {
            return View();
        }



        private Registro GetRegistro(string line)
        {
            var registro = new Registro
            {
                Cedula = line.Substring(0, 11),
                Apellido = line.Substring(11, 30),
                Nombre = line.Substring(41, 30),
                Sub = line.Substring(71, 4),
                Monto = line.Substring(75, 9),
                Fecha = line.Substring(84)
            };

            return registro;
        }

    }
}