namespace Sparkybit_Fibonacci.Services.Interfaces
{
    public interface IFibonacciService
    {
        public void FibonacciReverse(List<List<int>> data);
        public bool IsFibonacci(List<int> list);
        public void Reverse(List<int> list);

    }
}
