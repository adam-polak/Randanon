using WebAPI.DataAccess.Lib;

namespace WebAPI.Tests.DataAccess.Lib;

public class SqlValueFactory_CreateSqlValue
{
    SqlValueFactory _factory;

    public SqlValueFactory_CreateSqlValue()
    {
        _factory = new SqlValueFactory();
    }

    private bool IsSqlType<T>(ISqlValue value)
    {
        return value.GetType() == typeof(T);
    }

    [Theory]
    [InlineData("")]
    [InlineData("test")]
    public void CreateSqlValue_InputIsString_ReturnStringSqlValue(string value)
    {
        var result = _factory.CreateSqlValue("name", value);
        Assert.True(IsSqlType<StringSqlValue>(result), $"{result.GetType()} should be {typeof(StringSqlValue)}");
    }

    [Theory]
    [InlineData(1)]
    [InlineData(-1)]
    public void CreateSqlValue_InputIsInt_ReturnIntSqlValue(int value)
    {
        var result = _factory.CreateSqlValue("name", value);
        Assert.True(IsSqlType<IntSqlValue>(result), $"{result.GetType()} should be {typeof(IntSqlValue)}");
    }

    [Theory]
    [InlineData(-999999999999)]
    [InlineData(10000000000000)]
    public void CreateSqlValue_InputIsLong_ReturnLongSqlValue(long value)
    {
        var result = _factory.CreateSqlValue("name", value);
        Assert.True(IsSqlType<LongSqlValue>(result), $"{result.GetType()} should be {typeof(LongSqlValue)}");
    }
}