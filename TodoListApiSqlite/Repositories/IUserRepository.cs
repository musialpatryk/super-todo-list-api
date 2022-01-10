using TodoListApiSqlite.Models;

namespace TodoListApiSqlite.Repositories
{
    public interface IUserRepository
    {
        User? GetUser(string email, string password);
        User? GetUser(string email);
        void CreateUser(User user);
    }
}
