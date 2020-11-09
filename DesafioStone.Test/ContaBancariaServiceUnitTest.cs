using DesafioStone.Business.Conta;
using DesafioStone.Business.Contracts;
using DesafioStone.Domain.Models;
using DesafioStone.Infraestructure.Contracts;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Linq.Expressions;

namespace DesafioStone.Test
{
    public class ContaBancariaServiceUnitTest
    {
        Mock<IRepository> repositoryMock;
        Mock<ITransacaoService> transacaoServiceMock;
        Mock<ILogger<ContaBancariaService>> loggerMock;

        ContaBancaria conta;

        [SetUp]
        public void SetUp()
        {
            conta = new ContaBancaria(50) { Id = 1 };

            repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(x => x.GetOneAsync<ContaBancaria>(It.IsAny<Expression<Func<ContaBancaria, bool>>>())).ReturnsAsync(conta);

            transacaoServiceMock = new Mock<ITransacaoService>();

            loggerMock = new Mock<ILogger<ContaBancariaService>>();

        }

        [Test]
        public void RealizarSaqueComTaxasComSucesso()
        {
            ContaBancariaService contaBancariaService = new ContaBancariaService(repositoryMock.Object, transacaoServiceMock.Object, loggerMock.Object);

            var result = contaBancariaService.RealizarSaque(1, 10).Result;
            var foiSucesso = result.Item1;
            var mensagemErro = result.Item2;


            Assert.IsTrue(foiSucesso);
            Assert.AreEqual(36.0m, conta.Saldo);
        }

        [Test]
        public void ValorDoSaqueMaiorQueSaldo()
        {
            ContaBancariaService contaBancariaService = new ContaBancariaService(repositoryMock.Object, transacaoServiceMock.Object, loggerMock.Object);

            var result = contaBancariaService.RealizarSaque(1, 60).Result;
            var foiSucesso = result.Item1;
            var mensagemErro = result.Item2;


            Assert.IsTrue(foiSucesso == false);
            Assert.IsTrue(mensagemErro == "O Valor do Saque é maior que o Saldo disponível!");
            Assert.AreEqual(50.0m, conta.Saldo);
        }

        [Test]
        public void ValorDoSaqueMenorQueSaldoMasComTaxaSuperiorAoSaldoDisponivel()
        {
            ContaBancariaService contaBancariaService = new ContaBancariaService(repositoryMock.Object, transacaoServiceMock.Object, loggerMock.Object);

            var result = contaBancariaService.RealizarSaque(1, 47).Result;
            var foiSucesso = result.Item1;
            var mensagemErro = result.Item2;

            Assert.IsTrue(foiSucesso == false);
            Assert.IsTrue(mensagemErro == "O Valor do Saque é maior que o Saldo disponível!");
            Assert.AreEqual(50.0m, conta.Saldo);
        }

        [Test]
        public void RealizarDepositoComTaxasComSucesso()
        {
            ContaBancariaService contaBancariaService = new ContaBancariaService(repositoryMock.Object, transacaoServiceMock.Object, loggerMock.Object);

            var result = contaBancariaService.RealizarDeposito(1, 10).Result;
            var foiSucesso = result.Item1;
            var mensagemErro = result.Item2;

            Assert.IsTrue(foiSucesso);
            Assert.AreEqual(59.0m, conta.Saldo);
        }

    }
}