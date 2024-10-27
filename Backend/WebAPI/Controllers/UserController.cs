using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPI.DataAccess.Models;
using WebAPI.DataAccess.TableAccess;

namespace WebAPI.Controllers;

[EnableCors("corspolicy")]
[ApiController]
[Route("/user")]
public class UserController : ControllerBase
{
    private UserTableAccess _userTable;

    public UserController()
    {
        _userTable = new UserTableAccess();
    }

    [HttpPost("createuser")]
    public async Task<IActionResult> CreateUser()
    {
        try {
            UserModel user = await _userTable.CreateUserAsync();
            string json = JsonConvert.SerializeObject(user);
            return Ok(json);
        } catch(Exception e) {
            Console.WriteLine("Failed to create user...");
            Console.WriteLine(e.Message);
            return BadRequest("Failed to create user");
        }
    }

    [HttpGet("validuser")]
    public async Task<IActionResult> ValidUser([FromBody] UserModel user)
    {
        try {
            if(await _userTable.ValidUserAsync(user)) return Ok();
            else return Ok("false");
        } catch(Exception e) {
            Console.WriteLine("Failed to validate user...");
            Console.WriteLine(e.Message);
            return BadRequest("Failed");
        }
    }

}