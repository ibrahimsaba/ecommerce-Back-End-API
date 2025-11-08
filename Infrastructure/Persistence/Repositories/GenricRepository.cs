using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenricRepository<TEntity, TKey> : IGenricRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext context;

        public GenricRepository(StoreDbContext _context)
        {
            context = _context;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false)
        {
            if(typeof(TEntity) == typeof(Product))
            {
                if (trackChanges)
                {
                    return await context.Products.Include(p=> p.ProductBrand).Include(t=> t.ProductType).ToListAsync() as IEnumerable<TEntity>;
                }
                return await context.Products.Include(p => p.ProductBrand).Include(t => t.ProductType).AsNoTracking().ToListAsync() as IEnumerable<TEntity>;
            }
            if (trackChanges)
            {
                return await context.Set<TEntity>().ToListAsync();
            }
            return await context.Set<TEntity>().AsNoTracking().ToListAsync();

        }
        public async Task<TEntity?> GetAsync(TKey id)
        {
            if(typeof(TEntity) == typeof(Product))
            {
                return await context.Products
                    .Include(p => p.ProductBrand)
                    .Include(t => t.ProductType)
                    .FirstOrDefaultAsync(p => p.Id == id as int?) as TEntity;
            }
           return await context.Set<TEntity>().FindAsync(id);
        }
        public async Task AddAsync(TEntity entity)
        {
             await context.AddAsync(entity);
        }
        public void Update(TEntity entity)
        {
             context.Update(entity);
        }
        public void Delete(TEntity entity)
        {
             context.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> spec, bool trackChanges = false)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<TEntity?> GetAsync(ISpecifications<TEntity, TKey> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        public async Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await ApplySpecification(specifications).CountAsync();
        }
        private IQueryable<TEntity> ApplySpecification(ISpecifications<TEntity, TKey> spec)
        {
            return SpecificationEvaluator.GetQuery(context.Set<TEntity>(), spec);
        }


    }
}
