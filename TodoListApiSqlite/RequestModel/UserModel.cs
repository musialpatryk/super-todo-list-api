using System.Text.Json.Serialization;

namespace TodoListApiSqlite.RequestModel;

public class UserModel : IUserModel
{
    public string? Name { get; set; } = null!;
    public string? Email { get; set; } = null!;
    [JsonIgnore]
    public string? PasswordCheck { get; set; } = null!;
    [JsonIgnore]
    public string? Password { get; set; } = null;
}