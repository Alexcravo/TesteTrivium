using Microsoft.EntityFrameworkCore;
using TesteTrivium.Entities;

namespace TesteTrivium.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Compra> Compra { get; set; }
        public DbSet<CompraItem> CompraItem { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=TesteTrivium;Data Source=LAPTOP-TFG9GU34");
        }
    }
}
