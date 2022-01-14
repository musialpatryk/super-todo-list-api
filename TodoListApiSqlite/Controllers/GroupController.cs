using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using TodoListApiSqlite.Data;
using TodoListApiSqlite.Dtos;
using TodoListApiSqlite.Models;
using TodoListApiSqlite.Repositories;
using TodoListApiSqlite.RequestModel;
using TodoListApiSqlite.Services;

namespace TodoListApiSqlite.Controllers;

[ApiController]
[Route("group")]
[Authorize]
public class GroupController : ApiController
{

    private readonly GroupService _groupService;
    private readonly GroupUserRepository _groupUserRepository;

    public GroupController(TodoListApiContext context, GroupService groupService, GroupUserRepository groupUserRepository) : base(context)
    {
        _groupService = groupService;
        _groupUserRepository = groupUserRepository;
    }

    // [HttpGet("{id}")]
    // public async Task<ActionResult<IEnumerable<User>>> GetUsers(int id)
    // {
        // Group? group = _context.Groups.Find(id);
        // if (group == null)
        // {
        //     return Conflict("Group does not exists");
        // }

        // IEnumerable<User> users = _context
            // .GroupUsers
            // .Include(gu => gu.User)
            // .Where(gu => gu.GroupId == id)
            // .Select(gu => gu.User);
        
        // return users.ToList();
    // }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GroupDto>>> List()
    {
        var user = GetUser();
        IEnumerable<Group> groups = _context
            .GroupUsers
            .Include(gu => gu.Group)
            .Where(gu => gu.UserId == user.Id)
            .Select(gu => gu.Group);

        return groups.Select(g => GroupDto.Create(g)).ToList();
    }

    // [HttpGet("{id}")]
    // public async Task<ActionResult<Group>> GetUsers(int id)
    // {
    //     Group? group = _context.Groups.Find(id);
    //
    //     if (group == null)
    //     {
    //         return Conflict("Group does not exists");
    //     }
    //
    //     return group;
    // }

    [HttpPost]
    public async Task<ActionResult<GroupDto>> Create([FromBody] GroupModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var user = GetUser();
        model.AdministratorId = user.Id;
        Group group = _groupService.Create(model);
        var groupUser = new GroupUser();
        groupUser.GroupId = group.Id;
        groupUser.UserId = user.Id;
        _context.GroupUsers.Add(groupUser);
        await _context.SaveChangesAsync();
        return Ok(GroupDto.Create(group));
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<GroupDto>> Edit([FromBody] GroupModel model, int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var group = _context.Groups.Find(id);
        if (group == null)
        {
            return BadRequest("Group does not exists");
        }
        if (GetUser().Id != group.AdministratorId)
        {
            return Conflict("User have no permission to edit this group");
        }

        _groupService.Edit(model, group);
        return Ok(GroupDto.Create(group));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GroupDto>> Get(int id)
    {
        var group = _context.Groups.Find(id);
        if (group == null)
        {
            return BadRequest("Group does not exists");
        }
        if (!_groupUserRepository.UserBelongsToGroup(GetUser(), group))
        {
            return Conflict("User have no permission to view this group");
        }

        return Ok(GroupDto.Create(group));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var group = _context.Groups.Find(id);
        if (group == null)
        {
            return BadRequest("Group does not exists");
        }
        if (GetUser().Id != group.AdministratorId)
        {
            return Conflict("User have no permission to edit this group");
        }

        this._context.Groups.Remove(group);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost]
    [Route("leave/{id}")]
    public async Task<IActionResult> Leave(int id)
    {
        var group = _context.Groups.Find(id);
        if (group == null)
        {
            return BadRequest("Group does not exists");
        }

        if (!_groupUserRepository.UserBelongsToGroup(GetUser(), group))
        {
            return Conflict("User does not belong to this group");
        }

        if (GetUser().Id == group.AdministratorId)
        {
            return Conflict("Cannot remove administrator from group");
        }
        

        var groupUser = _context
            .GroupUsers
            .Where(gu => gu.GroupId == group.Id).FirstOrDefault(gu => gu.UserId == GetUser().Id);

        _context.GroupUsers.Remove(groupUser);
        _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost]
    [Route("{groupId}/kick/{userId}")]
    public async Task<IActionResult> KickUser(int groupId, int userId)
    {
        var group = _context.Groups.Find(groupId);
        if (group == null)
        {
            return BadRequest("Group does not exists");
        }
        
        if (GetUser().Id != group.AdministratorId)
        {
            return Conflict("User have no permission to edit this group");
        }

        if (userId == group.AdministratorId)
        {
            return Conflict("Cannot remove administrator from group");
        }

        var user = _context.Users.Find(userId);
        if (user == null)
        {
            return BadRequest("User does not exists");
        }

        var groupUser = _context
            .GroupUsers
            .Where(gu => gu.GroupId == group.Id).FirstOrDefault(gu => gu.UserId == user.Id);

        if (groupUser == null)
        {
            return BadRequest("User does not belong to this group");
        }

        _context.GroupUsers.Remove(groupUser);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet]
    [Route("get-users/{id}")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers(int id)
    {
        var group = _context.Groups.Find(id);
        if (group == null)
        {
            return BadRequest("Group does not exists");
        }

        var user = GetUser();
        IEnumerable<User> users = _context
            .GroupUsers
            .Include(gu => gu.User)
            .Where(gu => gu.GroupId == group.Id)
            .Select(gu => gu.User);
        return Ok(users.Select(u => UserDto.Create(u)));
    }
}