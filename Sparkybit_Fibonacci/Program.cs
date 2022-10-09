using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Sparkybit_Fibonacci.Models;
using Sparkybit_Fibonacci.Models.Interfaces;
using Sparkybit_Fibonacci.Services.Implementations;
using Sparkybit_Fibonacci.Services.Interfaces;

namespace Sparkybit_Fibonacci
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<LogsDatabaseSettings>(
                builder.Configuration.GetSection(nameof(LogsDatabaseSettings)));

            builder.Services.AddSingleton<ILogsDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<LogsDatabaseSettings>>().Value);

            builder.Services.AddSingleton<IMongoClient>(s =>
                new MongoClient(builder.Configuration.GetValue<string>("LogsDatabaseSettings:ConnectionString")));

            builder.Services.AddControllers();
            builder.Services.AddScoped<IFibonacciService, FibonacciService>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}