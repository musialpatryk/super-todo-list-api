using System.Text.Json.Serialization;

namespace TodoListApiSqlite.RequestModel;

public class GroupModel
{
    
    public string Name { get; set; } = null!;
    
    [JsonIgnore]
    public int? AdministratorId { get; set; }
}