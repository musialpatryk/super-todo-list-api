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

        public NotesController(TodoListApiContext context, NoteService noteService): base(context)
        {
            _noteService = noteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteDto>>> GetList([FromQuery(Name="group")] int groupId)
        {
            //TODO: Group -> User validation
            return await _context.Notes
                .Where(n => n.GroupId == groupId)
                .Select(x => NoteDto.Create(x))
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<NoteDto>> Create([FromBody] NoteModel noteModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User user = GetUser();
            if (_context.Groups.Find(noteModel.GroupId) == null)
            {
                return Conflict("Group does not exists");
            }
            // if (user.Groups.Where(g => g.Id == noteModel.GroupId).FirstOrDefault() == null)
            // {
            //     return Conflict("User does not belong to this group");
            // }

            Note note = _noteService.Create(noteModel);
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
            if (_context.Groups.Find(model.GroupId) == null)
            {
                return Conflict("Group does not exists");
            }
            // if (user.Groups.Where(g => g.Id == noteModel.GroupId).FirstOrDefault() == null)
            // {
            //     return Conflict("User does not belong to this group");
            // }

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
            // if (user.Groups.Where(g => g.Id == note.GroupId).SingleOrDefault() == null)
            // {
            //     return Conflict("User does not belong to this group");
            // }

            return Ok(NoteDto.Create(note));
        }

        //         // GET: Notes/Details/5
        // public async Task<IActionResult> Details(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     var note = await _context.Notes
        //         .Include(n => n.Group)
        //         .FirstOrDefaultAsync(m => m.Id == id);
        //     if (note == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return View(note);
        // }
        //
        // // GET: Notes/Edit/5
        // public async Task<IActionResult> Edit(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     var note = await _context.Notes.FindAsync(id);
        //     if (note == null)
        //     {
        //         return NotFound();
        //     }
        //     ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", note.GroupId);
        //     return View(note);
        // }
        //
        // // POST: Notes/Edit/5
        // // To protect from overposting attacks, enable the specific properties you want to bind to.
        // // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Priority,GroupId")] Note note)
        // {
        //     if (id != note.Id)
        //     {
        //         return NotFound();
        //     }
        //
        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             _context.Update(note);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if (!NoteExists(note.Id))
        //             {
        //                 return NotFound();
        //             }
        //             else
        //             {
        //                 throw;
        //             }
        //         }
        //         return RedirectToAction(nameof(Index));
        //     }
        //     ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", note.GroupId);
        //     return View(note);
        // }
        //
        // // GET: Notes/Delete/5
        // public async Task<IActionResult> Delete(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     var note = await _context.Notes
        //         .Include(n => n.Group)
        //         .FirstOrDefaultAsync(m => m.Id == id);
        //     if (note == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return View(note);
        // }
        //
        // // POST: Notes/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> DeleteConfirmed(int id)
        // {
        //     var note = await _context.Notes.FindAsync(id);
        //     _context.Notes.Remove(note);
        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        // }
        //
        // private bool NoteExists(int id)
        // {
        //     return _context.Notes.Any(e => e.Id == id);
        // }

    }
}
