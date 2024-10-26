
using System.Data.Common;
using Dapper;

namespace WebAPI.DataAccess.Lib;

public class RandanonConnection : IDbConnection
{

    private DbConnection _dbConnection;
    private ISqlStringBuilder _sqlSB;

    public RandanonConnection()
    {
        _dbConnection = DbConnectionFactory.CreateDbConnection();
        _sqlSB = new SqlSB();
    }

    public async Task<bool> ContainsAsync<T>(string table, ISqlValue value)
    {
        await _dbConnection.OpenAsync();
        string query = _sqlSB.SelectString(table, value);
        List<T> list = (List<T>)await _dbConnection.QueryAsync<T>(query);
        await _dbConnection.CloseAsync();
        bool ans = list.Count != 0;
        return ans;
    }

    public async void InsertAsync(string table, ISqlModel model)
    {
        await _dbConnection.OpenAsync();
        string query = _sqlSB.InsertString(table, model);

        using(DbCommand command = DbCommandFactory.CreateDbCommand(query, _dbConnection))
        {
            await command.ExecuteNonQueryAsync();
        }

        await _dbConnection.CloseAsync();
    }

    public async void InsertAllAsync(string table, List<ISqlModel> models)
    {
        await _dbConnection.OpenAsync();

        foreach(ISqlModel model in models)
        {
            string query = _sqlSB.InsertString(table, model);
            using(DbCommand command = DbCommandFactory.CreateDbCommand(query, _dbConnection))
            {
                await command.ExecuteNonQueryAsync();
            }
        }

        await _dbConnection.CloseAsync();
    }

    public async Task<List<ISqlModel>> SelectAsync<T>(string table, ISqlValue value)
    {
        await _dbConnection.OpenAsync();
        await _dbConnection.CloseAsync();
        throw new NotImplementedException();
    }

    public async Task<List<ISqlModel>> SelectAsync<T>(string table, List<ISqlValue> values)
    {
        await _dbConnection.OpenAsync();
        await _dbConnection.CloseAsync();
        throw new NotImplementedException();
    }

    public async Task<List<ISqlModel>> SelectAllAsync<T>(string table)
    {
        await _dbConnection.OpenAsync();
        await _dbConnection.CloseAsync();
        throw new NotImplementedException();
    }
}