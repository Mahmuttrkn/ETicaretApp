using EticaretApp.Application.Repositories;
using EticaretApp.Domain.Entities;
using EticaretApp.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Persistence.Repositories
{
    public class InvoiceFileWriterRepository : WriteRepository<InvoiceFile>, IInvoiceFileWriterRepository
    {
        public InvoiceFileWriterRepository(EticaretAppDbContext context) : base(context)
        {
        }
    }
}
