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
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly ResultsContext _context;
        private readonly IDataRepository<Result> _repo;

        public ResultsController(ResultsContext context, IDataRepository<Result> repo)
        {
            _context = context;
            _repo = repo;
        }

        // GET: api/Results
        [HttpGet]
        public IEnumerable<Result> GetResults()
        {
            return _context.Result.OrderByDescending(p => p.ResultId);
        }

        // GET: api/Results/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetResult([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _context.Result.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/Results/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResult([FromRoute] int id, [FromBody] Result result)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != result.ResultId)
            {
                return BadRequest();
            }

            _context.Entry(result).State = EntityState.Modified;

            try
            {
                _repo.Update(result);
                var save = await _repo.SaveAsync(result);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultExists(id))
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

        // POST: api/Results
        [HttpPost]
        public async Task<IActionResult> PostResult([FromBody] Result result)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repo.Add(result);
            var save = await _repo.SaveAsync(result);

            return CreatedAtAction("GetResult", new { id = result.ResultId }, result);
        }

        // DELETE: api/Results/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResult([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _context.Result.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            _repo.Delete(result);
            var save = await _repo.SaveAsync(result);

            return Ok(result);
        }

        private bool ResultExists(int id)
        {
            return _context.Result.Any(e => e.ResultId == id);
        }
    }
}
