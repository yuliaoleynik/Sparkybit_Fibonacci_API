using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sparkybit_Fibonacci.Services.Implementations;
using Sparkybit_Fibonacci.Services.Interfaces;
using Sparkybit_Fibonacci.Models;
using MongoDB.Driver;
using Sparkybit_Fibonacci.Models.Interfaces;

namespace Sparkybit_Fibonacci.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FibonacciController : ControllerBase
    {
        private readonly IFibonacciService _fibonacciService;
        private readonly IMongoCollection<Log> _mongoCollection;
        public FibonacciController(IFibonacciService fibonacciService, LogsDatabaseSettings settings, IMongoClient client)
        {
            _fibonacciService = fibonacciService;
            var database = client.GetDatabase(settings.DatabaseName);
            _mongoCollection = database.GetCollection<Log>(settings.LogsColectionName);
            _mongoCollection.InsertOne(new Log { Date = DateTime.Now,  Title = "Connected", Description = $"Succsses connection" });
        }

        [HttpPost("process")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            
        }
    }
}
