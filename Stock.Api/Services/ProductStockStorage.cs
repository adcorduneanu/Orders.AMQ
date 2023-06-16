using Stock.Api.Controllers;
using Stock.Contracts.Models;

namespace Stock.Api.Services
{
    public sealed class ProductStockStorage : IProductStockStorage
    {
        private readonly Dictionary<string, ProductStock> stock = new();

        public Task<ProductStock> GetStock(string productId)
        {
            return Task.FromResult(stock[productId]);
        }

        public Task UpdateStock(string productId, int stock)
        {
            this.stock[productId].Quantity = stock;
            return Task.CompletedTask;
        }

        public Task DeleteStock(string productId)
        {
            stock.Remove(productId);
            return Task.CompletedTask;
        }

        public Task AddStock(Product product, int stock)
        {
            this.stock.Add(product.ProductId, new ProductStock { Product = product, Quantity = stock });
            return Task.CompletedTask;
        }
    }
}