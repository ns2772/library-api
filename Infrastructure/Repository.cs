using LibraryApi.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly LibraryDbContext _libraryContext;
        public Repository(LibraryDbContext libraryDbContext)
        {
            _libraryContext = libraryDbContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _libraryContext.Set<T>().AddAsync(entity);
            await _libraryContext.SaveChangesAsync();
            return entity;
        }
        public async Task DeleteAsync(T entity)
        {
            _libraryContext.Set<T>().Remove(entity);
            await _libraryContext.SaveChangesAsync();
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _libraryContext.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _libraryContext.Set<T>().FindAsync(id);
        }
        public async Task<T> UpdateAsync(T entity)
        {
            _libraryContext.Entry(entity).State = EntityState.Modified;
            await _libraryContext.SaveChangesAsync();
            return entity;
        }
    }
}
