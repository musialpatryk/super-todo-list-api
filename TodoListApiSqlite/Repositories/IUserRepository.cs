using TodoListApiSqlite.Models;

namespace TodoListApiSqlite.Repositories
{
    public interface IUserRepository
    {
        User? GetUser(string email, string password);
    }
}
