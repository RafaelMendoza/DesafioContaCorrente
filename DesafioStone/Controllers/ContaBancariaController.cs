using System;
using System.Threading.Tasks;
using DesafioStone.Business.Contracts;
using DesafioStone.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DesafioStone.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ContaBancariaController : ControllerBase
    {

        private readonly ILogger<ContaBancariaController> _logger;
        private readonly IContaBancariaService _contaBancariaService;

        public ContaBancariaController(ILogger<ContaBancariaController> logger, IContaBancariaService contaBancariaService)
        {
            _logger = logger;
            _contaBancariaService = contaBancariaService;
        }

        [HttpPost]
        public async Task<IActionResult> RealizarDeposito([FromQuery]int contaId, [FromBody]DepositoViewModel deposito)
        {
            try
            {
                var resultado = await _contaBancariaService.RealizarDeposito(contaId, deposito.Valor);

                if(resultado.Item1)
                {
                    return Ok("Deposito realizado com sucesso!");
                }
                else
                {
                    return BadRequest(resultado.Item2);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"DepositoController - Erro ao realizar Deposito na conta {contaId} em {DateTime.Now}");
                return BadRequest("Erro ao obter o extrato. Caso o erro persista, contate o Administrador");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RealizarSaque([FromQuery] int contaId, [FromBody] SaqueViewModel saque)
        {
            try
            {
                var resultado = await _contaBancariaService.RealizarSaque(contaId, saque.Valor);

                if (resultado.Item1)
                {
                    return Ok("Saque realizado com sucesso!");
                }
                else
                {
                    return BadRequest(resultado.Item2);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"SaqueController - Erro ao realizar Saque na conta {contaId} em {DateTime.Now}");
                return BadRequest("Erro ao obter o extrato. Caso o erro persista, contate o Administrador");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObterExtrato(int contaId, DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                var extrato = await _contaBancariaService.ObterExtrato(contaId, dataInicio, dataFim.AddHours(24));

                if(extrato != null)
                {
                    return Ok(extrato);
                }
                else
                {
                    return BadRequest("Houve um erro ao obter o extrato. Por favor verifique os dados fornecidos");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ExtratoController - Erro ao obter Extrato na conta {contaId} em {DateTime.Now}");
                return BadRequest("Erro ao obter o extrato. Caso o erro persista, contate o Administrador");
            }
        }
    }
}
