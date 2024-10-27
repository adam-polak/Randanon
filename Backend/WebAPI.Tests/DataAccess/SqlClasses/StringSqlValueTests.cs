using WebAPI.DataAccess.Lib;

namespace WebAPI.Tests.DataAccess.Lib;

public class StringSqlValue_GetValueSqlString
{
    private StringSqlValue _value;

    public StringSqlValue_GetValueSqlString()
    {
        _value = new StringSqlValue("name", "test");
    }

    private bool BeginsAndEndsWithSingleQuote(string str)
    {
        return str.ElementAt(0) == '\'' && str.ElementAt(str.Count() - 1) == '\'';
    }

    private string GetStringNestedInQuotes(string str)
    {
        return str.Substring(1, str.Count() - 2);
    }

    [Fact]
    public void GetValueSqlString_Has2SingleQuotes()
    {
        string result = _value.GetValueSqlString();
        Assert.True(BeginsAndEndsWithSingleQuote(result), $"Expected: '<string>' Actual: {result}");
    }

    [Theory]
    [InlineData("test")]
    [InlineData(" ")]
    [InlineData("")]
    public void GetValueSqlString_InputIsString_ReturnsStringInQuotes(string value)
    {
        StringSqlValue temp = new StringSqlValue("name", value);
        string result = temp.GetValueSqlString();
        string nested = GetStringNestedInQuotes(result);
        Assert.True(value.Equals(nested), $"Nested string should be {value} was {nested}");
    }
}

public class StringSqlValue_ValidateString
{
    [Fact]
    public void VerifyValueString_InputIllegal_ReturnFalse()
    {
        foreach(string x in StringSqlValue.IllegalValues)
        {
            string result = StringSqlValue.ValidateString(x);
            Assert.True(!result.Contains(x), $"Result: {result} should not contain Illegal Value: {x}");
        }
    }

    [Fact]
    public void VerifyValueString_InputIllegalAtEndOfString_ReturnFalse()
    {
        string value = "test";
        foreach(string x in StringSqlValue.IllegalValues)
        {
            string input = value + x;
            string result = StringSqlValue.ValidateString(input);
            Assert.True(!result.Contains(x), $"Result: {result} should not contain Illegal Value: {x}");
        }
    }

    [Fact]
    public void VerifyValueString_InputIllegalInString_ReturnFalse()
    {
        string val1 = "te";
        string val2 = "st";
        foreach(string x in StringSqlValue.IllegalValues)
        {
            string input = val1 + x + val2;
            string result = StringSqlValue.ValidateString(input);
            Assert.True(!result.Contains(x), $"Result: {result} should not contain Illegal Value: {x}");
        }
    }

}