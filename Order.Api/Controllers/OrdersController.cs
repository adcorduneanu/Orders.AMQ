namespace Order.Api.Controllers
{
    using Mail.Contracts;
    using MassTransit;
    using Microsoft.AspNetCore.Mvc;
    using Order.Contracts;
    using Stock.Contracts.DTO;
    using Voucher.Contracts;

    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IRequestClient<VoucherRedeemRequest> voucherRedeemRequestClient;
        private readonly IRequestClient<StockUpdateRequest> stockUpdateRequestClient;
        private readonly IBus bus;
        private readonly ILogger<OrdersController> logger;

        public OrdersController(IRequestClient<VoucherRedeemRequest> voucherRedeemRequestClient, IRequestClient<StockUpdateRequest> stockUpdateRequestClient, IBus bus, ILogger<OrdersController> logger)
        {
            this.voucherRedeemRequestClient = voucherRedeemRequestClient;
            this.stockUpdateRequestClient = stockUpdateRequestClient;
            this.bus = bus;
            this.logger = logger;
        }

        [HttpPost]
        public async Task Post(Order order)
        {
            await bus.Publish(order);

            // Step 2: Redeem the voucher
            var voucherRedeemResponse = await voucherRedeemRequestClient.GetResponse<VoucherRedeemResponse>(new VoucherRedeemRequest { OrderId = order.OrderId });

            if (!voucherRedeemResponse.Message.VoucherRedeemedSuccessfully)
            {
                // Voucher redemption failed, handle the failure
                await HandleVoucherRedemptionFailure(order);
                return;
            }

            // Step 3: Update the stock
            var stockUpdateResponse = await stockUpdateRequestClient.GetResponse<StockUpdateResponse>(new StockUpdateRequest { OrderId = order.OrderId });

            if (!stockUpdateResponse.Message.StockUpdatedSuccessfully)
            {
                // Stock update failed, handle the failure
                await HandleStockUpdateFailure(order);
                return;
            }

            // Step 4: Notify the mail system
            await bus.Publish(new OrderConfirmationMail { OrderId = order.OrderId, Customer = order.CustomerName });
        }

        private async Task HandleVoucherRedemptionFailure(Order order)
        {
            // Perform the necessary actions when voucher redemption fails
            await bus.Publish(new VoucherRedeemedFailed { OrderId = order.OrderId });

            // Retry voucher redemption or take other appropriate actions
        }

        private async Task HandleStockUpdateFailure(Order order)
        {
            // Perform the necessary actions when stock update fails
            await bus.Publish(new StockUpdateFailed { OrderId = order.OrderId });

            // Retry stock update or take other appropriate actions
        }
    }
}