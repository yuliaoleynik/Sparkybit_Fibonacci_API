using Sparkybit_Fibonacci.Models.Interfaces;

namespace Sparkybit_Fibonacci.Models
{
    public class LogsDatabaseSettings : ILogsDatabaseSettings
    {
        public string LogsCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}
