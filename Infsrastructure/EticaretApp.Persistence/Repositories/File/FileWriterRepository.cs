using EticaretApp.Application.Repositories;
using EticaretApp.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Persistence.Repositories
{
    public class FileWriterRepository: WriteRepository<EticaretApp.Domain.Entities.File>, IFileWriterRepository
    {
        public FileWriterRepository(EticaretAppDbContext context) : base(context) { }  
    }
}
