namespace WebAPI.DataAccess.Lib;

public interface ISqlValue
{
    public string GetLabelString();
    public string GetValueSqlString();
    public string GetSqlEqualsString();
}