using app.proyectKevinBarre.accessData.Context;
using app.proyectKevinBarre.entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.proyectKevinBarre.accessData.repositories
{
    public class ProductoRepository : CrudGenericService<Producto>, IProductoRepository
    {
        public ProductoRepository(AppDbContext context) : base(context) 
        {
        }


        public async Task<Producto> CreateEntidad(Producto entity)
        {
            return await InsertEntity(entity);
        }

        public async Task DeleteEntidad(int id)
        {
            await DeleteEntity(id);
        }

        public async Task<Producto> GetEntidad(int id)
        {
            return await SelectEntity(id);
        }

        public async Task<List<Producto>> ObtenerEntidadesLista()
        {
            return await SelectEntitiesAll();
        }

        public async Task UpdateEntidad(Producto entity)
        {
            await UpdateEntity(entity);
        }

    }
}
