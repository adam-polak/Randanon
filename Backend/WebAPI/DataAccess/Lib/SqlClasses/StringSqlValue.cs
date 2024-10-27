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
        _value = ValidateString(value);
    }

    public static string ValidateString(string str)
    {
        char[] arr = str.ToCharArray();
        string validString = "";
        for(int i = 0; i < arr.Count(); i++)
        {
            if(IllegalValues.Contains($"{arr[i]}")) continue;
            else if(i < arr.Count() - 1 && IllegalValues.Contains($"{arr[i]}" + arr[i + 1])) continue;
            validString += arr[i];
        }

        return validString;
    }

    public override string GetValueSqlString()
    {
        return $"'{_value}'";
    }
}