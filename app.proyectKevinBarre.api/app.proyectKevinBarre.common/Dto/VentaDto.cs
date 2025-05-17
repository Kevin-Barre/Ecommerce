using app.proyectKevinBarre.entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.proyectKevinBarre.common.Dto
{
    public class VentaDto
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        public DateTime FechaVenta { get; set; }

        public string? NumeroFactura { get; set; }

        public string? MetodoPago { get; set; }

        public decimal TotalVenta { get; set; }
    }
}
