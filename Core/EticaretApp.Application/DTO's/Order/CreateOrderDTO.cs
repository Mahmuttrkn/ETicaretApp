﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.DTO_s.Order
{
    public class CreateOrderDTO
    {
        public string Description { get; set; }
        public string Address { get; set; }
        public string? BasketId { get; set; }
    }
}
