﻿namespace Stock.Contracts.DTO
{
    public sealed class StockUpdateResponse
    {
        public bool StockUpdatedSuccessfully { get; set; }
        public string OrderId { get; set; }
    }
}