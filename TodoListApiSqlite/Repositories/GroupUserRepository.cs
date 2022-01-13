using TodoListApiSqlite.Data;
using TodoListApiSqlite.Models;

namespace TodoListApiSqlite.Repositories;

public class GroupUserRepository : AbstractRepository
{
    
    public GroupUserRepository(IServiceScopeFactory scopeFactory) : base(scopeFactory)
    {
    }

    public bool UserBelongsToGroup(User user, Group group)
    {
        using (var scope = serviceScopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<TodoListApiContext>();
            var groupUser = context.GroupUsers.Where(gu => gu.UserId == user.Id).Where(gu => gu.GroupId == group.Id).SingleOrDefault();
            return null == groupUser;
        }
    }
    
}