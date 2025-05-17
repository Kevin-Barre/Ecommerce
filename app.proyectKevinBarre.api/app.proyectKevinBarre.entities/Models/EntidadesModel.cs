using System.ComponentModel.DataAnnotations;

namespace app.proyectKevinBarre.entities.Models
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
        public bool Estado { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class Categoria : EntityBase
    {
        [Required]
        [StringLength(30)]
        public string? Nombre { get; set; }

        [StringLength(30)]
        public string? Descripcion { get; set; }
    }

    public class Cliente : EntityBase
    {

        [Required]
        [StringLength(30)]
        public string? Nombre { get; set; }

        [Required]
        [StringLength(30)]
        public string? Apellido { get; set; }

        [Required]
        [StringLength(50)]
        public string? Email { get; set; }

        public DateTime FechaNacimiento { get; set; }

        [Required]
        [StringLength(10)]
        public string? CedulaIdentidad { get; set; }

    }


    public class Producto : EntityBase
    {
        [Required]
        [StringLength(50)]
        public string? Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string? Descripcion { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        public Categoria? Categoria { get; set; }

        public decimal PrecioUnitario { get; set; }
    }

    public class Venta : EntityBase
    {
        public int ClienteId { get; set; }

        public Cliente? Cliente { get; set; }

        public DateTime FechaVenta { get; set; }

        public string? NumeroFactura { get; set; }

        public string? MetodoPago { get; set; }

        public decimal TotalVenta { get; set; }
    }

    public class VentaDetalle : EntityBase
    {
        public int? VentaId { get; set; }

        public Venta? Venta { get; set; }

        public int NumeroItem { get; set; }

        public int ProductoId { get; set; }

        public Producto? Producto { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Cantidad { get; set; }

        public decimal Total { get; set; }
    }

    public class Usuario : EntityBase
    {
        [Required]
        [StringLength(30)]
        public string? Correo { get; set; }

        [Required]
        [StringLength(30)]
        public string? Clave { get; set; }
    }
}
