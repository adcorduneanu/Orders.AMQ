namespace Voucher.Api.Consumers
{
    using MassTransit;
    using Order.Contracts;
    using Voucher.Contracts;

    public class VoucherRedeemRequestConsumer : IConsumer<VoucherRedeemRequest>
    {
        public async Task Consume(ConsumeContext<VoucherRedeemRequest> context)
        {
            // Perform voucher redemption logic
            var voucherRedeemedSuccessfully = await RedeemVoucher(context.Message.OrderId);

            // Send the response
            await context.RespondAsync(new VoucherRedeemResponse { OrderId = context.Message.OrderId, VoucherRedeemedSuccessfully = voucherRedeemedSuccessfully });
        }

        private async Task<bool> RedeemVoucher(string orderId)
        {
            // Perform the voucher redemption logic
            // Return true if the voucher was redeemed successfully, false otherwise
            // You can replace this with your actual voucher redemption logic
            return await Task.FromResult(orderId.Contains("voucher"));
        }
    }
}
