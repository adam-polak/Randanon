using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPI.Controllers.Lib;
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
    public IActionResult CreateUser()
    {
        AddHeader.AddCors(this);
        try {
            UserModel user = _userTable.CreateUser();
            string json = JsonConvert.SerializeObject(user);
            return Ok(json);
        } catch(Exception e) {
            Console.WriteLine("Failed to create user...");
            Console.WriteLine(e.Message);
            return BadRequest("Failed to create user");
        }
    }

    [HttpPost("validuser")]
    public IActionResult ValidUser([FromBody] UserModel user)
    {
        AddHeader.AddCors(this);
        try {
            if(_userTable.ValidUser(user)) return Ok();
            else return Ok("false");
        } catch(Exception e) {
            Console.WriteLine("Failed to validate user...");
            Console.WriteLine(e.Message);
            return BadRequest("Failed");
        }
    }
}