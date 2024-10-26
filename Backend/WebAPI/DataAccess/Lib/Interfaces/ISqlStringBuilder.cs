namespace WebAPI.DataAccess.Lib;

public interface ISqlStringBuilder
{
    public string SelectString(string table, ISqlValue value);
    public string SelectString(string table, List<ISqlValue> values);

    public string InsertString(string table, ISqlModel model);

    public string DeleteString(string table, ISqlValue value);
    public string DeleteString(string table, List<ISqlValue> values);
}