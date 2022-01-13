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
    private readonly IJWTAuthenticationManager _authenticationManager;

    public ProfileController(TodoListApiContext context, UserService userService, IJWTAuthenticationManager authenticationManager): base(context)
    {
        _userService = userService;
        _authenticationManager = authenticationManager;
    }

    [HttpPut]
    public async Task<ActionResult<UserDto>> Edit([FromBody] UserModel model)
    {
        if (model.Password != model.PasswordCheck)
        {
            return Conflict("Passwords not valid");
        }
        var token = _authenticationManager.Authenticate(GetUser().Email, model.Password);
        var user = _userService.Edit(model, GetUser());
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        var userDto = UserDto.Create(user);
        userDto.Token = token;
        return Ok(userDto);
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