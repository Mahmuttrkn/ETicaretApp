
using EticaretApp.Application.Abstractions.Services;
using EticaretApp.Application.Abstractions.Services.Authentication;
using EticaretApp.Application.Repositories;
using EticaretApp.Domain.Entities.Identity;
using EticaretApp.Persistence.Contexts;
using EticaretApp.Persistence.Repositories;
using EticaretApp.Persistence.Services;
using Microsoft.AspNetCore.Identity;
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
            }).AddEntityFrameworkStores<EticaretAppDbContext>()
            .AddDefaultTokenProviders();


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
            services.AddScoped<IBasketReadRepository, BasketReadRepository>();
            services.AddScoped<IBasketWriterRepository, BasketWriterRepository>();
            services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
            services.AddScoped<IBasketItemWriterRepository, BasketItemWriterRepository>();


            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IExternalAuthentication,UserLoginService>();
            services.AddScoped<IInternalAuthentication,UserLoginService>();
            services.AddScoped<IAuthService,UserLoginService>();
            services.AddScoped<IBasketService, BasketService>();


            services.AddScoped<IOrderService , OrderService>();


        }
    }
}
