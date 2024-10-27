using System.Data.Common;
using Dapper;

namespace WebAPI.DataAccess.Lib;

public static class RandanonQueryCache
{
    //implement dictionary that contains cache for queries, so that connection only needs to
    //query above the highest id
}

public class RandanonConnection : IDbConnection
{

    private DbConnection _dbConnection;
    private ISqlStringBuilder _sqlSB;

    public RandanonConnection()
    {
        _dbConnection = DbConnectionFactory.CreateDbConnection();
        _sqlSB = new SqlSB();
    }

    private bool IsSqlModel(Type t)
    {
        return typeof(ISqlModel).IsAssignableFrom(t);
    }

    private void ValidateType(Type t)
    {
        if(!IsSqlModel(t)) throw new Exception("Type T must implement ISqlModel");
    }

    private async Task<List<T>> DoQuery<T>(string query)
    {
        await _dbConnection.OpenAsync();
        List<T> ans = (List<T>)await _dbConnection.QueryAsync<T>(query);
        await _dbConnection.CloseAsync();
        return ans;
    }

    private async Task<int> DoCommand(string query)
    {
        await _dbConnection.OpenAsync();
        using(DbCommand command = DbCommandFactory.CreateDbCommand(query, _dbConnection))
        {
            await command.ExecuteNonQueryAsync();
        }
        await _dbConnection.CloseAsync();

        return 1;
    }

    private async Task<int> DoCommandNoClose(string query)
    {
        using(DbCommand command = DbCommandFactory.CreateDbCommand(query, _dbConnection))
        {
            await command.ExecuteNonQueryAsync();
        }

        return 1;
    }

    private async Task<bool> ContainsNoClose<T>(string query)
    {
        List<T> list = (List<T>)await _dbConnection.QueryAsync<T>(query);
        return list.Count != 0;
    }

    public async Task<bool> ContainsAsync<T>(string table, ISqlValue value)
    {
        List<T> list = await SelectAsync<T>(table, value);
        return list.Count != 0;
    }

    public async Task<bool> ContainsAsync<T>(string table, List<ISqlValue> values)
    {
        List<T> list = await SelectAsync<T>(table, values);
        return list.Count != 0;
    }

    public async void InsertAsync(string table, ISqlModel model)
    {
        string query = _sqlSB.InsertString(table, model.GetSqlValues());
        await DoCommand(query);
    }

    public async void InsertAllAsync(string table, List<ISqlModel> models)
    {
        await _dbConnection.OpenAsync();

        foreach(ISqlModel model in models)
        {
            string query = _sqlSB.InsertString(table, model.GetSqlValues());
            await DoCommandNoClose(query);
        }

        await _dbConnection.CloseAsync();
    }

    public async Task<List<T>> SelectAsync<T>(string table, ISqlValue value)
    {
        ValidateType(typeof(T));
        string query = _sqlSB.SelectString(table, value);
        return await DoQuery<T>(query);
    }

    public async Task<List<T>> SelectAsync<T>(string table, List<ISqlValue> values)
    {
        ValidateType(typeof(T));
        string query = _sqlSB.SelectString(table, values);
        return await DoQuery<T>(query);
    }

    public async Task<List<T>> SelectAllAsync<T>(string table)
    {
        ValidateType(typeof(T));
        string query = _sqlSB.SelectAllString(table);
        return await DoQuery<T>(query);
    }

    public async Task<bool> DeleteAsync<T>(string table, ISqlValue value)
    {
        ValidateType(typeof(T));
        if(!await ContainsAsync<T>(table, value)) return false;
        string query = _sqlSB.DeleteString(table, value);
        await DoCommand(query);
        return true;
    }

    public async Task<bool> DeleteAsync<T>(string table, List<ISqlValue> values)
    {
        ValidateType(typeof(T));
        if(!await ContainsAsync<T>(table, values)) return false;
        string query = _sqlSB.DeleteString(table, values);
        await DoCommand(query);
        return true;
    }

    public async Task<int> DeleteAllAsync<T>(string table, List<ISqlModel> models)
    {
        ValidateType(typeof(T));

        int removed = 0;
        await _dbConnection.OpenAsync();
        foreach(ISqlModel model in models)
        {
            string query = _sqlSB.DeleteString(table, model.GetSqlValues());
            if(await ContainsNoClose<T>(query)) removed++;
            await DoCommandNoClose(query);
        }
        await _dbConnection.CloseAsync();

        return removed;
    }
}