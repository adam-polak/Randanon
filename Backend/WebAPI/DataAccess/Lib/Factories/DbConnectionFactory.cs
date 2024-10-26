using System.Data.Common;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace WebAPI.DataAccess.Lib;

public static class DbConnectionFactory
{
    public static DbConnection CreateDbConnection()
    {
        string connectionString;
        if(Environment.GetEnvironmentVariable("ConnectionString") == null)
        {
            connectionString = JsonReader.GetValue("ConnectionString");
            return new NpgsqlConnection(connectionString);
        } else {
            connectionString = Environment.GetEnvironmentVariable("ConnectionString") ?? "";
            return new SqlConnection(connectionString);
        }
    }
}