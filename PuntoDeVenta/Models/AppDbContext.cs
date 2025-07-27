using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity; 

namespace PuntoDeVenta.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(): base("AppDbContext")
        {

        }
        public DbSet<Cliente> Clientes { get; set; }
         public DbSet<Producto> Productos { get; set; }
    }
}
