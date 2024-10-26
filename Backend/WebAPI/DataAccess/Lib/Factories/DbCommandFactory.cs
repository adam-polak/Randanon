using System.Data.Common;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace WebAPI.DataAccess.Lib;

public static class DbCommandFactory
{
    public static DbCommand CreateDbCommand(string query, DbConnection connection)
    {
        if(connection.GetType() == typeof(NpgsqlConnection))
        {
            return new NpgsqlCommand(query, (NpgsqlConnection)connection);
        } else {
            return new SqlCommand(query, (SqlConnection)connection);
        }
    }
}