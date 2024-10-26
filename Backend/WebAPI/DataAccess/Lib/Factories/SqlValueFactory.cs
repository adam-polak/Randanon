namespace WebAPI.DataAccess.Lib;

public static class SqlValueFactory
{
    public static ISqlValue CreateSqlValue(string name, object? value)
    {
        if(value == null) throw new Exception("Value can't be null.");
        
        if(value is string) return CreateSqlValue(name, (string)value);
        else if(value is long) return CreateSqlValue(name, (long)value);
        else if(value is int) return CreateSqlValue(name, (int)value);
        else throw new Exception($"{value.GetType().Name} is not a supported type.");
    }

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