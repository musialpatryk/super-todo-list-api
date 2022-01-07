namespace TodoListApiSqlite.Services
{
    public interface IJWTAuthenticationManager
    {
        string Authenticate(string username, string password);
    }
}
