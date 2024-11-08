namespace WebAPI.DataAccess.Lib;

public interface ISqlStringBuilder
{
    public string SelectString(string table, ISqlValue value);
    public string SelectString(string table, List<ISqlValue> values);

    public string SelectAllString(string table);

    public string InsertString(string table, List<ISqlValue> values);

    public string DeleteString(string table, ISqlValue value);
    public string DeleteString(string table, List<ISqlValue> values);
}