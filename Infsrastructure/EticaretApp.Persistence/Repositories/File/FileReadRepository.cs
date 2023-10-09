using EticaretApp.Application.Repositories;
using EticaretApp.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Persistence.Repositories
{
    public class FileReadRepository: ReadRepository<EticaretApp.Domain.Entities.File>, IFileReadRepository
    {
        public FileReadRepository(EticaretAppDbContext context) :base(context) { }
    }
}
