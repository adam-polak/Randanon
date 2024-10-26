namespace WebAPI.DataAccess.Lib;

public abstract class AbstractSqlValue : ISqlValue
{
    private string _name;

    public AbstractSqlValue(string name)
    {
        _name = name;
    }

    public string GetLabel()
    {
        return _name;
    }

    public string GetSqlEqualsString()
    {
        return $"{GetLabelString()}={GetValueSqlString()}";
    }

    public abstract string GetValueSqlString();
}