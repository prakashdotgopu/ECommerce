namespace ECommerce.Api.Orders.Db
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total 
        {
            get { return Items != null ? Items.Sum(i => i.UnitPrice) : 0; }
        }
        public List<OrderItem> Items { get; set; }
    }
}
