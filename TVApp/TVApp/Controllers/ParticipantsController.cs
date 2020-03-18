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
    public class ParticipantsController : ControllerBase
    {
        private readonly ParticipantsContext _context;
        private readonly IDataRepository<Participant> _repo;

        public ParticipantsController(ParticipantsContext context, IDataRepository<Participant> repo)
        {
            _context = context;
            _repo = repo;
        }

        // GET: api/Participants
        [HttpGet]
        public IEnumerable<Participant> GetBlogPosts()
        {
            return _context.Participant.OrderByDescending(p => p.ParticipantId);
        }

        // GET: api/BlogPosts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetParticipant([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var participant = await _context.Participant.FindAsync(id);

            if (participant == null)
            {
                return NotFound();
            }

            return Ok(participant);
        }

        // PUT: api/BlogPosts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipant([FromRoute] int id, [FromBody] Participant participant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != participant.ParticipantId)
            {
                return BadRequest();
            }

            _context.Entry(participant).State = EntityState.Modified;

            try
            {
                _repo.Update(participant);
                var save = await _repo.SaveAsync(participant);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantExists(id))
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

        // POST: api/BlogPosts
        [HttpPost]
        public async Task<IActionResult> ParticipantPost([FromBody] Participant participant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repo.Add(participant);
            var save = await _repo.SaveAsync(participant);

            return CreatedAtAction("GetParticipantPost", new { id = participant.ParticipantId }, participant);
        }

        // DELETE: api/BlogPosts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipant([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var participant = await _context.Participant.FindAsync(id);
            if (participant == null)
            {
                return NotFound();
            }

            _repo.Delete(participant);
            var save = await _repo.SaveAsync(participant);

            return Ok(participant);
        }

        private bool ParticipantExists(int id)
        {
            return _context.Participant.Any(e => e.ParticipantId == id);
        }
    }
}
