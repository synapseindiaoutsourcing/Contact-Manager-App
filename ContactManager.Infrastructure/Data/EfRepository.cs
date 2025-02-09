﻿using ContactManager.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using ContactManager.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Data;
using ContactManager.Infrastructure.Data;

namespace ContactManager.Infrastructure.Data
{
    /// <summary>
    /// "There's some repetition here - couldn't we have some the sync methods call the async?"
    /// https://blogs.msdn.microsoft.com/pfxteam/2012/04/13/should-i-expose-synchronous-wrappers-for-asynchronous-methods/
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EfRepository<T> : IAsyncRepository<T> where T : BaseEntity, IAggregateRoot
    {
        protected readonly ContactManagerContext _dbContext;

        public EfRepository(ContactManagerContext dbContext)
        {
            _dbContext = dbContext;
            
        }
        
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        
        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        
        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
           
                _dbContext.Set<T>().Add(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
        }
        
        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public void UpdateMultiple(IEnumerable<T> List)
        {
            foreach (var entity in List)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public async Task<T> SingleAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().SingleAsync(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AsNoTracking().AnyAsync(predicate);
        }

        public IQueryable<T> GetAllQuerable()
        {
            return _dbContext.Set<T>().AsNoTracking();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }

      
        public IEnumerable<T> GetAllFromProcedure(string proc, params object[] parameters)
        {
            try
            {               
                 return _dbContext.Set<T>().FromSqlRaw(proc, parameters).ToList();                
            }
            catch (Exception ex)
            {
                return null;
                
            }
        }
    }
}
