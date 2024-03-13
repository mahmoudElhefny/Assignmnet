using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Domain.Contracts.IRepositories
{
    public interface IBaseRepository<TEntity,TId> where TEntity : class
    {
        Task<TEntity> Get(TId id);
        IQueryable<TEntity> GetAll();
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateDetached(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Remove(TId entity);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
