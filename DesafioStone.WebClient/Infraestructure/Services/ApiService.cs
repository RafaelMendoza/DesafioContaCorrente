using DesafioStone.Domain.Models;
using DesafioStone.Domain.ViewModel;
using DesafioStone.WebClient.Infraestructure.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DesafioStone.WebApi.Infraestructure.Services
{
    public class ApiService
    {
        public HttpClient Client { get; }

        private readonly ApiConfiguration apiConfiguration;

        public ApiService(HttpClient client, IOptions<ApiConfiguration> apiConfiguration)
        {
            this.apiConfiguration = apiConfiguration.Value;

            client.BaseAddress = new Uri(this.apiConfiguration.EnderecoBaseAPI);
            // GitHub API versioning
            client.DefaultRequestHeaders.Add("Accept",
                "application/json");

            Client = client;
            
        }

        public async Task<(bool, string)> RealizarSaque(int contaId, SaqueViewModel saqueVM)
        {

            var json = new StringContent(
                                JsonConvert.SerializeObject(saqueVM),
                                Encoding.UTF8,
                                "application/json");

            var response = await Client.PostAsync($"/api/ContaBancaria/RealizarSaque?contaId={contaId}", json);

            var responseStream = await response.Content.ReadAsStringAsync();
            var mensagemDeErro = JsonConvert.DeserializeObject<string>(responseStream);
            
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return (true, null);
            }
            else
            {
                return (false, mensagemDeErro);
            }
        }

        public async Task<string> RealizarDeposito(int contaId, DepositoViewModel depositoVM)
        {
            var json = new StringContent(
                                JsonConvert.SerializeObject(depositoVM),
                                Encoding.UTF8,
                                "application/json");

            var response = await Client.PostAsync($"/api/ContaBancaria/RealizarDeposito?contaId={contaId}", json);

            var responseStream = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<string>(responseStream);

        }

        public async Task<Extrato> ObterExtrato(int contaId, DateTime dataInicio, DateTime dataFim)
        {
            var response = await Client.GetAsync($"/api/ContaBancaria/ObterExtrato?contaId={contaId}&dataInicio={dataInicio.Year}-{dataInicio.Month}-{dataInicio.Day}&dataFim={dataFim.Year}-{dataFim.Month}-{dataFim.Day}");

            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<Extrato>(responseStream);
            
        }
    }
}
