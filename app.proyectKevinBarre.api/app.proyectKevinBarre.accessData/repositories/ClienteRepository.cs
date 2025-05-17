using app.proyectKevinBarre.accessData.Context;
using app.proyectKevinBarre.entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.proyectKevinBarre.accessData.repositories
{
    public class ClienteRepository : CrudGenericService<Cliente>, IClienteRepository
    {
        public ClienteRepository(AppDbContext context) : base(context) 
        { 
        }

        public async Task<Cliente> CreateEntidad(Cliente entity)
        {
           return await InsertEntity(entity);
        }

        public async Task DeleteEntidad(int id)
        {
            await DeleteEntity(id);
        }

        public async Task<Cliente> GetEntidad(int id)
        {
            return await SelectEntity(id);
        }

        public async Task<List<Cliente>> ObtenerEntidadesLista()
        {
            return await SelectEntitiesAll(); 
        }

        public async Task UpdateEntidad(Cliente entity)
        {
            await UpdateEntity(entity);
        }
    }
}
