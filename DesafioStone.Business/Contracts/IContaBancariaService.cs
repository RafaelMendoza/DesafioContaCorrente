using DesafioStone.Domain.Models;
using System;
using System.Threading.Tasks;

namespace DesafioStone.Business.Contracts
{
    public interface IContaBancariaService
    {
        public Task<(bool, string)> RealizarSaque(int contaId, decimal valor);
        public Task<(bool, string)> RealizarDeposito(int contaId, decimal valor);
        public Task<Extrato> ObterExtrato(int contaId, DateTime dataInicio, DateTime dataFinal);
    }
}
