using app.proyectKevinBarre.entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_NetCore.Dto.Request
{
    public class ProductoRequest
    {
        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        public string? Nombre { get; set; }

        [StringLength(50, ErrorMessage = "El ancho del campo es muy largo")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El campo CategoriaId es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El campo CategoriaId debe ser un número positivo.")]
        public int CategoriaId { get; set; }

        public Categoria? Categoria { get; set; }

        [Required(ErrorMessage = "El campo PrecioUnitario es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio unitario debe ser mayor que cero.")]
        public decimal PrecioUnitario { get; set; }
    }
}
