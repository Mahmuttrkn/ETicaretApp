using EticaretApp.Application.Repositories;
using EticaretApp.Domain.Entities;
using EticaretApp.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Persistence.Repositories
{
    public class BasketReadRepository : ReadRepository<EticaretApp.Domain.Entities.Basket>, IBasketReadRepository
    {
        public BasketReadRepository(EticaretAppDbContext context) : base(context)
        {
        }
    }
}
