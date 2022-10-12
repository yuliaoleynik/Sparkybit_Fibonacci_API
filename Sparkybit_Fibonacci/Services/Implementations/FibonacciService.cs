using MongoDB.Driver;
using Sparkybit_Fibonacci.Services.Interfaces;

namespace Sparkybit_Fibonacci.Services.Implementations
{
    public class FibonacciService : IFibonacciService
    {
        public FibonacciService() { }

        public string FibonacciReverse(string data)
        {
            List<string> result = new();
            List<string> lines = data.Split("\r\n").ToList();

            foreach (string line in lines)
            {
                var array = line.Split(' ').Where(n => !string.IsNullOrWhiteSpace(n)).Select(n => Convert.ToInt32(n.Trim())).ToArray();

                if (IsFibonacci(array))
                {
                    Reverse(array);
                }

                result.Add(string.Join(' ', array));
            }
            return string.Join("\r\n", result);
        }

        public bool IsFibonacci(int[] line)
        {
            for (int i = 2; i < line.Length; i++)
            {
                if ((line[i - 1] + line[i - 2]) != line[i])
                    return false;
            }
            return true;
        }

        public void Reverse(int[] line)
        {
            for (int i = 0; i < line.Length - i; i++)
            {
                var value = line[line.Length - i - 1];
                line[line.Length - i - 1] = line[i];
                line[i] = value;
            }
        }
    }
}
