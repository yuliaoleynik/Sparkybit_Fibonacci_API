namespace Sparkybit_Fibonacci.Models.Interfaces
{
    public interface ILogsDatabaseSettings
    {
        string LogsColectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
