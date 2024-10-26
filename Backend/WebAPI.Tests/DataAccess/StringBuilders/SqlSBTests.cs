using Microsoft.Identity.Client;
using WebAPI.DataAccess.Lib;

namespace WebAPI.Tests.DataAccess.Lib;

public class TestSqlSB
{
    protected SqlSB _sb;
    protected string _table;

    public TestSqlSB()
    {
        _sb = new SqlSB();
        _table = "test_table";
    }
}

public class SqlSB_DeleteString_SingleValue : TestSqlSB
{
    [Fact]
    public void DeleteString_IntValue()
    {
        string result = _sb.DeleteString(_table, new IntSqlValue("number", 1));
        Assert.Equal($"DELETE FROM {_table} WHERE number=1;", result);
    }

    [Fact]
    public void DeleteString_StringValue()
    {
        string label = "str";
        string value = "test";
        string result = _sb.DeleteString(_table, new StringSqlValue(label, value));
        string expected = $"DELETE FROM {_table} WHERE {label}='{value}';";
        Assert.Equal(expected, result);
    }
}

public class SqlSB_DeleteString_ListOfValues : TestSqlSB
{
    [Fact]
    public void DeleteString_EmptyList_ThrowsException()
    {
        Assert.ThrowsAny<Exception>(
            () => {
                _sb.DeleteString(_table, []);
            }
        );
    }

    [Fact]
    public void DeleteString_SingleElement()
    {
        string label = "label";
        int value = 2;
        List<ISqlValue> values = [ new IntSqlValue(label, value) ];
        string result = _sb.DeleteString(_table, values);
        string expected = $"DELETE FROM {_table} WHERE {label}={value};";
        Assert.Equal(expected, result);
    }

    [Fact]
    public void DeleteString_MultipleElements()
    {
        string label1 = "label1";
        int value1 = 1;
        string label2 = "label2";
        string value2 = "2";
        string label3 = "label3";
        long value3 = 9282334899;
        IntSqlValue v1 = new IntSqlValue(label1, value1);
        StringSqlValue v2 = new StringSqlValue(label2, value2);
        LongSqlValue v3 = new LongSqlValue(label3, value3);
        List<ISqlValue> values = [ v1, v2, v3 ];
        string result = _sb.DeleteString(_table, values);
        string expected = $"DELETE FROM {_table} WHERE {label1}={value1} AND {label2}='{value2}' AND {label3}={value3};";
        Assert.Equal(expected, result);
    }
}

public class SqlSB_InsertString : TestSqlSB
{
    [Fact]
    public void InsertString_EmptyList_ThrowsException()
    {
        Assert.ThrowsAny<Exception>(
            () => {
                _sb.InsertString(_table, []);
            }
        );
    }

    [Fact]
    public void InsertString_SingleElement()
    {
        string label = "label";
        int value = 2;
        List<ISqlValue> values = [ new IntSqlValue(label, value) ];
        string result = _sb.InsertString(_table, values);
        string expected = $"INSERT INTO {_table} ({label}) VALUES ({value});";
        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertString_MultipleElements()
    {
        string label1 = "label1";
        int value1 = 1;
        string label2 = "label2";
        string value2 = "2";
        string label3 = "label3";
        long value3 = 9282334899;
        IntSqlValue v1 = new IntSqlValue(label1, value1);
        StringSqlValue v2 = new StringSqlValue(label2, value2);
        LongSqlValue v3 = new LongSqlValue(label3, value3);
        List<ISqlValue> values = [ v1, v2, v3 ];
        string result = _sb.InsertString(_table, values);
        string expected = $"INSERT INTO {_table} ({label1}, {label2}, {label3}) VALUES ({value1}, '{value2}', {value3});";
        Assert.Equal(expected, result);
    }
}

public class SqlSB_SelectString_SingleValue : TestSqlSB
{

}

public class SqlSB_SelectString_ListOfValues : TestSqlSB
{

}

public class SqlSB_SelectString_All : TestSqlSB
{

}