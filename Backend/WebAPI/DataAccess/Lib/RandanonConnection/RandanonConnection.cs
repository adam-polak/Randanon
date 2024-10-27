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

    public async Task<bool> ContainsAsync<T>(string table, ISqlValue value)
    {
        ValidateType(typeof(T));

        List<T> list = await SelectAsync<T>(table, value);
        return list.Count != 0;
    }

    public async Task<bool> ContainsAsync<T>(string table, ISqlModel model)
    {
        ValidateType(typeof(T));
        List<T> list = await SelectAsync<T>(table, model.GetSqlValues());
        return list.Count != 0;
    }

    public async void InsertAsync(string table, ISqlModel model)
    {
        await _dbConnection.OpenAsync();
        string query = _sqlSB.InsertString(table, model.GetSqlValues());

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
            string query = _sqlSB.InsertString(table, model.GetSqlValues());
            using(DbCommand command = DbCommandFactory.CreateDbCommand(query, _dbConnection))
            {
                await command.ExecuteNonQueryAsync();
            }
        }

        await _dbConnection.CloseAsync();
    }

    public async Task<List<T>> SelectAsync<T>(string table, ISqlValue value)
    {
        ValidateType(typeof(T));

        await _dbConnection.OpenAsync();
        string query = _sqlSB.SelectString(table, value);
        List<T> list = (List<T>)await _dbConnection.QueryAsync<T>(query);

        await _dbConnection.CloseAsync();

        return list;
    }

    public async Task<List<T>> SelectAsync<T>(string table, List<ISqlValue> values)
    {
        ValidateType(typeof(T));

        await _dbConnection.OpenAsync();

        string query = _sqlSB.SelectString(table, values);
        List<T> list = (List<T>)await _dbConnection.QueryAsync<T>(query);

        await _dbConnection.CloseAsync();
        
        return list;
    }

    public async Task<List<T>> SelectAllAsync<T>(string table)
    {
        ValidateType(typeof(T));

        await _dbConnection.OpenAsync();

        string query = _sqlSB.SelectAllString(table);
        List<T> list = (List<T>)await _dbConnection.QueryAsync<T>(query);

        await _dbConnection.CloseAsync();
        
        return list;
    }
}