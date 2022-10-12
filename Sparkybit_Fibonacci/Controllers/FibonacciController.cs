using Microsoft.AspNetCore.Mvc;
using Sparkybit_Fibonacci.Services.Interfaces;
using Sparkybit_Fibonacci.Models;
using MongoDB.Driver;
using Sparkybit_Fibonacci.Models.Interfaces;
using System.Text;

namespace Sparkybit_Fibonacci.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FibonacciController : ControllerBase
    {
        private readonly IFibonacciService _fibonacciService;
        private readonly IMongoCollection<Log> _mongoCollection; 

        public FibonacciController(IFibonacciService fibonacciService, ILogsDatabaseSettings settings, IMongoClient client)
        {
            _fibonacciService = fibonacciService;
            var database = client.GetDatabase(settings.DatabaseName);
            _mongoCollection = database.GetCollection<Log>(settings.LogsCollectionName);
        }

        [HttpPost("process")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            _mongoCollection.InsertOne(new Log { Date = DateTime.Now, Title = "HttpPost UploadFile", Description = $"Start to process the file." });

            var data = new StringBuilder();
            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                        data.AppendLine(reader.ReadLine());
                }

                var result = _fibonacciService.FibonacciReverse(data.ToString());

                _mongoCollection.InsertOne(new Log { Date = DateTime.Now, Title = "HttpPost UploadFile", Description = $"Successfuly processed the file." });
                return File(ListToStream(result), "application/txt", file.FileName);
            }
            catch
            {
                _mongoCollection.InsertOne(new Log { Date = DateTime.Now, Title = "HttpPost UploadFile", Description = $"File read error." });
                return BadRequest(new { massage = "The file is empty or incorrectly filled out." });
            }
        }

        private MemoryStream ListToStream(string? list)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(list ?? ""));
        }
    }
}
