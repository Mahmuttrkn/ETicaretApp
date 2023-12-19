using EticaretApp.Application.Repositories;
using EticaretApp.Domain.Entities;
using EticaretApp.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Persistence.Repositories
{
    public class BasketWriterRepository : WriteRepository<Basket>, IBasketWriterRepository
    {
        public BasketWriterRepository(EticaretAppDbContext context) : base(context)
        {
        }
    }
}
