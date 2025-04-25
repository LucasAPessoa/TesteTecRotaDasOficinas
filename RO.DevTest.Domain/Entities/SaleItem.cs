using RO.DevTest.Domain.Abstract;

namespace RO.DevTest.Domain.Entities
{
    public class SaleItem : BaseEntity
    {
        public Product Product { get; set; } = new Product();
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Product.Price * Quantity;
    }
}
