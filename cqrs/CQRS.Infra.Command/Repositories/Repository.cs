using System.Collections.Generic;
using System.Threading.Tasks;
using CQRS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Infra.Command.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly EntityContext _context;

        public Repository(EntityContext context)
        {
            _context = context;
        }

        public virtual async Task<IEnumerable<T>> ListAsync()
        {
            return await _context.Set<T>()
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public virtual async Task<T> FindByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            //await _context.SaveChangesAsync();
        }

        public virtual void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}