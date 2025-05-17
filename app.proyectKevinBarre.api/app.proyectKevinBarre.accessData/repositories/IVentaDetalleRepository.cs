using app.proyectKevinBarre.entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.proyectKevinBarre.accessData.repositories
{
    public interface IVentaDetalleRepository
    {
        Task<VentaDetalle> GetEntidad(int id);

        Task<VentaDetalle> CreateEntidad(VentaDetalle entity);

        Task<List<VentaDetalle>> ObtenerEntidadesLista();

        Task UpdateEntidad(VentaDetalle entity);

        Task DeleteEntidad(int id);
    }
}

