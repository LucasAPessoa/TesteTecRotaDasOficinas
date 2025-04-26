using Microsoft.EntityFrameworkCore;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Persistence.Repositories
{
    public class SaleRepository : BaseRepository<SaleItem>, ISaleRepository
    {
        private readonly DefaultContext _context;

        public SaleRepository(DefaultContext context) : base(context)
        {
            _context = context;
        }
        public IQueryable<Sale> GetAllAsQueryable()
        {
            return _context.Sales.AsQueryable();
        }
        public async Task<Sale> GetByIdAsync(Guid id)
        {
            return await _context.Sales
                .Include(s => s.SaleItems)
                    .ThenInclude(si => si.Product)
                    .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task<Sale> CreateAsync(Sale sale)
        {
            var entry = await _context.Sales.AddAsync(sale);
            await _context.SaveChangesAsync();
            return sale;
        }
        public async Task<Sale> UpdateAsync(Sale sale)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync();
            return sale;
        }
        public async Task DeleteAsync(Guid id)
        {
            var sale = await GetByIdAsync(id);
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
