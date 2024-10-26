namespace WebAPI.DataAccess.Lib;

public interface IDbConnection
{
    private string _database;
    private string _connectionString;

    public List<ISqlModel> SelectAll(string table);
    public List<ISqlModel> Select(string table, string query);
    public bool Contains(string table, string query);
    public void Insert(string table, ISqlModel model);
    public void InsertAll(string table, List<ISqlModel> models);
}