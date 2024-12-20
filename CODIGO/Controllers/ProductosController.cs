using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Ejercicio2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ejercicio2.AccesoDatos;

namespace Ejercicio2.Controllers
{
    //[Route("[Productos]")]
    public class ProductosController : Controller
    {
        private readonly ILogger<ProductosController> _logger;

        public ProductosController(ILogger<ProductosController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ConexionBD conexion = new ConexionBD();

            List<Productos> LisProdu = new List<Productos>();
            LisProdu = conexion.OntenerTodo();
            return View(LisProdu);
        }

        public IActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Nuevo(Productos mod)
        {
            ConexionBD cn = new ConexionBD();
            cn.Insertar(mod);
            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(int id)
        {
            ConexionBD cn = new ConexionBD();
            List<Productos> modelo = new List<Productos>();
            modelo = cn.ObtenerPorId(id);
            return View(modelo[0]);
        }
        [HttpPost, ActionName("Eliminar")]
        public IActionResult EliminarId(int Id)

        {
            ConexionBD cn = new ConexionBD();
            bool Eliminar = cn.Eliminar(Id);

            return RedirectToAction("Index");
        }

        public IActionResult Detalles(int Id)
        {
            ConexionBD cn = new ConexionBD();
            List<Productos> lismodelo = new List<Productos>();
            lismodelo = cn.ObtenerPorId(Id);
            return View(lismodelo[0]);
        }
   public IActionResult Actualizar(int Id)
   {
             ConexionBD cn = new ConexionBD();
            List<Productos> listmodelo = new List<Productos>();
            listmodelo = cn.ObtenerPorId(Id);
            return View(listmodelo[0]);
   }

   [HttpPost]
public IActionResult Actualizar(Productos mod)
{
    ConexionBD cn = new ConexionBD();
    bool Insertar = cn.Actualizar(mod);
    return RedirectToAction("Index");
}
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}