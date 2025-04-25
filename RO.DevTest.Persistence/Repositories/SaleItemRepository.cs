using Microsoft.EntityFrameworkCore;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Entities;
using RO.DevTest.Persistence;

public class SaleItemRepository : ISaleItemRepository
{
    private readonly DefaultContext _context;

    public SaleItemRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<SaleItem> GetByIdAsync(Guid id)
    {
        var saleItem = await _context.SaleItems
            .FirstOrDefaultAsync(si => si.Id == id);

        if (saleItem == null)
        {
            throw new ArgumentException($"SaleItem com ID {id} não encontrado.");
        }

        return saleItem;
    }

    public async Task<SaleItem> GetByProductIdAsync(Guid productId)
    {
        var saleItem = await _context.SaleItems
            .FirstOrDefaultAsync(si => si.Product.Id == productId);

        if (saleItem == null)
        {
            throw new ArgumentException($"SaleItem com ProductId {productId} não encontrado.");
        }

        return saleItem;
    }

    public async Task<List<SaleItem>> GetAllAsync()
    {
        return await _context.SaleItems.ToListAsync();
    }

    public async Task UpdateAsync(SaleItem saleItem)
    {
        _context.SaleItems.Update(saleItem);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var saleItem = await GetByIdAsync(id);
        if (saleItem != null)
        {
            _context.SaleItems.Remove(saleItem);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<SaleItem> CreateAsync(SaleItem saleItem)
    {
        var entry = await _context.SaleItems.AddAsync(saleItem);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task DeleteManyAsync(IEnumerable<Guid> ids)
    {
        var saleItems = await _context.SaleItems
            .Where(si => ids.Contains(si.Id))
            .ToListAsync();
        if (saleItems.Any())
        {
            _context.SaleItems.RemoveRange(saleItems);
            await _context.SaveChangesAsync();
        }
    }

}