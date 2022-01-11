using System.Configuration;
using Microsoft.AspNetCore.Mvc;
using TodoListApiSqlite.Data;
using TodoListApiSqlite.RequestModel;
using TodoListApiSqlite.Services;
using TodoListApiSqlite.Validators;

namespace TodoListApiSqlite.Controllers;

[ApiController]
[Route("register")]
public class RegisterController : ApiController
{
    private readonly UserService _userService;
    private readonly GroupService _groupService;

    public RegisterController(TodoListApiContext context, UserService userService, GroupService groupService) : base(context)
    {
        _userService = userService;
        _groupService = groupService;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        if (!RegisterModelValidator.Validate(model))
        {
            return BadRequest("Something went wrong, try again later");
        }
        if (_context.Users.Where(u => u.Email == model.Email).FirstOrDefault() != null)
        {
            return BadRequest("Email is already registered");
        }
        _userService.Create(model);
        
        return Ok();
    }
}