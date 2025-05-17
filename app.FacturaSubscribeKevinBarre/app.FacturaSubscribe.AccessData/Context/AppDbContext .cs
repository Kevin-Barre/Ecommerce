using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.FacturaSubscribe.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace app.FacturaSubscribe.DataAccess.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<VentaDetalle> VentaDetalles { get; set; }

        

    }
}
