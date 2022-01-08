using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoListApiSqlite.Data;
using TodoListApiSqlite.Dtos;
using TodoListApiSqlite.Models;
using TodoListApiSqlite.RequestModel;
using TodoListApiSqlite.Services;

namespace TodoListApiSqlite.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly TodoListApiContext _context;

        private readonly NoteService _noteService;

        public NotesController(TodoListApiContext context, NoteService noteService)
        {
            _context = context;
            _noteService = noteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteDto>>> GetList()
        {
            return await _context.Notes.Select(x => NoteDto.Create(x)).ToListAsync();
        }

        // [HttpPost]
        // public async Task<ActionResult<NoteDto>> Create([FromBody] NoteModel model)
        // {
        //     
        // }
        
        // GET: Notes/Create
        /*[HttpPost]
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id");
            return View();
        }*/

        // POST: Notes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult<NoteDto>> Create([Bind("Id,Name,Description,Priority,GroupId")] NoteModel noteModel)
        {
            if (ModelState.IsValid)
            {
                Note note = _noteService.Create(noteModel);
                return Ok(NoteDto.Create(note));
            }
            return BadRequest();
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
