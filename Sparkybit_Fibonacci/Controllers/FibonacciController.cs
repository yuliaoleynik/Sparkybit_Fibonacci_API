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
        public FibonacciController(IFibonacciService fibonacciService, ILogsDatabaseSettings settings, IMongoClient client)
        {
            _fibonacciService = fibonacciService;
            var database = client.GetDatabase(settings.DatabaseName);
            _mongoCollection = database.GetCollection<Log>(settings.LogsColectionName);
        }

        [HttpPost("process")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            _mongoCollection.InsertOne(new Log { Date = DateTime.Now, Title = "HttpPost UploadFile", Description = $"Start to process the file." });

            var data = new List<List<int>>();
            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    string fileContent = reader.ReadToEnd();
                    string[] lines = fileContent.Split("\r\n");

                    for (int i = 0; i < lines.Length; i++)
                    {
                        data.Add(new List<int>());
                        string[] splittedLines = lines[i].Split(' ');
                        for (int j = 0; j < splittedLines.Length; j++)
                        {
                            data[i].Add(Convert.ToInt32(splittedLines[j]));
                        }
                    }
                }
                _fibonacciService.FibonacciReverse(data);
                _mongoCollection.InsertOne(new Log { Date = DateTime.Now, Title = "HttpPost UploadFile", Description = $"Successfuly processed the file." });
                return File();
            }
            catch
            {
                _mongoCollection.InsertOne(new Log { Date = DateTime.Now, Title = "HttpPost UploadFile", Description = $"File read error." });
            }
        }
    }
}
