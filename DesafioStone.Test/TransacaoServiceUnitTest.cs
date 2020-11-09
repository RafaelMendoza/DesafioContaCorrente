using DesafioStone.Business.Conta;
using DesafioStone.Domain.Models;
using DesafioStone.Infraestructure.Contracts;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DesafioStone.Test
{
    public class TransacaoServiceUnitTest
    {
        Mock<IRepository> repositoryMock;
        Mock<ILogger<TransacaoService>> loggerMock;

        ContaBancaria conta;

        [SetUp]
        public void SetUp()
        {
            conta = new ContaBancaria(50) { Id = 1 };

            repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(x => x.GetOneAsync<ContaBancaria>(It.IsAny<Expression<Func<ContaBancaria, bool>>>())).ReturnsAsync(conta);

            

            loggerMock = new Mock<ILogger<TransacaoService>>();

        }
    }
}
