namespace WebAPI.DataAccess.Lib;

public class StringSqlValue : AbstractSqlValue
{
    private string _value;

    public static readonly HashSet<string> IllegalValues =
    [
        "'",
        ";",
        "--",
        "#",
        "/",
        "*",
        "!",
        "="
    ];

    public StringSqlValue(string name, string value) : base(name)
    {
        _value = value;
        //todo: verify string is legal input
    }

    public static bool VerifyValueString(string str)
    {
        return false;
    }

    public override string GetValueSqlString()
    {
        return $"'{_value}'";
    }
}