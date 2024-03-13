using Assignment.Domain.Contracts.IRepositories;
using Assignment.Domain.Contracts.Repositories;
using Assignment.Infrastructure.__AppContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Infrastructure.Reposatories
{
    public class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        // private DbSet<TEntity,TId> _set;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            //_set = _context.Set<TEntity,TId>();
        }
        public TEntity Get(int id)
        {
            var x = _context.Set<TEntity>().Find(id);
            return x;
        } 

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _context.Set<IEnumerable<TEntity>>().AddRangeAsync(entities);
        }
        public async Task Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
        public Task DeleteRange(IEnumerable<TId> entities)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
        public void Add(TEntity entity)
        {
             _context.Set<TEntity>().AddAsync(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> Get(TId id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public void Remove(TId entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateDetached(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
