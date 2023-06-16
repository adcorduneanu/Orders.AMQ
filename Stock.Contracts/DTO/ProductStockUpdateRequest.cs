namespace Stock.Api.Controllers
{
    internal class ProductStockUpdateRequest
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}