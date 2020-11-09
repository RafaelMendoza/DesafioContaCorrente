using DesafioStone.Business.Contracts;
using DesafioStone.Domain.Models;
using DesafioStone.Infraestructure.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioStone.Business.Conta
{
    public class TransacaoService : ITransacaoService
    {
        private readonly IRepository _repo;
        private readonly ILogger<TransacaoService> _logger;

        public TransacaoService(IRepository repo, ILogger<TransacaoService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task AdicionarTransacao(Transacao transacao)
        {
            try
            {
                await _repo.CreateAsync(transacao);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao registrar Transacao para conta {transacao.Id} em {DateTime.Now}");
                throw;
            }
        }

        public async Task<List<Transacao>> ObterTransacoes(int contaId, DateTime dataInicio, DateTime dataFinal)
        {
            try
            {
                var transacoes = await _repo.GetAsync<Transacao>(x => x.DataTransacao > dataInicio && x.DataTransacao <= dataFinal);
                return transacoes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao Lista de Transacões para conta {contaId} em {DateTime.Now}");
                throw;
            }
        }
    }
}
