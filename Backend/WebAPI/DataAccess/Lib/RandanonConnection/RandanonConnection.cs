
using System.Data.Common;

namespace WebAPI.DataAccess.Lib;

public class RandanonConnection : IDbConnection
{

    private DbConnection _dbConnection;

    public RandanonConnection()
    {
        _dbConnection = DbConnectionFactory.CreateDbConnection();
    }

    public bool Contains(string table, string query)
    {
        throw new NotImplementedException();
    }

    public void Insert(string table, ISqlModel model)
    {
        throw new NotImplementedException();
    }

    public void InsertAll(string table, List<ISqlModel> models)
    {
        throw new NotImplementedException();
    }

    public List<ISqlModel> Select(string table, string query)
    {
        throw new NotImplementedException();
    }

    public List<ISqlModel> SelectAll(string table)
    {
        throw new NotImplementedException();
    }
}