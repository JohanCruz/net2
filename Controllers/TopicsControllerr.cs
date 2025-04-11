using Contactly.Data;
using Contactly.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Contactly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly ContactlyDbContext _dbContext;

        public TopicsController(ContactlyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Topics
        [HttpGet]
        public IActionResult GetAllTopics()
        {
            var topics = _dbContext.Topics.ToList();
            return Ok(topics);
        }

        // POST: api/Topics
        [HttpPost]
        public IActionResult AddTopic([FromBody] TopicRequestDto request)
        {
            var topic = new Topic
            {
                Name = request.Name
            };

            _dbContext.Topics.Add(topic);
            _dbContext.SaveChanges();

            return Ok(topic);
        }

        // DELETE: api/Topics/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteTopic(int id)
        {
            var topic = _dbContext.Topics.Find(id);

            if (topic == null)
                return NotFound();

            _dbContext.Topics.Remove(topic);
            _dbContext.SaveChanges();

            return Ok();
        }
    }

    public class TopicRequestDto
    {
        public string Name { get; set; }
    }
}
