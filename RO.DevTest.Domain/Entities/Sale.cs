using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RO.DevTest.Domain.Abstract;

namespace RO.DevTest.Domain.Entities
{
    public class Sale : BaseEntity
    {

        public List<SaleItem> SaleItems { get; set; } = new List<SaleItem>();

        public decimal TotalSalePrice => SaleItems.Sum(item => item.Quantity * item.Product.Price);

        public int TotalItems => SaleItems.Sum(item => item.Quantity);
        public string UserId { get; set; } = string.Empty;
        public User User { get; set; }

    }
}
