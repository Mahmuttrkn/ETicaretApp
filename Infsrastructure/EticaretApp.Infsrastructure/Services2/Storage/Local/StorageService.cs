using EticaretApp.Application.Abstractions.Storage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Infsrastructure.Services2.Storage.Local
{
    public class StorageService : IStorageService
    {
        private readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public string StorageName => _storage.GetType().Name;   

        public async Task DeleteAsync(string pathOrContainerName, string fileName)

         => await _storage.DeleteAsync(pathOrContainerName, fileName);


        public List<string> GetFiles(string pathOrContainerName)
       => _storage.GetFiles(pathOrContainerName);

        public bool HasFile(string pathOrContainerName, string fileName)
        => _storage.HasFile(pathOrContainerName, fileName);

        public async Task<List<(string fileName, string pathOrContainerName)>> UpluoadAsync(string pathOrContainerName, IFormFileCollection files)
        => await _storage.UpluoadAsync(pathOrContainerName, files);
    }
}
