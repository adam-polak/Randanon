
namespace WebAPI.DataAccess.Lib;

public class SqlSB : ISqlStringBuilder
{
    public string DeleteString(string table, ISqlValue value)
    {
        throw new NotImplementedException();
    }

    public string DeleteString(string table, List<ISqlValue> values)
    {
        throw new NotImplementedException();
    }

    public string InsertString(string table, ISqlModel model)
    {
        throw new NotImplementedException();
    }

    public string SelectString(string table, ISqlValue value)
    {
        throw new NotImplementedException();
    }

    public string SelectString(string table, List<ISqlValue> values)
    {
        throw new NotImplementedException();
    }
}