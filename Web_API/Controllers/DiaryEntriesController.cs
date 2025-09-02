using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Web_API.Models;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaryEntriesController : ControllerBase
    {
        private readonly IMongoCollection<DiaryEntries> _dc;

        public DiaryEntriesController(IMongoCollection<DiaryEntries> client) 
        {
            _dc = client;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DiaryEntries>> GetById(string id, [FromQuery] string userId)
        {
            var filter = Builders<DiaryEntries>.Filter.And(
                Builders<DiaryEntries>.Filter.Eq("id", id),
                Builders<DiaryEntries>.Filter.Eq("UserId", userId)
                );
            var entry = await _dc.Find(filter).FirstOrDefaultAsync();
            return entry == null ? NotFound() : Ok(entry);
        }
        [HttpPost]
        public async Task<ActionResult<DiaryEntries>> Create(DiaryEntries entries)
        {
            entries.Id = null; // Ensure a new ID is generated
            await _dc.InsertOneAsync(entries);
            return CreatedAtAction(nameof(GetById), new { id = entries.Id }, entries);
        }
    }
}
