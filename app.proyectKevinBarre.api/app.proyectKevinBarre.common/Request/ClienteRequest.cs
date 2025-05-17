using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_NetCore.Dto.Request
{
    public class ClienteRequest
    {

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Nombre { get; set; } 
        
        [Required(ErrorMessage = "El campo Apellido es obligatorio")]
        public string Apellido { get; set; }

        [StringLength(50, ErrorMessage = "El ancho del campo es muy largo")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo de la fecha de nacimiento es obligatorio")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El campo Cedula de Identidad es obligatorio")]
        public string CedulaIdentidad { get; set; }
    }
}
