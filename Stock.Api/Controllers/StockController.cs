using Apache.NMS.ActiveMQ.Commands;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Stock.Contracts.Models;

namespace Stock.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductStockController : ControllerBase
    {
        private readonly IBus bus;
        private readonly ILogger<ProductStockController> logger;
        private readonly IProductStockStorage productStockStorage;

        public ProductStockController(IBus bus, ILogger<ProductStockController> logger, IProductStockStorage productStockStorage)
        {
            this.bus = bus;
            this.logger = logger;
            this.productStockStorage = productStockStorage;
        }

        [HttpGet("{productId}")]
        public async Task<ProductStock> Get(string productId)
        {
            return await productStockStorage.GetStock(productId);
        }

        [HttpPost]
        public async Task Post(ProductStock productStock)
        {
            await bus.Publish(new ProductStockCreateRequest { ProductStock = productStock });
        }

        [HttpPut("{productId}")]
        public async Task Put(string productId, int quantity)
        {
            await bus.Publish(new ProductStockUpdateRequest { ProductId = productId, Quantity = quantity });
        }

        [HttpDelete("{productId}")]
        public async Task Delete(string productId)
        {
            await bus.Publish(new ProductStockDeleteRequest { ProductId = productId });
        }
    }
}