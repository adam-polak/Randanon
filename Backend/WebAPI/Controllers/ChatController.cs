using System.Text.Json;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPI.DataAccess.Models;
using WebAPI.DataAccess.TableAccess;

namespace WebAPI.Controllers;

[EnableCors("corspolicy")]
[ApiController]
[Route("/user")]
public class ChatController : ControllerBase
{

    [HttpPost("sendchat")]
    public async Task<IActionResult> SendChat([FromBody] JsonElement json)
    {
        //need to parse json from body
        return Ok();
    }

}