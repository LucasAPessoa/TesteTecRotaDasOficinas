using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Persistence.Repositories;

public class ProductRepository
    : BaseRepository<Product>, IProductRepository
{
    private readonly DefaultContext _context;

    public ProductRepository(DefaultContext context) : base(context)
    {
        _context = context;
    }
    public async Task<Product> GetByIdAsync(Guid id)
    {
        return await _context.Products.FirstAsync(p => p.Id == id);
    }

    public async Task<Product?> GetByNameAsync(string name)
    {
        return await _context.Products
            .Where(p => p.Name == name)
            .FirstOrDefaultAsync();
    }

    public IQueryable<Product> GetAllAsQueryable()
    {
        return _context.Products.AsQueryable();
    }

    public async Task<Product> CreateAsync(Product product)
    {
        var entry = await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await GetByIdAsync(id);
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

}