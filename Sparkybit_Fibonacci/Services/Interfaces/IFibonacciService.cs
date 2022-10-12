namespace Sparkybit_Fibonacci.Services.Interfaces
{
    public interface IFibonacciService
    {
        public string FibonacciReverse(string data);
        public bool IsFibonacci(int[] line);
        public void Reverse(int[] line);

    }
}
