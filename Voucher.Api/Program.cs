using MassTransit;
using Voucher.Api.Consumers;

namespace Voucher.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMassTransit(config =>
            {
                config.AddConsumer<VoucherRedeemRequestConsumer>(); // Add your consumers here

                // Configure the ActiveMQ transport
                config.UsingActiveMq((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });
            });


            // Add services to the container.

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}