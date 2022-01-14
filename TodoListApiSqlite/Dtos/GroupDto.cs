
using TodoListApiSqlite.Models;

namespace TodoListApiSqlite.Dtos;

public record GroupDto
{
    
    public int Id { get; set; }
    public string Name { get; set; }
    public int? AdministratorId { get; set; }

    public static GroupDto Create(Group group)
    {
        GroupDto dto = new GroupDto();

        dto.Id = group.Id;
        dto.Name = group.Name;
        dto.AdministratorId = group.AdministratorId;

        return dto;
    }
}