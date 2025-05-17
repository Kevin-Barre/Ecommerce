using app.proyectKevinBarre.entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.proyectKevinBarre.accessData.repositories
{
    public interface IVentaRepository
    {
        Task<Venta> GetEntidad(int id);

        Task<Venta> CreateEntidad(Venta entity);

        Task<List<Venta>> ObtenerEntidadesLista();

        Task UpdateEntidad(Venta entity);

        Task DeleteEntidad(int id);
    }
}
