using DesafioStone.Domain.Models;

namespace DesafioStone.Domain.ViewModel
{
    public class DepositoViewModel
    {
        public decimal Valor { get; set; }
        public ContaBancaria Conta { get; set; }
    }
}
