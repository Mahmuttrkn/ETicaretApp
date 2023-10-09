
using EticaretApp.Application.Repositories;
using EticaretApp.Persistence.Contexts;
using EticaretApp.Persistence.Repositories;
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
