using app.proyectKevinBarre.entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.proyectKevinBarre.accessData.repositories
{
    public interface IProductoRepository
    {
        Task<Producto> GetEntidad(int id);

        Task<Producto> CreateEntidad(Producto entity);

        Task<List<Producto>> ObtenerEntidadesLista();

        Task UpdateEntidad(Producto entity);

        Task DeleteEntidad(int id);
    }
}
