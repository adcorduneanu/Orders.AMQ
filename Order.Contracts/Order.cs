namespace Order.Contracts
{
    public class Order
    {
        public string OrderId { get; set; }
        public string CustomerName { get; set; }
        public string[] ProductIds { get; set; }
        public string VoucherCode { get; set; }
    }
}