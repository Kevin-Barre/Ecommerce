using app.proyectKevinBarre.accessData.Context;
using app.proyectKevinBarre.entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.proyectKevinBarre.accessData.repositories
{
    public class VentaDetalleRepository : CrudGenericService<VentaDetalle>, IVentaDetalleRepository
    {
        public VentaDetalleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<VentaDetalle> CreateEntidad(VentaDetalle entity)
        {
            return await InsertEntity(entity);
        }

        public async Task DeleteEntidad(int id)
        {
            await DeleteEntity(id);
        }

        public async Task<VentaDetalle> GetEntidad(int id)
        {
            return await SelectEntity(id);
        }

        public async Task<List<VentaDetalle>> ObtenerEntidadesLista()
        {
            return await SelectEntitiesAll();
        }

        public async Task UpdateEntidad(VentaDetalle entity)
        {
            await UpdateEntity(entity);
        }
    }
}
