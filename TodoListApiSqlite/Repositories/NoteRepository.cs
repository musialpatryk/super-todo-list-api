using System.Linq;
using TodoListApiSqlite.Data;
using TodoListApiSqlite.Models;

namespace TodoListApiSqlite.Repositories;

public class NoteRepository : AbstractRepository
{
    public NoteRepository(IServiceScopeFactory scopeFactory) : base(scopeFactory)
    {
    }

    public async void CreateNote(Note note)
    {
        using (var scope = serviceScopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<TodoListApiContext>();
            await context.Notes.AddAsync(note);
            await context.SaveChangesAsync();
        }
    }

    public async void UpdateNote(Note note)
    {
        using (var scope = serviceScopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<TodoListApiContext>();
            context.Update(note);
            await context.SaveChangesAsync();
        }
    }
    
}