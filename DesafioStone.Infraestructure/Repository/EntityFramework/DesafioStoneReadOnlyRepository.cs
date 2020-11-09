using DesafioStone.Domain.Models.Contracts;
using DesafioStone.Infraestructure.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DesafioStone.Infraestructure.Repository.EntityFramework
{
    public class DesafioStoneReadOnlyRepository<TContext> : IReadOnlyRepository
            where TContext : DbContext
    {
        protected readonly TContext context;

        public DesafioStoneReadOnlyRepository(TContext context)
        {
            this.context = context;
        }

        public virtual async Task<List<TEntity>> GetAllAsync<TEntity>()
            where TEntity : class, IEntity
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetAsync<TEntity>(Expression<Func<TEntity, bool>> filter)
            where TEntity : class, IEntity
        {
            return await context.Set<TEntity>()
                .Where(filter)
                .ToListAsync();
        }

        public virtual async Task<TEntity> GetOneAsync<TEntity>(Expression<Func<TEntity, bool>> filter)
            where TEntity : class, IEntity
        {
            return await context.Set<TEntity>()
                .Where(filter)
                .FirstOrDefaultAsync();
        }
    }
}
