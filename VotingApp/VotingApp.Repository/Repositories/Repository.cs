using Microsoft.EntityFrameworkCore;
using VotingApp.Repository.Context;
using VotingApp.Repository.Models;

namespace VotingApp.Repository.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly VotingContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(VotingContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<int> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity.Id;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
           _dbSet.Update(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
