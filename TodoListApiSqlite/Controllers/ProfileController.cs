using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoListApiSqlite.Data;
using TodoListApiSqlite.Dtos;
using TodoListApiSqlite.Exceptions;
using TodoListApiSqlite.Models;
using TodoListApiSqlite.RequestModel;
using TodoListApiSqlite.Services;

namespace TodoListApiSqlite.Controllers;

[ApiController]
[Authorize]
[Route("profile")]
public class ProfileController : ApiController
{
    private readonly UserService _userService;

    public ProfileController(TodoListApiContext context, UserService userService): base(context)
    {
        _userService = userService;
    }

    [HttpPut]
    public async Task<ActionResult<UserDto>> Edit([FromBody] UserModel model)
    {
        var user = _userService.Edit(model, GetUser());
        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return Ok(UserDto.Create(user));
    }

    [HttpPost]
    [Route("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] PasswordChangeModel model)
    {
        User user;
        try
        {
            user = _userService.ChangePassword(model, GetUser());
        }
        catch (PasswordNotCorrectException e)
        {
            return Conflict(e.Message);
        }
        _context.Users.Update(user);
        return Ok();
    }
    
}