
using HealthChecks.UI.Client;

namespace Catalog.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("Database");

            Console.WriteLine("====================================");
            Console.WriteLine($"DB CONNECTION: {connectionString}");
            Console.WriteLine("====================================");

           

            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(Program).Assembly);
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

            builder.Services.AddCarter();
            builder.Services.AddMarten(options =>
            {
                options.Connection(connectionString!);
            })
            .UseLightweightSessions();

            if(builder.Environment.IsDevelopment())
            {
                builder.Services.InitializeMartenWith<CatalogInitialData>();
            }

            builder.Services.AddExceptionHandler<CustomExceptionHandler>();


            builder.Services.AddHealthChecks()
                .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

            var app = builder.Build();


            
            app.MapCarter();

            app.UseExceptionHandler(options => { });

            app.UseHealthChecks("/health",
                new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

            app.Run();
        }
    }
}
