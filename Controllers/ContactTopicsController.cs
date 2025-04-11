using Contactly.Data;
using Contactly.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contactly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactTopicsController : ControllerBase
    {
        private readonly ContactlyDbContext _dbContext;

        public ContactTopicsController(ContactlyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/ContactTopics
        [HttpPost]
        public IActionResult AddTopicToContact([FromBody] ContactTopicRequestDto request)
        {
            var contactTopic = new ContactTopic
            {
                ContactId = request.ContactId,
                TopicId = request.TopicId,
                InterestLevel = request.InterestLevel,
                Comment = request.Comment
            };

            _dbContext.ContactTopics.Add(contactTopic);
            _dbContext.SaveChanges();

            return Ok(contactTopic);
        }

        // DELETE: api/ContactTopics?contactId=...&topicId=...
        [HttpDelete]
        public IActionResult RemoveTopicFromContact(Guid contactId, Guid topicId)
        {
            var contactTopic = _dbContext.ContactTopics
                .FirstOrDefault(ct => ct.ContactId == contactId && ct.TopicId == topicId);

            if (contactTopic == null)
                return NotFound();

            _dbContext.ContactTopics.Remove(contactTopic);
            _dbContext.SaveChanges();

            return Ok();
        }
    }

    public class ContactTopicRequestDto
    {
        public Guid ContactId { get; set; }
        public Guid TopicId { get; set; }
        public int InterestLevel { get; set; }
        public string? Comment { get; set; }
    }
}
