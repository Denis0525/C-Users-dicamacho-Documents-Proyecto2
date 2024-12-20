using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ejercicio2.Models
{
    public class Productos
    {
        public int id_producto { get; set; }
        public string? nombre { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }
        public string? fecha_Registro { get; set; }
        public string? estado { get; set; }
    }
}