
using app.proyectKevinBarre.accessData.Context;
using app.proyectKevinBarre.entities.Models;

namespace app.proyectKevinBarre.accessData.repositories
{
    public class CategoriaRepository : CrudGenericService<Categoria>,  ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<Categoria> CreateEntidad(Categoria entity)
        {
            return await InsertEntity(entity);
        }

        public async Task DeleteEntidad(int id)
        {
            await DeleteEntity(id);
        }

        public async Task<Categoria> GetEntidad(int id)
        {
            return await SelectEntity(id);
        }

        public async Task<List<Categoria>> ObtenerEntidadesLista()
        {
            return await SelectEntitiesAll();
        }

        public async Task UpdateEntidad(Categoria entity)
        {
            await UpdateEntity(entity);
        }
    }
}
