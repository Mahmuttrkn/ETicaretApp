using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.DTO_s.Order
{
    public class SingleOrderDTO
    {
        public string Adress { get; set; }
        public object BasketItems { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public string OrderCode { get; set; }
    }
}
