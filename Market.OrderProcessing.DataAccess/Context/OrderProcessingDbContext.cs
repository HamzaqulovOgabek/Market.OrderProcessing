using Market.OrderProcessing.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.OrderProcessing.Application.Context
{
    public class OrderProcessingDbContext : DbContext
    {
        public OrderProcessingDbContext(DbContextOptions<OrderProcessingDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(e =>
            {
                e.Property(o => o.TotalPrice)
                .HasPrecision(18, 2);
            });
            modelBuilder.Entity<Payment>(e =>
            {
                e.Property(p => p.Amount)
                .HasPrecision(18, 2);

                e.HasIndex(p => new { p.UserId, p.OrderId }).IsUnique();
            });
            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
