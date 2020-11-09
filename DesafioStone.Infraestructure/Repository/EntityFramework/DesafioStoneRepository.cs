using DesafioStone.Domain.Models.Contracts;
using DesafioStone.Infraestructure.Contracts;
using DesafioStone.Infraestructure.Repository.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DesafioStone.Infraestructure.Repository
{
    public class DesafioStoneRepository<TContext> : DesafioStoneReadOnlyRepository<TContext>, IRepository
            where TContext : DbContext
    {
        public DesafioStoneRepository(TContext context)
            : base(context)
        {
        }

        public virtual async Task CreateAsync<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            context.Set<TEntity>().Add(entity);
            await SaveAsync();
        }

        public virtual async Task UpdateAsync<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            context.Set<TEntity>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            await SaveAsync();
        }

        protected virtual async Task SaveAsync()
        {
            await context.SaveChangesAsync();

        }
    }
}
