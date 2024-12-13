using Microsoft.EntityFrameworkCore;

namespace APNPromise.Models
{
    public class OrdersContext : DbContext
    {
        public OrdersContext(DbContextOptions<OrdersContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var order = modelBuilder.Entity<Order>();
            order.HasKey(order => order.OrderID);
            order.HasMany(order => order.OrderLines);

            var orderLine = modelBuilder.Entity<OrderLine>();
            orderLine.HasKey(orderLine => new { orderLine.BookId, orderLine.Quantity });
        }

        public DbSet<Order> OrdersList { get; set; } = null!;
    }
}
