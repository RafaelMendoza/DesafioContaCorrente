using DesafioStone.Domain.Models.Base;

namespace DesafioStone.Domain.Models
{
    public class ContaBancaria : Entity
    {
        public decimal Saldo { get; private set; }


        public ContaBancaria()
        {

        }

        public ContaBancaria(decimal saldoInicial)
        {
            Saldo = saldoInicial;
        }

        public void RemoverFundos(decimal valor)
        {
            Saldo -= valor;
        }

        public void AdicionarFundos(decimal valor)
        {
            this.Saldo += valor ;
        }

        public bool ValorDeSaqueMaiorQueSaldo(decimal valor)
        {
            return this.Saldo - valor < 0;
        }

        public bool ValorDeSaqueMaisTaxaMaiorQueSaldo(decimal valor, decimal taxa)
        {
            return this.Saldo - (valor + taxa) < 0;
        }
    }
}
