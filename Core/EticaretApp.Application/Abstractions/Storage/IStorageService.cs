using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Abstractions.Storage
{
    public interface IStorageService: IStorage
    {
        string StorageName { get; }
    }
}
