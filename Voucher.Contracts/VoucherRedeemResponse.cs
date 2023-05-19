namespace Voucher.Contracts
{
    public sealed class VoucherRedeemResponse
    {
        public bool VoucherRedeemedSuccessfully { get; set; }
        public string OrderId { get; set; }
    }
}