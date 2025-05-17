using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.FacturaSubscribe.Entities.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool Estado { get; set; }
        public DateTime Fecha { get; set; }
    }



    public class VentaDetalle 
    {
        public int Id { get; set; }
        public int? VentaId { get; set; }
        public int? NumeroItem { get; set; }
        public int ProductoId { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Total { get; set; }
        public bool Estado { get; set; }
        public DateTime Fecha { get; set; }
    }
}
