
using APNPromise.Controllers;
using APNPromise.Models;
using Microsoft.EntityFrameworkCore;

namespace APNPromiseTests
{
    public class OrdersTests
    {
        [Test]
        public async Task GetOrdersList_ShouldReturnCorrectOrderCount()
        {
            var options = new DbContextOptionsBuilder<OrdersContext>()
                .UseInMemoryDatabase(databaseName: "Orders Test")
                .Options;

            var testOrders = GetTestOrders();
            using (var ordersContext = new OrdersContext(options))
            {
                ordersContext.AddRange(testOrders);
                ordersContext.SaveChanges();
            }

            using (var ordersContext = new OrdersContext(options))
            {
                var controller = new OrdersController(ordersContext);
                var result = await controller.GetOrdersList();
                Assert.That(result.Value, Is.Not.Null);
                Assert.That(result.Value.Count, Is.EqualTo(testOrders.Count));
                ordersContext.Database.EnsureDeleted();
            }
        }

        private List<Order> GetTestOrders()
        {
            var testOrders = new List<Order>();
            testOrders.Add(new Order { OrderID = Guid.NewGuid(), OrderLines = new[] { new OrderLine { BookId = 1, Quantity = 2 }, new OrderLine { BookId = 3, Quantity = 4 } } });
            testOrders.Add(new Order { OrderID = Guid.NewGuid(), OrderLines = new[] { new OrderLine { BookId = 5, Quantity = 6 }, new OrderLine { BookId = 7, Quantity = 8 } } });

            return testOrders;
        }
    }
}
