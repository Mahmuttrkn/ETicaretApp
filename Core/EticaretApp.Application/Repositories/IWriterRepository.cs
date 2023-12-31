﻿using EticaretApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Repositories
{
    public interface IWriterRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> datas);
        bool Remove(T model);
        bool RemoveRange(List<T> datas);
        Task<bool> RemoveAsync(string id);
        bool Update(T model);
        Task<T> GetByIdAsync(string id, bool tracking = true);
        Task<int> SaveAsync();
    }
}
