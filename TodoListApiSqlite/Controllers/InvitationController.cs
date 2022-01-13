using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoListApiSqlite.Data;
using TodoListApiSqlite.Models;
using TodoListApiSqlite.Repositories;
using TodoListApiSqlite.RequestModel;

namespace TodoListApiSqlite.Controllers;

[ApiController]
[Authorize]
[Route("invitation")]
public class InvitationController : ApiController
{
    private readonly GroupUserRepository _groupUserRepository;

    public InvitationController(TodoListApiContext context, GroupUserRepository groupUserRepository) : base(context)
    {
        _groupUserRepository = groupUserRepository;
    }
    
    [HttpPost]
    public async Task<ActionResult> Index([FromBody] InvitationModel model)
    {
        var user = _context.Users.Where(u => u.InvitationLink == model.InvitationLink).SingleOrDefault();
        if (user == null)
        {
            return Conflict("Invitation link is not correct");
        }
        var group = _context.Groups.Find(model.GroupId);
        if (group == null)
        {
            return Conflict("Group is not correct");
        }
        if (group.AdministratorId != GetUser().Id)
        {
            return Conflict("User has no permission to edit group");
        }
        var groupUser = new GroupUser();
        groupUser.GroupId = group.Id;
        groupUser.UserId = user.Id;
        _context.GroupUsers.Add(groupUser);
        await _context.SaveChangesAsync();

        return Ok();
    }
}