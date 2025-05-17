using System.ComponentModel.DataAnnotations;

namespace ECommerce_NetCore.Dto.Request
{
    public class CategoriaRequest
    {
        [Required(ErrorMessage = "El campo Name es obligatorio")]
        public string Nombre { get; set; }

        [StringLength(20, ErrorMessage = "El ancho del campo es muy largo")]
        public string Descripcion { get; set; }
    }
}