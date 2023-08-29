using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SpaceTruckerCompany.Web.Admin.Models;

namespace SpaceTruckerCompany.Web.Admin.Data
{
    public interface IRepository<T> where T : class, IEntity
    {
        public T GetById(string id);
        public T Create(T entity);
        public T Update(T entity);
        public void Delete(T entity);
        public void DeleteBatch(List<T> entities);
        public List<T> Get();
        public List<T> Search(Expression<Func<T, bool>> predicate);
    }
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly ILogger<Repository<T>> _logger;
        private readonly ApplicationDbContext _context;
        public Repository(ILogger<Repository<T>> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public T GetById(string id)
        {
            lock (_context)
            {
                return _context.Set<T>().Find(id) ?? throw new Exception($"Unable to find {typeof(T)} by Id");
            }
        }
        public T Create(T entity)
        {
            lock (_context)
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
                return entity;
            }
        }
        public T Update(T entity)
        {
            lock (_context)
            {
                _context.Set<T>().Attach(entity);
                _context.Set<T>().Update(entity);
                _context.SaveChanges();
                return entity;
            }

        }
        public void Delete(T entity)
        {
            lock (_context)
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }
        }

        public void DeleteBatch(List<T> entities)
        {
            lock (_context)
            {
                _context.Set<T>().RemoveRange(entities);
                _context.SaveChanges();
            }
            /*
            await Task.WhenAll(entities.Select(async entity =>
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }));
            */
        }
        public List<T> Get()
        {
            lock (_context)
            {
                return _context.Set<T>().ToList();
            }
        }
        public List<T> Search(Expression<Func<T, bool>> predicate)
        {
            lock (_context)
            {
                return _context.Set<T>().Where(predicate).ToList();
            }
        }


    }
}
