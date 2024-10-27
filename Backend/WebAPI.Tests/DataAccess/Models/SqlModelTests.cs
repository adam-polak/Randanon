using WebAPI.DataAccess.Lib;
using WebAPI.DataAccess.Models;

namespace WebAPI.Tests.DataAccess.Lib;

public class UserModel_GetSqlValues
{
    [Fact]
    public void GetSqlValues_UserModel()
    {
        UserModel m = new UserModel() { ID = 123, UserKey = 456 };
        List<ISqlValue> values = m.GetSqlValues();
        ISqlValue v = values.ElementAt(0);
        Assert.Equal("ID", v.GetLabelString());
        Assert.Equal($"{m.ID}", v.GetValueSqlString());
        v = values.ElementAt(1);
        Assert.Equal("UserKey", v.GetLabelString());
        Assert.Equal($"{m.UserKey}", v.GetValueSqlString());
    }
}

public class ChatModel_GetSqlValues
{
    [Fact]
    public void GetSqlValues_ChatModel()
    {
        ChatModel m = new ChatModel() { UserID = 123, ChatNumber = 456, Message = "laalala" };
        List<ISqlValue> values = m.GetSqlValues();
        ISqlValue v = values.ElementAt(0);
        Assert.Equal("UserID", v.GetLabelString());
        Assert.Equal($"{m.UserID}", v.GetValueSqlString());
        v = values.ElementAt(1);
        Assert.Equal("ChatNumber", v.GetLabelString());
        Assert.Equal($"{m.ChatNumber}", v.GetValueSqlString());
        v = values.ElementAt(2);
        Assert.Equal("Message", v.GetLabelString());
        Assert.Equal($"'{m.Message}'", v.GetValueSqlString());
    }
}