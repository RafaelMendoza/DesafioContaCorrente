using DesafioStone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioStone.Business.Contracts
{
    public interface ITransacaoService
    {
        public Task AdicionarTransacao(Transacao transacao);
        public Task<List<Transacao>> ObterTransacoes(int contaId, DateTime dataInicio, DateTime dataFinal);
    }
}
