using app.FacturaSubscribe.DataAccess.Context;
using app.FacturaSubscribe.Entities.Models;
using app.FacturaSubscribe.services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace app.FacturaSubscribe.services.Implementacion
{
    public class ProcesoService : IProcesoService
    {
        private readonly AppDbContext _context;

        public ProcesoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task GuardarCategoriaAsync(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.Entry(categoria).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }
        
        public async Task GuardarVentaDetalleAsync(VentaDetalle ventaDetalle)
        {
            _context.VentaDetalles.Add(ventaDetalle);
            _context.Entry(ventaDetalle).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

    }
}
