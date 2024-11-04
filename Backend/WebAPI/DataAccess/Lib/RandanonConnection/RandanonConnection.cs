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

    private List<T> DoQuery<T>(string query)
    {
        _dbConnection.Open();
        List<T> ans = (List<T>)_dbConnection.Query<T>(query);
        _dbConnection.Close();
        return ans;
    }

    private int DoCommand(string query)
    {
        _dbConnection.Open();
        using(DbCommand command = DbCommandFactory.CreateDbCommand(query, _dbConnection))
        {
            command.ExecuteNonQuery();
        }
        _dbConnection.Close();

        return 1;
    }

    private int DoCommandNoClose(string query)
    {
        using(DbCommand command = DbCommandFactory.CreateDbCommand(query, _dbConnection))
        {
            command.ExecuteNonQuery();
        }

        return 1;
    }

    private bool ContainsNoClose<T>(string query)
    {
        List<T> list = (List<T>)_dbConnection.Query<T>(query);
        return list.Count != 0;
    }

    public bool Contains<T>(string table, ISqlValue value)
    {
        List<T> list = Select<T>(table, value);
        return list.Count != 0;
    }

    public bool Contains<T>(string table, List<ISqlValue> values)
    {
        List<T> list = Select<T>(table, values);
        return list.Count != 0;
    }

    public void Insert(string table, ISqlModel model)
    {
        string query = _sqlSB.InsertString(table, model.GetSqlValues());
        DoCommand(query);
    }

    public void InsertAll(string table, List<ISqlModel> models)
    {
        _dbConnection.Open();

        foreach(ISqlModel model in models)
        {
            string query = _sqlSB.InsertString(table, model.GetSqlValues());
            DoCommandNoClose(query);
        }

        _dbConnection.Close();
    }

    public List<T> Select<T>(string table, ISqlValue value)
    {
        ValidateType(typeof(T));
        string query = _sqlSB.SelectString(table, value);
        return DoQuery<T>(query);
    }

    public List<T> Select<T>(string table, List<ISqlValue> values)
    {
        ValidateType(typeof(T));
        string query = _sqlSB.SelectString(table, values);
        return DoQuery<T>(query);
    }

    public List<T> SelectAll<T>(string table)
    {
        ValidateType(typeof(T));
        string query = _sqlSB.SelectAllString(table);
        return DoQuery<T>(query);
    }

    public bool Delete<T>(string table, ISqlValue value)
    {
        ValidateType(typeof(T));
        if(!Contains<T>(table, value)) return false;
        string query = _sqlSB.DeleteString(table, value);
        DoCommand(query);
        return true;
    }

    public bool Delete<T>(string table, List<ISqlValue> values)
    {
        ValidateType(typeof(T));
        if(!Contains<T>(table, values)) return false;
        string query = _sqlSB.DeleteString(table, values);
        DoCommand(query);
        return true;
    }

    public int DeleteAll<T>(string table, List<ISqlModel> models)
    {
        ValidateType(typeof(T));

        int removed = 0;
        _dbConnection.Open();
        foreach(ISqlModel model in models)
        {
            string query = _sqlSB.DeleteString(table, model.GetSqlValues());
            if(ContainsNoClose<T>(query)) removed++;
            DoCommandNoClose(query);
        }
        _dbConnection.Close();

        return removed;
    }
}