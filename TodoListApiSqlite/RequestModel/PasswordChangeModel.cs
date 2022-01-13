namespace TodoListApiSqlite.RequestModel;

public record PasswordChangeModel
{
    public string OldPassword { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string PasswordCheck { get; set; } = null!;
}