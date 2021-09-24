using Microsoft.EntityFrameworkCore;
using new_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_project.Data
{
    public class RepositoryFor<T> : Repository<T> where T: class
    {
        private readonly DbSet<T> _dbSet;
        private readonly ApplicationDbContext _context;
        public RepositoryFor(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Create(T t)
        {
            _dbSet.AddAsync(t);
            _context.SaveChanges();
        }

        public void Delete(T t)
        {
            _dbSet.Remove(t);
            _context.SaveChanges();
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T Read(int id)
        {
            return _dbSet.FindAsync(id).GetAwaiter().GetResult();
        }

        public void Update(T t)
        {
            _dbSet.Update(t);
            _context.SaveChanges();
        }
    }
}
