using EticaretApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Features.Queries.Product.GetProductID
{
    public class GetProductIDQueryResponse
    {
        public String Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }

        //public ICollection<Order> Orders { get; set; }
       // public ICollection<EticaretApp.Domain.Entities.ProductImageFile> ProductImageFiles { get; set; }
    }
}
