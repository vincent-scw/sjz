using SJZ.Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SJZ.Common
{
    /// <summary>
    /// CRUD repository
    /// </summary>
    /// <typeparam name="TEntity">entity</typeparam>
    public interface IRepository<TEntity>
        where TEntity : IAggregateRoot
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> GetAsync(object id);
        Task DeleteAsync(object id);
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}
