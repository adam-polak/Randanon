namespace WebAPI.DataAccess.Lib;

public interface IDbConnection
{
    public List<T>SelectAll<T>(string table);

    public List<T> Select<T>(string table, ISqlValue value);
    public List<T> Select<T>(string table, List<ISqlValue> values);

    public bool Contains<T>(string table, ISqlValue value);

    public void Insert(string table, ISqlModel model);
    public void InsertAll(string table, List<ISqlModel> models);

    public bool Delete<T>(string table, ISqlValue value);
    public bool Delete<T>(string table, List<ISqlValue> values);
    public int DeleteAll<T>(string table, List<ISqlModel> models);
}