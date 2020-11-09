using DesafioStone.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioStone.Infraestructure.Configs
{
    public class ContaBancariaConfig : IEntityTypeConfiguration<ContaBancaria>
    {
        public void Configure(EntityTypeBuilder<ContaBancaria> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Saldo);
        }
    }
}
