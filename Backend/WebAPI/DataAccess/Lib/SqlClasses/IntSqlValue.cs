namespace WebAPI.DataAccess.Lib;

public class IntSqlValue : AbstractSqlValue
{
    private int _value;

    public IntSqlValue(string name, int value) : base(name)
    {
        _value = value;
    }

    public override string GetValueSqlString()
    {
        return $"{_value}";
    }
}