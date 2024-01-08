using EticaretApp.Domain.Entities;
using EticaretApp.Domain.Entities.Common;
using EticaretApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Persistence.Contexts
{
    public class EticaretAppDbContext : IdentityDbContext<AppUser,AppRole,string>
    {
        public EticaretAppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) // 1-1 ilişkiyi bu şekilde override ederek gösteriyoruz.
        {
            builder.Entity<Order>()
                .HasKey(b => b.Id);

            builder.Entity<Order>()
                .HasIndex(o => o.OrderCode)
                .IsUnique();

            builder.Entity<Basket>()
                .HasOne(b => b.Order)
                .WithOne(b => b.Basket)
                .HasForeignKey<Order>(b => b.Id);

            base.OnModelCreating(builder); //Identity kullandığımız için base üzerinden çağırmamız gerekiyor.
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
           var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreateDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdateDate = DateTime.UtcNow,
                    _=>DateTime.UtcNow

                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
