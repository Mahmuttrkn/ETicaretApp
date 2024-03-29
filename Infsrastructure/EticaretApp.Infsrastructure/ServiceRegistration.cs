﻿
using EticaretApp.Application.Abstractions.Services;
using EticaretApp.Application.Abstractions.Services.Configurations;
using EticaretApp.Application.Abstractions.Storage;
using EticaretApp.Application.Abstractions.Token;
using EticaretApp.Infsrastructure;
using EticaretApp.Infsrastructure.Services2;
using EticaretApp.Infsrastructure.Services2.Configuration;
using EticaretApp.Infsrastructure.Services2.Storage;
using EticaretApp.Infsrastructure.Services2.Storage.Local;
using EticaretApp.Infsrastructure.Services2.Token;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Infsrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IStorageService, StorageService>();
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
            serviceCollection.AddScoped<IMailService, MailService>();
            serviceCollection.AddScoped<IApplicationService, ApplicationService>();
            
        }
        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storage, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
        }
    }
}
