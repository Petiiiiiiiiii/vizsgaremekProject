using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dungeonMaster_db.Models;

namespace dungeonMaster_db.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnlockedSkillsController : ControllerBase
    {
        private readonly dungeonMaster _context;

        public UnlockedSkillsController(dungeonMaster context)
        {
            _context = context;
        }

        // GET: api/UnlockedSkills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnlockedSkill>>> GetUnlockedSkills()
        {
            return await _context.UnlockedSkills.ToListAsync();
        }

        // GET: api/UnlockedSkills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UnlockedSkill>> GetUnlockedSkill(int id)
        {
            var unlockedSkill = await _context.UnlockedSkills.FindAsync(id);

            if (unlockedSkill == null)
            {
                return NotFound();
            }

            return unlockedSkill;
        }

        // PUT: api/UnlockedSkills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnlockedSkill(int id, UnlockedSkill unlockedSkill)
        {
            if (id != unlockedSkill.PlayerId)
            {
                return BadRequest();
            }

            _context.Entry(unlockedSkill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnlockedSkillExists(id))
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

        // POST: api/UnlockedSkills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UnlockedSkill>> PostUnlockedSkill(UnlockedSkill unlockedSkill)
        {
            _context.UnlockedSkills.Add(unlockedSkill);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UnlockedSkillExists(unlockedSkill.PlayerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUnlockedSkill", new { id = unlockedSkill.PlayerId }, unlockedSkill);
        }

        // DELETE: api/UnlockedSkills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnlockedSkill(int id)
        {
            var unlockedSkill = await _context.UnlockedSkills.FindAsync(id);
            if (unlockedSkill == null)
            {
                return NotFound();
            }

            _context.UnlockedSkills.Remove(unlockedSkill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UnlockedSkillExists(int id)
        {
            return _context.UnlockedSkills.Any(e => e.PlayerId == id);
        }
    }
}
