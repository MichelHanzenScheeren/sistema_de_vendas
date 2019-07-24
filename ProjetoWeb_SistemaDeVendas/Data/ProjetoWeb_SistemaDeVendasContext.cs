using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjetoWeb_SistemaDeVendas.Models
{
    public class ProjetoWeb_SistemaDeVendasContext : DbContext
    {
        public ProjetoWeb_SistemaDeVendasContext (DbContextOptions<ProjetoWeb_SistemaDeVendasContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }
    }
}
