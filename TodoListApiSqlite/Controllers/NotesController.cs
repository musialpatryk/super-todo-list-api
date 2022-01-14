using System.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoListApiSqlite.Data;
using TodoListApiSqlite.Dtos;
using TodoListApiSqlite.Models;
using TodoListApiSqlite.Repositories;
using TodoListApiSqlite.RequestModel;
using TodoListApiSqlite.Services;

namespace TodoListApiSqlite.Controllers
{
    [Authorize]
    [Route("note")]
    [ApiController]
    public class NotesController : ApiController
    {
        
        private readonly NoteService _noteService;
        private readonly GroupUserRepository _groupUserRepository;

        public NotesController(TodoListApiContext context, NoteService noteService, GroupUserRepository groupUserRepository): base(context)
        {
            _noteService = noteService;
            _groupUserRepository = groupUserRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteDto>>> GetList([FromQuery(Name="group")] int groupId)
        {
            return await _context.Notes
                .Where(n => n.GroupId == groupId)
                .Select(x => NoteDto.Create(x))
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<NoteDto>> Create([FromBody] NoteModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User user = GetUser();
            var group = _context.Groups.Find(model.GroupId);
            if (group == null)
            {
                return Conflict("Group does not exists");
            }
            if (!_groupUserRepository.UserBelongsToGroup(user, group))
            {
                return Conflict("User does not belong to this group");
            }

            Note note = _noteService.Create(model);
            return Ok(NoteDto.Create(note));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<NoteDto>> Update([FromBody] NoteModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User user = GetUser();
            var group = _context.Groups.Find(model.GroupId);
            if (group == null)
            {
                return Conflict("Group does not exists");
            }
            if (!_groupUserRepository.UserBelongsToGroup(user, group))
            {
                return Conflict("User does not belong to this group");
            }

            Note note = _context.Notes.Find(id);
            if (note == null)
            {
                return BadRequest("Note does not exists");
            } 
            note = _noteService.Update(model, note);
            return Ok(NoteDto.Create(note));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NoteDto>> Get(int? id)
        {
            
            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                return BadRequest("Note doesn't exists");
            }
            User user = GetUser();
            var group = _context.Groups.Find(note.GroupId);
            if (!_groupUserRepository.UserBelongsToGroup(user, group))
            {
                return Conflict("User does not belong to this group");
            }

            return Ok(NoteDto.Create(note));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                return BadRequest("Note doesn't exists");
            }
            User user = GetUser();
            var group = _context.Groups.Find(note.GroupId);
            if (!_groupUserRepository.UserBelongsToGroup(user, group))
            {
                return Conflict("User does not belong to this group");
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
