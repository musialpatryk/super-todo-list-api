namespace TodoListApiSqlite.RequestModel;

public class NoteModel
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int? Priority { get; set; }
    public int GroupId { get; set; }
}