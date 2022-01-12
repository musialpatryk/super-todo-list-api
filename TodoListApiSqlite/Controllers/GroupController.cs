using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListApiSqlite.Data;
using TodoListApiSqlite.Dtos;
using TodoListApiSqlite.Models;
using TodoListApiSqlite.RequestModel;
using TodoListApiSqlite.Services;

namespace TodoListApiSqlite.Controllers;

[ApiController]
[Route("group")]
[Authorize]
public class GroupController : ApiController
{

    private readonly GroupService _groupService;
    
    public GroupController(TodoListApiContext context, GroupService groupService) : base(context)
    {
        _groupService = groupService;
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

    // [HttpGet("{id}")]
    // public Task<ActionResult<GroupDto>> Index()
    // {
    //     
    // }
}