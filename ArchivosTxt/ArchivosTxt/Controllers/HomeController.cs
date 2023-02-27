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
            var lineaList = line.Split(" ").ToList();
            List<string> list = new List<string>();
            var registro = new Registro();

            foreach (var linea in lineaList)
            {

                if (linea != "")
                {
                    if (list.Count() == 0)
                    {
                        string cedula = linea.Substring(0, 11);
                        string nombre = linea.Substring(11);

                        int currentNombre = 0;
                        string PrimerNombre = "";
                        string SegundoNombre = "";

                        for (int i = 0; i < nombre.Length; i++)
                        {
                            if (nombre[i].ToString() == nombre[i].ToString().ToUpper() && i == 0)
                            {
                                currentNombre = 1;
                            }

                            if (nombre[i].ToString() == nombre[i].ToString().ToUpper() && i > 0)
                            {
                                currentNombre = 2;
                            }

                            if (currentNombre == 1)
                            {
                                PrimerNombre += nombre[i];
                            }
                            else
                            {
                                SegundoNombre += nombre[i];
                            }

                        }

                        list.Add(cedula);
                        list.Add(PrimerNombre + " " + SegundoNombre);
                    }
                    else
                    {
                        list.Add(linea);
                    }
                }

            };

            string monto, fecha, sub;
            sub = list[3].Substring(0, 4);
            monto = list[3].Substring(5, 9);
            fecha = list[3].Substring(10);

            registro.Cedula = list[0];
            registro.Apellido = list[1];
            registro.Nombre = list[2];
            registro.Sub = sub;
            registro.Monto = monto.Substring(3);
            registro.Fecha = fecha.Substring(3);

            return registro;
        }

    }
}