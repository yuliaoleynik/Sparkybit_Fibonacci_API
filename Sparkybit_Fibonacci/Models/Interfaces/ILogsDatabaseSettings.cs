namespace Sparkybit_Fibonacci.Models.Interfaces
{
    public class ILogsDatabaseSettings
    {
        string LogsColectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
