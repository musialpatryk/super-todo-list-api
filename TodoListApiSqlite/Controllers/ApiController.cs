using Microsoft.AspNetCore.Mvc;
using TodoListApiSqlite.Data;
using TodoListApiSqlite.Models;

namespace TodoListApiSqlite.Controllers;

public abstract class ApiController : ControllerBase
{

    protected TodoListApiContext _context;
    
    protected ApiController(TodoListApiContext context)
    {
        _context = context;
    }

    protected User GetUser()
    {
        return _context.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault();
    }
    
}