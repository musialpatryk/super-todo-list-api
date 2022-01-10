namespace TodoListApiSqlite.RequestModel;

public class RegisterModel : IRequestModel
{ 
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string PasswordCheck { get; set; } = null!;
}