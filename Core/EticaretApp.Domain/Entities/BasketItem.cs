using EticaretApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Domain.Entities
{
    public class BasketItem: BaseEntity
    {
        public Guid BasketId { get; set; } //ürün hangi sepete eklendi onun bilgisi
        public Guid ProductId { get; set; } //Hangi ürün olduğunun bilgisi
        public int Quantity { get; set; } // Üründen kaç adet eklendiğinin bilgisi
        public Product Product { get; set; }
        public Basket Basket { get; set; }
    }
}
