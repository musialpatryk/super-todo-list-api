namespace TodoListApiSqlite.RequestModel;

public class UserModel : IUserModel
{
    public string? Name { get; set; } = null!;
    public string? Email { get; set; } = null!;
    public string? PasswordCheck { get; set; } = null!;
    public string? Password { get; set; } = null;
}