using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SpaceTruckerCompany.API.Models;

namespace SpaceTruckerCompany.API.Data
{
    public interface IRepository<T> where T : class, IEntity
    {
        public T GetById(string id);
        public T Create(T entity);
        public T Update(T entity);
        public void Delete(T entity);
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
            return _context.Set<T>().Find(id) ?? throw new Exception($"Unable to find {typeof(T)} by Id");
        }
        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }
        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
            return entity;
        }
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
        public List<T> Get()
        {
            return _context.Set<T>().ToList();
        }
        public List<T> Search(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList();
        }


    }
}
