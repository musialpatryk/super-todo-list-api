using TodoListApiSqlite.Models;

namespace TodoListApiSqlite.Dtos;

public record UserDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }

    public static UserDto Create(User user)
    {
        UserDto dto = new UserDto();
        dto.Id = user.Id;
        dto.Name = user.Name;
        dto.Email = user.Email;

        return dto;
    }
}