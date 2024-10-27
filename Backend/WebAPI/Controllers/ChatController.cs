using System.Text.Json;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPI.Controllers.Errors;
using WebAPI.DataAccess.Models;
using WebAPI.DataAccess.TableAccess;
using WebAPI.Controllers.Models;

namespace WebAPI.Controllers;

[EnableCors("corspolicy")]
[ApiController]
[Route("/user")]
public class ChatController : ControllerBase
{

    UserTableAccess _userTable;
    ChatTableAccess _chatTable;

    public ChatController()
    {
        _userTable = new UserTableAccess();
        _chatTable = new ChatTableAccess();
    }

    [HttpPost("sendchat")]
    // Have to fix, from body can only be bound to one variable, need to bind it to a json object instead
    public async Task<IActionResult> SendChat([FromBody] UserModel user, [FromBody] MessageModel message)
    {
        try {
            if(!await _userTable.ValidUserAsync(user)) return BadRequest(ErrorMessages.INVALID_USER);

            return Ok();
        } catch(Exception e) {
            Console.WriteLine("Failed to send chat...");
            Console.WriteLine(e.Message);
            return BadRequest("Failed to send chat");
        }
    }

}