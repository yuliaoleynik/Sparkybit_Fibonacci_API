using MongoDB.Driver;
using Sparkybit_Fibonacci.Models;
using Sparkybit_Fibonacci.Services.Interfaces;

namespace Sparkybit_Fibonacci.Services.Implementations
{
    public class FibonacciService : IFibonacciService
    {
        private readonly IMongoCollection<Log> _mongoCollection;
        public FibonacciService(IMongoClient client, LogsDatabaseSettings settings)
        {
            var database = client.GetDatabase(settings.DatabaseName);
            _mongoCollection = database.GetCollection<Log>(settings.LogsColectionName);
        }

        public void FibonacciReverse(List<List<int>> data)
        {
            _mongoCollection.InsertOne(new Log { Date = DateTime.Now, Title = "FibonacciReverse", Description = $"Start to revers fibonacci rows." });

            foreach(List<int> row in data)
            {
                if (IsFibonacci(row))
                {
                    Reverse(row);
                }
            }
        }

        public bool IsFibonacci(List<int> list)
        {
            for (int i = 2; i < list.Count; i++)
            {
                if ((list[i - 1] + list[i - 2]) != list[i])
                    return false;
            }
            return true;
        }

        public void Reverse(List<int> list)
        {
            for (int i = 0; i < list.Count - i; i++)
            {
                var value = list[list.Count - i - 1];
                list[list.Count - i - 1] = list[i];
                list[i] = value;
            }
        }
    }
}
