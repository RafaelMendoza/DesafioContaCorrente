using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DesafioStone.WebClient.Models;
using DesafioStone.WebApi.Infraestructure.Services;
using DesafioStone.Domain.ViewModel;
using DesafioStone.Domain.Models;

namespace DesafioStone.WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApiService _apiService;

        public HomeController(ILogger<HomeController> logger, ApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RealizarSaque(decimal valorSaque)
        {
            try
            {

                //Para efeito de demonstração, deixei fixo o Id da Conta que será usado na API
                var res = await _apiService.RealizarSaque(1, new SaqueViewModel { Conta = new ContaBancaria { Id = 1 }, Valor = valorSaque });
                if(res.Item1 == true)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(res.Item2);
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao Realizar Saque");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> RealizarDeposito(decimal valorDeposito)
        {
            try
            {
                //Para efeito de demonstração, deixei fixo o Id da Conta que será usado na API
                var res = await _apiService.RealizarDeposito(1, new DepositoViewModel { Conta = new ContaBancaria { Id = 1 }, Valor = valorDeposito });

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao Realizar Depósito");
                return BadRequest("Houve um erro ao realizar o Deposito. Caso persista, por favor, contate o administrador");
            }
        }

        public async Task<IActionResult> _ExtratoPartialView()
        {
            return PartialView("_ExtratoPartialView", new Extrato {Transacoes = new List<Transacao>() });
        }

        [HttpGet]
        public async Task<IActionResult> ObterExtrato(DateTime dataInicial, DateTime dataFinal)
        {
            try
            {
                //Para efeito de demonstração, deixei fixo o Id da Conta que será usado na API
                var extrato = await _apiService.ObterExtrato(1, dataInicial, dataFinal);

                return PartialView("_ExtratoPartialView", extrato);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao Realizar Obter Extrato");
                return BadRequest("Houve um erro ao obter o Extrato. Caso persista, por favor, contate o administrador");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
