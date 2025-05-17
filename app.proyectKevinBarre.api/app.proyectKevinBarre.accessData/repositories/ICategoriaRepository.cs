
using app.proyectKevinBarre.entities.Models;

namespace app.proyectKevinBarre.accessData.repositories
{
    public interface ICategoriaRepository
    {
        Task<Categoria> GetEntidad(int id);

        Task<Categoria> CreateEntidad(Categoria entity);

        Task<List<Categoria>> ObtenerEntidadesLista();

        Task UpdateEntidad(Categoria entity);

        Task DeleteEntidad(int id);
    }
}
