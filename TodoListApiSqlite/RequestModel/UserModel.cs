namespace TodoListApiSqlite.RequestModel;

public class UserModel
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string PasswordCheck { get; set; } = null!;
}