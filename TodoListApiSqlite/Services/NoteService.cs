using TodoListApiSqlite.Models;
using TodoListApiSqlite.Repositories;
using TodoListApiSqlite.RequestModel;

namespace TodoListApiSqlite.Services;

public class NoteService
{

    private readonly NoteRepository _noteRepository;
    
    public NoteService(NoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }
    
    public Note Create(NoteModel model)
    {
        Note note = new Note();
        note = SetCommonFields(model, note);
        
        _noteRepository.CreateNote(note);
        return note;
    }

    public Note Update(NoteModel model, Note note)
    {
        note = SetCommonFields(model, note);
        
        _noteRepository.UpdateNote(note);
        return note;
    }

    public Note SetCommonFields(NoteModel model, Note note)
    {
        note.Description = model.Description;
        note.GroupId = model.GroupId;
        note.Priority = model.Priority;
        note.Name = model.Name;
        return note;
    }
    
}