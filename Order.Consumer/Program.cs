namespace Order.Consumer
{
    using System.Text;
    using System.Threading.Tasks;
    using Order.Contracts;
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var order = new Order
            {
                OrderId = "12345 voucher stock",
                CustomerName = "John Doe",
                ProductIds = new string[] { "P1", "P2", "P3" },
                VoucherCode = "ABC123"
            };

            var httpClient = new HttpClient();
            var apiUrl = "https://localhost:7184/orders";

            var content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(order),
                Encoding.UTF8,
                "application/json"
            );

            var response = await httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Order created successfully.");
            }
            else
            {
                Console.WriteLine("Failed to create the order. Status code: " + response.StatusCode);
            }

            Console.WriteLine("Done!");
        }
    }
}