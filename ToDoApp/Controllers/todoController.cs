using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class todoController : ControllerBase
    {
        private readonly ShobDBContext _context;

        public todoController(ShobDBContext context)
        {
            _context = context;
        }

        // GET: api/todo
        [HttpGet]
        public IEnumerable<Todotbl> GetTodotbl()
        {
            return _context.Todotbl;
        }

        // GET: api/todo/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodotbl([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todotbl = await _context.Todotbl.FindAsync(id);

            if (todotbl == null)
            {
                return NotFound();
            }

            return Ok(todotbl);
        }

        // PUT: api/todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodotbl([FromRoute] int id, [FromBody] Todotbl todotbl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != todotbl.TaskId)
            {
                return BadRequest();
            }

            _context.Entry(todotbl).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodotblExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/todo
        [HttpPost]
        public async Task<IActionResult> PostTodotbl([FromBody] Todotbl todotbl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Todotbl.Add(todotbl);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TodotblExists(todotbl.TaskId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
                //return BadRequest();
            }

            return CreatedAtAction("GetTodotbl", new { id = todotbl.TaskId }, todotbl);
        }

        // DELETE: api/todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodotbl([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todotbl = await _context.Todotbl.FindAsync(id);
            if (todotbl == null)
            {
                return NotFound();
            }

            _context.Todotbl.Remove(todotbl);
            await _context.SaveChangesAsync();

            return Ok(todotbl);
        }

        private bool TodotblExists(int id)
        {
            return _context.Todotbl.Any(e => e.TaskId == id);
        }
    }
}