using DesafioStone.Business.Conta.Constants;
using DesafioStone.Business.Contracts;
using DesafioStone.Domain.Models;
using DesafioStone.Domain.Models.Conta.Enums;
using DesafioStone.Infraestructure.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioStone.Business.Conta
{
    /// <summary>
    /// Classe de Serviço que representa as operações com a Conta Bancária
    /// </summary>
    public class ContaBancariaService : IContaBancariaService
    {
        private readonly IRepository _repo;
        private readonly ITransacaoService _transacaoService;
        private readonly ILogger<ContaBancariaService> _logger;

        public ContaBancariaService(IRepository repo, ITransacaoService transacaoService, ILogger<ContaBancariaService> logger)
        {
            _repo = repo;
            _transacaoService = transacaoService;
            _logger = logger;
        }

        public async Task<(bool, string)> RealizarSaque(int contaId, decimal valor)
        {
            try
            {
                var conta = await _repo.GetOneAsync<ContaBancaria>(x => x.Id == contaId);

                if(conta == null)
                {
                    return (false, "Conta não encontrada");
                }

                if (conta.ValorDeSaqueMaiorQueSaldo(valor))
                    return (false, "O Valor do Saque é maior que o Saldo disponível!");

                if (conta.ValorDeSaqueMaisTaxaMaiorQueSaldo(valor, ConfigContaBancaria.TAXA_SAQUE))
                    return (false, "O Valor do Saque é maior que o Saldo disponível!");

                conta.RemoverFundos(valor);
                await _repo.UpdateAsync(conta);

                await _transacaoService.AdicionarTransacao(new Transacao
{
                    ContaBancariaId = conta.Id,
                    TipoTransacao = TipoTransacao.Saque,
                    Saldo = conta.Saldo,
                    DataTransacao = DateTime.Now,
                    Valor = valor
                });

                conta.RemoverFundos(ConfigContaBancaria.TAXA_SAQUE);

                await _transacaoService.AdicionarTransacao(new Transacao
                {
                    ContaBancariaId = conta.Id,
                    TipoTransacao = TipoTransacao.TaxaSaque,
                    Saldo = conta.Saldo,
                    DataTransacao = DateTime.Now,
                    Valor = ConfigContaBancaria.TAXA_SAQUE
                });

                return (true, null);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao realizar Saque na conta {contaId} em {DateTime.Now}");
                throw;
            }
        }

        public async Task<(bool, string)> RealizarDeposito(int contaId, decimal valor)
        {
            try
            {
                var conta = await _repo.GetOneAsync<ContaBancaria>(x => x.Id == contaId);

                if (conta == null)
                {
                    return (false, "Conta não encontrada");
                }

                var valorDeTaxa = (valor * ConfigContaBancaria.TAXA_DEPOSITO);
                var valorAjustado = valor - valorDeTaxa;

                conta.AdicionarFundos(valorAjustado);
                await _repo.UpdateAsync(conta);

                var transacao = new Transacao
                {
                    ContaBancariaId = conta.Id,
                    TipoTransacao = TipoTransacao.Deposito,
                    Saldo = conta.Saldo + valorDeTaxa,
                    DataTransacao = DateTime.Now,
                    Valor = valor
                };

                await _transacaoService.AdicionarTransacao(transacao);

                var transacaoTaxaDeposito = new Transacao
                {
                    ContaBancariaId = conta.Id,
                    TipoTransacao = TipoTransacao.TaxaDeposito,
                    Saldo = conta.Saldo,
                    DataTransacao = DateTime.Now,
                    Valor = valorDeTaxa
                };

                await _transacaoService.AdicionarTransacao(transacaoTaxaDeposito);

                return (true, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao realizar Depósito na conta {contaId} em {DateTime.Now}");
                throw;
            }
        }

        public async Task<Extrato> ObterExtrato(int contaId, DateTime dataInicio, DateTime dataFinal)
        {
            try
            {
                var conta = await _repo.GetOneAsync<ContaBancaria>(x => x.Id == contaId);

                if (conta == null)
                {
                    return null;
                }

                var transacoes = await _transacaoService.ObterTransacoes(conta.Id, dataInicio, dataFinal);
                return new Extrato { Conta = conta, DataExtracao = DateTime.Now, Transacoes = transacoes ?? new List<Transacao>() };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao obter Extrato para conta {contaId} em {DateTime.Now} no período de {dataInicio} à {dataFinal}");
                throw;
            }
        }
    }
}
