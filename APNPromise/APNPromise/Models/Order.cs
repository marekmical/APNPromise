namespace APNPromise.Models
{
    public class Order
    {
        public Guid OrderID { get; set; }
        public IEnumerable<OrderLine> OrderLines { get; set; }
    }
}
