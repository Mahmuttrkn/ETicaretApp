using EticaretApp.Application.Repositories.CompletedOrder;
using EticaretApp.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Persistence.Repositories.CompletedOrder
{
    public class CompletedOrderWriterRepository : WriteRepository<EticaretApp.Domain.Entities.CompletedOrder>, ICompletedWriterRepository
    {
        public CompletedOrderWriterRepository(EticaretAppDbContext context) : base(context)
        {
        }
    }
}
