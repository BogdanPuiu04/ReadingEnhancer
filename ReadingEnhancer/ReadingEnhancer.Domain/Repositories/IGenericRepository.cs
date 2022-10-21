namespace ReadingEnhancer.Domain.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetFirstAsync(string id);
        Task<List<T>> GetAllAsync();

        Task<T> AddAsync(T entity);

        Task RemoveOne(T entity);

        Task<T> UpdateOne(string id, T entity);
    }
}