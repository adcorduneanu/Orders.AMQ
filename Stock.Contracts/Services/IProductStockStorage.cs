using Stock.Contracts.Models;

namespace Stock.Api.Controllers
{
    public interface IProductStockStorage
    {
        Task<ProductStock> GetStock(string productId);

        Task UpdateStock(string productId, int stock);

        Task DeleteStock(string productId);

        Task AddStock(Product product, int stock);
    }
}