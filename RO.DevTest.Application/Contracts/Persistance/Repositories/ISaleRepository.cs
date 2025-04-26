using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Application.Contracts.Persistance.Repositories
{
    public interface ISaleRepository 
    {
        IQueryable<Sale> GetAllAsQueryable();
        Task<Sale> GetByIdAsync(Guid id);
        Task<Sale> CreateAsync(Sale sale);
        Task<Sale> UpdateAsync(Sale sale);
        Task DeleteAsync(Guid Id);
        Task SaveChangesAsync();


    }
}
