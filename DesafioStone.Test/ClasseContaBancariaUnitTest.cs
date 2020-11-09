using DesafioStone.Domain.Models;
using NUnit.Framework;

namespace DesafioStone.Test
{
    public class ClasseContaBancariaTestes
    {
        [Test]
        public void AdicionarFundosSemConsiderarTaxas()
        {
            var conta = new ContaBancaria() { Id = 1 };
            conta.AdicionarFundos(10);

            Assert.AreEqual(10.0m, conta.Saldo);
        }

        [Test]
        public void RemoverFundosSemConsiderarTaxas()
        {
            var conta = new ContaBancaria(25) { Id = 1 };
            conta.RemoverFundos(10);

            Assert.AreEqual(15.0m, conta.Saldo);
        }

        [Test]
        public void VerificarSeSaqueMaiorQueSaldo()
        {
            var conta = new ContaBancaria(25) { Id = 1 };
            var res = conta.ValorDeSaqueMaiorQueSaldo(30);

            Assert.IsTrue(res);
        }

        [Test]
        public void VerificarSeSaqueMenorQueSaldo()
        {
            var conta = new ContaBancaria(25) { Id = 1 };
            var res = conta.ValorDeSaqueMaiorQueSaldo(15);

            Assert.IsFalse(res);
        }

        [Test]
        public void VerificarSeSaqueMaisTaxaMaiorQueSaldo()
        {
            var conta = new ContaBancaria(25) { Id = 1 };
            var res = conta.ValorDeSaqueMaisTaxaMaiorQueSaldo(20, 6);

            Assert.IsTrue(res);
        }

        [Test]
        public void VerificarSeSaqueMaisTaxaMenorQueSaldo()
        {
            var conta = new ContaBancaria(25) { Id = 1 };
            var res = conta.ValorDeSaqueMaisTaxaMaiorQueSaldo(15, 5);

            Assert.IsFalse(res);
        }
    }
}