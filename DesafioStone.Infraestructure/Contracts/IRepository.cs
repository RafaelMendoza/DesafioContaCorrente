using DesafioStone.Domain.Models.Contracts;
using System.Threading.Tasks;

namespace DesafioStone.Infraestructure.Contracts
{
    public interface IRepository : IReadOnlyRepository
    {
        public Task CreateAsync<TEntity>(TEntity entity)
            where TEntity : class, IEntity;

        public Task UpdateAsync<TEntity>(TEntity entity)
            where TEntity : class, IEntity;
    }
}
