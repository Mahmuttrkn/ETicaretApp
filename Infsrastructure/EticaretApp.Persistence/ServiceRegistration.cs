
using EticaretApp.Application.Repositories;
using EticaretApp.Domain.Entities.Identity;
using EticaretApp.Persistence.Contexts;
using EticaretApp.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Persistence
{

    public static class ServiceRegistration
    {

        public static void AddPersistenceServices(this IServiceCollection services)
        {


            services.AddDbContext<EticaretAppDbContext>(options => options.UseNpgsql(Configuration.ConnectingString));
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false; 
            }).AddEntityFrameworkStores<EticaretAppDbContext>();
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriterRepository, CustomerWriterRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriterRepository, ProductWriterRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriterRepository, OrderWriterRepository>();
            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriterRepository, FileWriterRepository>();
            services.AddScoped<IProductImageReadRepository, ProductReadImageRepository>();
            services.AddScoped<IProductImageWriterRepository, ProductWriterImageRepository>();
            services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
            services.AddScoped<IInvoiceFileWriterRepository, InvoiceFileWriterRepository>();


        }
    }
}
