using WebAPI.DataAccess.Lib;

namespace WebAPI.DataAccess.Models;

public class ChatModel : ISqlModel
{
    public long UserID { get; set; }
    public long ChatNumber { get; set; }
    public required string Message { get; set; }

    public List<ISqlValue> GetSqlValues()
    {
        List<ISqlValue> values = [
            SqlValueFactory.CreateSqlValue(nameof(UserID), UserID),
            SqlValueFactory.CreateSqlValue(nameof(ChatNumber), ChatNumber),
            SqlValueFactory.CreateSqlValue(nameof(Message), Message)
        ];
        
        return values;
    }
}