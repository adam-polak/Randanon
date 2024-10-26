using WebAPI.DataAccess.Lib;

namespace WebAPI.DataAccess.Models;

public class ChatModel : ISqlModel
{
    public long UserID { get; set; }
    public long ChatNumber { get; set; }
    public required string ChatName { get; set; }
    public required string Message { get; set; }

    public List<ISqlValue> GetSqlValues()
    {
        throw new NotImplementedException();
    }
}