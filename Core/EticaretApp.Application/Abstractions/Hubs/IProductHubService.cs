﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Application.Abstractions.Hubs
{
    public interface IProductHubService
    {
        Task ProductEditMessageAsync(string message);
    }
}
