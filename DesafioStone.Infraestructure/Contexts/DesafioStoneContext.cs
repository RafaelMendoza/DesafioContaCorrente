using DesafioStone.Infraestructure.Configs;
using Microsoft.EntityFrameworkCore;

namespace DesafioStone.Infraestructure.Contexts
{
    public class DesafioStoneContext : DbContext
    {
        public DesafioStoneContext(DbContextOptions<DesafioStoneContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TransacaoConfig());
            modelBuilder.ApplyConfiguration(new ContaBancariaConfig());
        }
    }
}
