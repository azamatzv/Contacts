using Contacts.Data;
using Contacts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Contacts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ValuesController(MyDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUser", new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Serverda xatolik yuz berdi: " + ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest("Id does not match");
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}