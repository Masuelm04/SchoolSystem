using Microsoft.EntityFrameworkCore;
using SchoolSystem.Core.Interfaces;
using SchoolSystem.Domain.ClaseBase;
using SchoolSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Infrastructure.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _entities;
        protected RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }
        public async virtual Task AddAsync(T entity)
        {
            _entities.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async virtual Task<bool?> DeleteByIdAsync(int id)
        {
            var entity = await _entities.FindAsync(id);

            entity.Eliminado = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async virtual Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.Where(e => e.Eliminado == false).ToListAsync();
        }

        public async virtual Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _entities;

            query = query.Where(e => !e.Eliminado);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            // Incluir las propiedades de navegación especificadas
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async virtual Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _entities;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async virtual Task<T> GetByIdAsync(int id)
        {
            return await _entities.Where(e => e.Id == id && e.Eliminado == false).FirstOrDefaultAsync();
        }

        public async virtual Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _entities;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => e.Id == id && e.Eliminado == false);
        }

        public async virtual Task UpdateAsync(T entity)
        {
            var existingEntity = await _entities.Where(e => e.Id == entity.Id && e.Eliminado == false).FirstOrDefaultAsync();

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            _context.Entry(existingEntity).Property(e => e.FechaRegistro).IsModified = false;

            await _context.SaveChangesAsync();
        }
    }
}
