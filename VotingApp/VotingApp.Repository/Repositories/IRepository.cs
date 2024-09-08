using VotingApp.Repository.Models;

namespace VotingApp.Repository.Repositories
{
    public interface IRepository<T> where T : class, IEntity
    {
        public Task<int> AddAsync(T entity);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T?> GetByIdAsync(int id);
        public void Update(T entity);
        public Task SaveChangesAsync();
    }
}
