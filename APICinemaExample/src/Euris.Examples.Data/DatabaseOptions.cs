using Euris.Examples.Common.Repositories;

namespace Euris.Examples.Data;

public class DatabaseOptions : IDatabaseOptions
{
    public DatabaseOptions(string connectionString)
    {
        ConnectionString = connectionString;
    }
    public string ConnectionString { get; private set; }
}