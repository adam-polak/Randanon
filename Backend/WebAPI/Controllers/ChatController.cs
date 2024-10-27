using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPI.Controllers.Errors;
using WebAPI.DataAccess.Models;
using WebAPI.DataAccess.TableAccess;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;

namespace WebAPI.Controllers;

[EnableCors("corspolicy")]
[ApiController]
[Route("/chat")]
public class ChatController : ControllerBase
{

    UserTableAccess _userTable;
    ChatTableAccess _chatTable;

    public ChatController()
    {
        _userTable = new UserTableAccess();
        _chatTable = new ChatTableAccess();
    }

    private UserModel GetUserFromJson(JObject json)
    {
        JToken? userObj = json["User"];
        if(userObj == null) throw new Exception("Json body is missing User object");

        UserModel? user = JsonConvert.DeserializeObject<UserModel>(userObj.ToString() ?? "");
        if(user == null) throw new Exception("User object in Json body is incorrectly formatted");

        return user;
    }

    private string GetMessageFromJson(JObject json)
    {
        JToken? token = json["Message"];
        if(token == null) throw new Exception("Json body is missing Message key");

        string? message = token.Value<string>();
        if(message == null || message.Count() == 0) throw new Exception("Message value is empty");

        return message;
    }

    private long GetChatNumberFromJson(JObject json)
    {
        JToken? token = json["ChatNumber"];
        if(token == null) throw new Exception("Json body is missing ChatNumber key");

        long chatNumber = token.Value<long>();
        return chatNumber;
    }

    private JObject GetJObject(JsonObject json)
    {
        return JObject.Parse(json.ToString());
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendChat([FromBody] JsonObject j)
    {
        try {
            JObject json = GetJObject(j);

            UserModel user = GetUserFromJson(json);
            if(!await _userTable.ValidUserAsync(user)) return Ok(ErrorMessages.INVALID_USER);

            string message = GetMessageFromJson(json);
            _chatTable.SendChatAsync(user, message);
            return Ok();
        } catch(Exception e) {
            Console.WriteLine("Failed to send chat...");
            Console.WriteLine(e.Message);
            return BadRequest("Failed");
        }
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteChat([FromBody] JsonObject j)
    {
        try {
            JObject json = GetJObject(j);
            UserModel user = GetUserFromJson(json);
            if(!await _userTable.ValidUserAsync(user)) return Ok(ErrorMessages.INVALID_USER);

            long chatNumber = GetChatNumberFromJson(json);

            bool deleted = await _chatTable.DeleteChat(user, chatNumber);

            if(deleted) return Ok();
            else return Ok(ErrorMessages.NONE_TO_DELETE);

        } catch(Exception e) {
            Console.WriteLine("Failed to delete chat...");
            Console.WriteLine(e.Message);
            return BadRequest("Failed");
        }
    }

}