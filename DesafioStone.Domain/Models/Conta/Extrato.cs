using System;
using System.Collections.Generic;

namespace DesafioStone.Domain.Models
{
    public class Extrato
    {
        public ContaBancaria Conta { get; set; }
        public DateTime DataExtracao { get; set; }
        public List<Transacao> Transacoes { get; set; }
    }
}
