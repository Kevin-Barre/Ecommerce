using app.FacturaSubscribe.Entities.Models;

namespace app.FacturaSubscribe.services.Interfaces
{
    public interface IProcesoService
    {
        Task GuardarCategoriaAsync(Categoria categoria);

        Task GuardarVentaDetalleAsync (VentaDetalle ventaDetalle);
    }
}
