
using app.proyectKevinBarre.entities.Models;

namespace app.proyectKevinBarre.accessData.repositories
{
    public interface IClienteRepository
    {
        Task<Cliente> GetEntidad(int id);

        Task<Cliente> CreateEntidad(Cliente entity);

        Task<List<Cliente>> ObtenerEntidadesLista();

        Task UpdateEntidad(Cliente entity);

        Task DeleteEntidad(int id);
    }
}
