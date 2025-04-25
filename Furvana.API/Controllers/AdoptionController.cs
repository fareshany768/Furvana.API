using Furvana.API.Data;
using Furvana.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            [HttpPost]
            public async Task<ActionResult<AdoptionRequest>> SubmitRequest(AdoptionRequest request)
            {
                _context.AdoptionRequests.Add(request);
                await _context.SaveChangesAsync();
                return Ok(request);
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<AdoptionRequest>>> GetAll()
            {
                return await _context.AdoptionRequests.ToListAsync();
            }
        }
    }

