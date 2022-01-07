using TodoListApiSqlite.Models;
using TodoListApiSqlite.Data;

namespace TodoListApiSqlite.Repositories
{
    public class UserRepository : IUserRepository
    {
        /*private readonly TodoListApiContext _context;*/

        private readonly IServiceScopeFactory _serviceScopeFactory;

        public UserRepository(IServiceScopeFactory scopeFactory)
        {
            _serviceScopeFactory = scopeFactory;
        }

        public User? GetUser(string email, string password)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TodoListApiContext>();
                return context.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
            }
        }

    }
}
