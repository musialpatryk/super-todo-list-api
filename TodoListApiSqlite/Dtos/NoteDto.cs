using TodoListApiSqlite.Models;

namespace TodoListApiSqlite.Dtos
{
    public record NoteDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? Priority { get; set; }
        public int GroupId { get; set; }

        public static NoteDto Create(Note note)
        {
            NoteDto dto = new NoteDto();
            dto.Id = note.Id;
            dto.Name = note.Name;
            dto.Description = note.Description;
            dto.Priority = note.Priority;
            dto.GroupId = note.GroupId;

            return dto;
        }
    }
}
