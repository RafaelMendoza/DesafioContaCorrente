using DesafioStone.Domain.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DesafioStone.Infraestructure.Contracts
{
    public interface IReadOnlyRepository
    {

        Task<List<TEntity>> GetAllAsync<TEntity>()
            where TEntity : class, IEntity;

        Task<List<TEntity>> GetAsync<TEntity>(Expression<Func<TEntity, bool>> filter)
            where TEntity : class, IEntity;

        Task<TEntity> GetOneAsync<TEntity>(Expression<Func<TEntity, bool>> filter)
            where TEntity : class, IEntity;
    }
}