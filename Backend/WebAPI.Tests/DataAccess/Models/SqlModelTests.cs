using WebAPI.DataAccess.Lib;
using WebAPI.DataAccess.Models;

namespace WebAPI.Tests.DataAccess.Lib;

public class UserModel_GetSqlValues
{
    [Fact]
    public void GetSqlValues_UserModel()
    {
        UserModel m = new UserModel() { ID = 123, Key = 456 };
        List<ISqlValue> values = m.GetSqlValues();
        ISqlValue v = values.ElementAt(0);
        Assert.Equal("ID", v.GetLabelString());
        Assert.Equal($"{m.ID}", v.GetValueSqlString());
        v = values.ElementAt(1);
        Assert.Equal("Key", v.GetLabelString());
        Assert.Equal($"{m.Key}", v.GetValueSqlString());
    }
}