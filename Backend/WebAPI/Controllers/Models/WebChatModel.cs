namespace WebAPI.Controllers.Models;

public class WebChatModel
{
    public long UserID { get; set; }
    public long ChatNumber { get; set; }
    public required string Message { get; set; }
}