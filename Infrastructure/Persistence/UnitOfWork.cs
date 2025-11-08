using Domain.Contracts;
using Domain.Models;
using Persistence.Data;
using Persistence.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext context;
        private readonly ConcurrentDictionary<string, object> repository;

        public UnitOfWork(StoreDbContext _context)
        {
            context = _context;
            repository = new ConcurrentDictionary<string, object>();
        }
        public   IGenricRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            return (GenricRepository<TEntity, TKey>)repository.GetOrAdd(typeof(TEntity).Name, new GenricRepository<TEntity, TKey>(context));
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
