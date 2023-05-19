namespace Mail.Contracts
{
    public sealed class OrderConfirmationMail
    {
        public string OrderId { get; set; }
        public string Customer { get; set; }
    }
}