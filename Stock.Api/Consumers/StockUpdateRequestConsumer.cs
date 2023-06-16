namespace Stock.Api.Consumers
{
    using MassTransit;
    using Stock.Contracts;
    using Stock.Contracts.DTO;

    public class StockUpdateRequestConsumer : IConsumer<StockUpdateRequest>
    {
        public async Task Consume(ConsumeContext<StockUpdateRequest> context)
        {
            // Perform stock update logic
            var stockUpdatedSuccessfully = await UpdateStock(context.Message.OrderId);

            // Send the response
            await context.RespondAsync(new StockUpdateResponse { OrderId = context.Message.OrderId, StockUpdatedSuccessfully = stockUpdatedSuccessfully });
        }

        private async Task<bool> UpdateStock(string orderId)
        {
            // Perform the stock update logic
            // Return true if the stock was updated successfully, false otherwise
            // You can replace this with your actual stock update logic
            return await Task.FromResult(orderId.Contains("stock"));
        }
    }
}
