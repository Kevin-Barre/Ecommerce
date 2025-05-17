using app.proyectKevinBarre.accessData.Context;
using app.proyectKevinBarre.entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.proyectKevinBarre.accessData.repositories
{
    public class VentaRepository : CrudGenericService<Venta>, IVentaRepository
    {
        public VentaRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Venta> CreateEntidad(Venta entity)
        {
            return await InsertEntity(entity);
        }

        public async Task DeleteEntidad(int id)
        {
            await DeleteEntity(id);
        }

        public async Task<Venta> GetEntidad(int id)
        {
            return await SelectEntity(id);
        }

        public async Task<List<Venta>> ObtenerEntidadesLista()
        {
            return await SelectEntitiesAll();
        }

        public async Task UpdateEntidad(Venta entity)
        {
            await UpdateEntity(entity);
        }
    }
}
