using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPI.Controllers.Errors;
using WebAPI.DataAccess.Models;
using WebAPI.DataAccess.TableAccess;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using WebAPI.Controllers.Lib;
using System.Runtime.CompilerServices;
using WebAPI.Controllers.Models;

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

    private string GetJsonForChatModelList(List<ChatModel> chats) 
    {
        // Sort chats
        chats.Sort((x, y) => {
            if(x.ChatNumber < y.ChatNumber) return -1;
            else if(x.ChatNumber > y.ChatNumber) return 1;
            else return 0;
        });

        List<WebChatModel> webChatModels = [];
        foreach(ChatModel chat in chats)
        {
            webChatModels.Add(new WebChatModel() {
                ChatNumber = chat.ChatNumber,
                Message = chat.Message
            });
        }

        return JsonConvert.SerializeObject(webChatModels);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetChats()
    {
        AddHeader.AddCors(this);
        try {
            List<ChatModel> chats = await _chatTable.GetAllChats();
            string json = GetJsonForChatModelList(chats);
            return Ok(json);
        } catch(Exception e) {
            Console.WriteLine("Failed to get all chats...");
            Console.WriteLine(e.Message);
            return BadRequest("Failed");
        }
    }

    [HttpGet("/chats/{num}")]
    public async Task<IActionResult> GetChatsAbove(int num)
    {
        AddHeader.AddCors(this);
        try {
            List<ChatModel> chats = await _chatTable.GetChatsAbove(num);
            string json = GetJsonForChatModelList(chats);
            return Ok(json);
        } catch(Exception e) {
            Console.WriteLine("Failed to get chats above num...");
            Console.WriteLine(e.Message);
            return BadRequest("Failed");
        }
    }

    [HttpGet("count")]
    public async Task<IActionResult> GetChatCount()
    {
        AddHeader.AddCors(this);
        try {
            int count = await _chatTable.GetChatCount();
            return Ok(count);
        } catch(Exception e) {
            Console.WriteLine("Failed to get chat count...");
            Console.WriteLine(e.Message);
            return BadRequest("Failed");
        }
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendChat([FromBody] JsonObject j)
    {
        AddHeader.AddCors(this);
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
        AddHeader.AddCors(this);
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