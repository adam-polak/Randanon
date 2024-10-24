namespace WebAPI.DataAccess.Lib;

public class SqlValueFactory
{
    public ISqlValue CreateSqlValue(string name, string value)
    {
        return new StringSqlValue(name, value);
    }

    public ISqlValue CreateSqlValue(string name, int value)
    {
        return new IntSqlValue(name, value);
    }

    public ISqlValue CreateSqlValue(string name, long value)
    {
        return new LongSqlValue(name, value);
    }
}