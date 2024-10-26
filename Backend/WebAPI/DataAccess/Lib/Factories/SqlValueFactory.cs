namespace WebAPI.DataAccess.Lib;

public static class SqlValueFactory
{
    public static ISqlValue CreateSqlValue(string name, string value)
    {
        return new StringSqlValue(name, value);
    }

    public static ISqlValue CreateSqlValue(string name, int value)
    {
        return new IntSqlValue(name, value);
    }

    public static ISqlValue CreateSqlValue(string name, long value)
    {
        return new LongSqlValue(name, value);
    }
}