using WebAPI.DataAccess.Lib;

namespace WebAPI.Tests.DataAccess.Lib;

public class JsonInfoRetriever_GetValue
{
    [Fact]
    public void GetValue_ValueExists()
    {
        Assert.NotEqual("", JsonInfoRetriever.GetValue("hide.json", "ConnectionString"));
    }

    [Fact]
    public void GetValue_ValueNotExist()
    {
        Assert.Equal("", JsonInfoRetriever.GetValue("hide.json", "Fake value"));
    }
}