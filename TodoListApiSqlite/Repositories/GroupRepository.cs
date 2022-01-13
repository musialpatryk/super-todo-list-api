using System.Linq;
using TodoListApiSqlite.Data;
using TodoListApiSqlite.Models;

namespace TodoListApiSqlite.Repositories;

public class GroupRepository : AbstractRepository
{
    public GroupRepository(IServiceScopeFactory scopeFactory) : base(scopeFactory)
    {
    }

    public async void CreateGroup(Group group)
    {
        using (var scope = serviceScopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<TodoListApiContext>();
            await context.Groups.AddAsync(group);
            await context.SaveChangesAsync();
        }
    }

    public async void UpdateGroup(Group group)
    {
        using (var scope = serviceScopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<TodoListApiContext>();
            context.Groups.Update(group);
            await context.SaveChangesAsync();
        }
    }
    
}