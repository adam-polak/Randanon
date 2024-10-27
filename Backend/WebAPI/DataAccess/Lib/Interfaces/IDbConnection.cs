namespace WebAPI.DataAccess.Lib;

public interface IDbConnection
{
    public Task<List<T>> SelectAllAsync<T>(string table);

    public Task<List<T>> SelectAsync<T>(string table, ISqlValue value);
    public Task<List<T>> SelectAsync<T>(string table, List<ISqlValue> values);

    public Task<bool> ContainsAsync<T>(string table, ISqlValue value);

    public void InsertAsync(string table, ISqlModel model);
    public void InsertAllAsync(string table, List<ISqlModel> models);

    public Task<bool> DeleteAsync<T>(string table, ISqlValue value);
    public Task<bool> DeleteAsync<T>(string table, List<ISqlValue> values);
    public Task<int> DeleteAllAsync<T>(string table, List<ISqlModel> models);
}