using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TVApp.Data;
using TVApp.Models;

namespace TVApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreTablesController : ControllerBase
    {
        private readonly ScoreTablesContext _context;

        public ScoreTablesController(ScoreTablesContext context)
        {
            _context = context;
        }

        // GET: api/ScoreTables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScoreTable>>> GetScoreTable()
        {
            return await _context.ScoreTable.ToListAsync();
        }

        // GET: api/ScoreTables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScoreTable>> GetScoreTable(int id)
        {
            var scoreTable = await _context.ScoreTable.FindAsync(id);

            if (scoreTable == null)
            {
                return NotFound();
            }

            return scoreTable;
        }

        // PUT: api/ScoreTables/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScoreTable(int id, ScoreTable scoreTable)
        {
            if (id != scoreTable.Id)
            {
                return BadRequest();
            }

            _context.Entry(scoreTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoreTableExists(id))
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

        // POST: api/ScoreTables
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ScoreTable>> PostScoreTable(ScoreTable scoreTable)
        {
            _context.ScoreTable.Add(scoreTable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScoreTable", new { id = scoreTable.Id }, scoreTable);
        }

        // DELETE: api/ScoreTables/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ScoreTable>> DeleteScoreTable(int id)
        {
            var scoreTable = await _context.ScoreTable.FindAsync(id);
            if (scoreTable == null)
            {
                return NotFound();
            }

            _context.ScoreTable.Remove(scoreTable);
            await _context.SaveChangesAsync();

            return scoreTable;
        }

        private bool ScoreTableExists(int id)
        {
            return _context.ScoreTable.Any(e => e.Id == id);
        }
    }
}
