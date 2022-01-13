namespace TodoListApiSqlite.RequestModel;

public interface IUserModel
{
    string? Email { get; set; }
    string? Name { get; set; }
    string? Password { get; set; }
    string? PasswordCheck { get; set; }
}