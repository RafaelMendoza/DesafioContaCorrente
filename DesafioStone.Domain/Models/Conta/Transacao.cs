using DesafioStone.Domain.Models.Base;
using DesafioStone.Domain.Models.Conta.Enums;
using System;

namespace DesafioStone.Domain.Models
{
    public class Transacao : Entity
    {
        public int ContaBancariaId { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public decimal Saldo { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataTransacao { get; set; }
    }
}
