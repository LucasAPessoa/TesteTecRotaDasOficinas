using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Application.Contracts.Persistance.Repositories
{
    public interface IProductRepository 
    {
        Task<Product> GetByIdAsync(Guid id);
        Task<Product?> GetByNameAsync(string name);
        IQueryable<Product> GetAllAsQueryable();
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
    
}

