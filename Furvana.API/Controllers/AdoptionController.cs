using Furvana.API.Data;
using Furvana.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Furvana.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdoptionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdoptionController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/adoption
        [HttpPost]
        public async Task<ActionResult<AdoptionRequest>> SubmitRequest([FromBody] AdoptionRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.AdoptionRequests.Add(request);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Application submitted successfully!" });
        }

        // GET: api/adoption
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdoptionRequest>>> GetAll()
        {
            return await _context.AdoptionRequests.ToListAsync();
        }
    }
}
