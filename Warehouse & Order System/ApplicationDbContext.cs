using Microsoft.EntityFrameworkCore;
using Warehouse___Order_System.Models;

namespace Warehouse___Order_System
{
    public class ApplicationDbContext : DbContext
    {
      
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerProfile> CustomerProfiles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

      
        public DbSet<Product> Products { get; set; }
        public DbSet<PhysicalProduct> PyProducts { get; set; }
        public DbSet<Digital> DiProducts { get; set; }

   
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Data Source=DESKTOP-CJ469SU\\SQLEXPRESS;Initial Catalog=MySystem1;Integrated Security=True;TrustServerCertificate=True;");
        }

      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

    
     
   
            modelBuilder.Entity<Product>()
                .HasDiscriminator<string>("ProductType")
                .HasValue<PhysicalProduct>("Physical")
                .HasValue<Digital>("Digital");

          
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.profile)
                .WithOne(p => p.customer)
                .HasForeignKey<CustomerProfile>(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Cascade); 

            
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique();


      
          
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .HasConversion<string>()
                .HasMaxLength(20);


            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });

      
            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.orderProduct)
                .HasForeignKey(op => op.OrderId);

     
            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.product)
                .WithMany() 
                .HasForeignKey(op => op.ProductId);


            modelBuilder.Entity<Order>()
                .HasIndex(o => new { o.OrderDate, o.Id })
                .HasDatabaseName("IX_Orders_Date_Customer");
        }
    }
}