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
    private UserTableAccess _userAccess;

    public UserController()
    {
        _userAccess = new UserTableAccess();
    }

    [HttpPost("createuser")]
    public async Task<IActionResult> CreateUser()
    {
        try {
            UserModel user = await _userAccess.CreateUser();
            string json = JsonConvert.SerializeObject(user);
            return Ok(json);
        } catch(Exception e) {
            Console.WriteLine("Failed to create user...");
            Console.WriteLine(e.Message);
            return BadRequest("Failed to create user...");
        }
    }

}