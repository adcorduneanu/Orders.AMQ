namespace Mail.Api.Consumers
{
    using Mail.Contracts;
    using MassTransit;

    public class OrderConfirmationMailConsumer : IConsumer<OrderConfirmationMail>
    {
        public async Task Consume(ConsumeContext<OrderConfirmationMail> context)
        {
            var order = context.Message;
            Console.WriteLine($"Order {order.OrderId}: Notifying mail system");
            // Logic to notify the mail system about the order
        }
    }
}
