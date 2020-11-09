using DesafioStone.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioStone.Infraestructure.Configs
{
    public class TransacaoConfig : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ContaBancariaId);
            builder.Property(x => x.DataTransacao);
            builder.Property(x => x.TipoTransacao);
            builder.Property(x => x.Valor);
            builder.Property(x => x.Saldo);

            //builder.HasOne(x => x.ContaBancaria);
        }
    }
}
