namespace WebAPI.DataAccess.Lib;

public class LongSqlValue : AbstractSqlValue
{
    private long _value;

    public LongSqlValue(string name, long value) : base(name)
    {
        _value = value;
    }

    public override string GetValueSqlString()
    {
        return $"{_value}";
    }
}