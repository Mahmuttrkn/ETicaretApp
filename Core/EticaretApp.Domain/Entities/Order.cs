using EticaretApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
         
        public Basket Basket { get; set; } // Basketle Order arasında 1-1 bir ilişki oluşturduk.

        public ICollection<Product> Products { get; set; }
        public Customer Customer { get; set; } //1 sepetin sadece 1 tane sahibi olabilir.
    }
}
