using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Application.Contracts.Persistance.Repositories
{
    public interface ISaleItemRepository
    {
        Task<SaleItem> GetByIdAsync(Guid id);
        Task<SaleItem> GetByProductIdAsync(Guid productId);
        Task<List<SaleItem>> GetAllAsync();
        Task UpdateAsync(SaleItem saleItem);
        Task DeleteAsync(Guid id);
        Task DeleteManyAsync(IEnumerable<Guid> ids);
        Task<SaleItem> CreateAsync(SaleItem saleItem);
    }
}