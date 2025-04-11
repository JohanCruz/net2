using Contactly.Data;
using Contactly.Models;
using Contactly.Models.Domain;
using Contactly.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contactly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactlyDbContext dbContext;

        public ContactsController(ContactlyDbContext dbContext )
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        //public IActionResult GetAllContacts()
        //{
        //    var contacts = dbContext.Contacts.ToList(); 
        //    return Ok( contacts );
        //}       
        public IActionResult GetAllContacts()
        {
            var contacts = dbContext.Contacts
                .Include(c => c.ContactTopics)
                    .ThenInclude(ct => ct.Topic)
                .Select(c => new ContactDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    Phone = c.Phone,
                    Favorite = c.Favorite,
                    Topics = c.ContactTopics.Select(ct => new ContactTopicDto
                    {
                        TopicId = ct.TopicId,
                        TopicName = ct.Topic.Name, // Opcional
                        InterestLevel = ct.InterestLevel,
                        Comment = ct.Comment
                    }).ToList()
                })
                .ToList();

            return Ok(contacts);
        }


        [HttpPost]
        //public IActionResult AddContact(AddContactRequestDTO request )
        //{
        //    var domainModelContact = new Contact
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = request.Name,
        //        Email = request.Email,
        //        Phone = request.Phone,
        //        Favorite = request.Favorite
        //    };

        //    dbContext.Contacts.Add(domainModelContact);
        //    dbContext.SaveChanges();

        //    return Ok( domainModelContact );
        //}
        [HttpPost]
        public IActionResult AddContact(AddContactRequestDTO request)
        {
            var id = Guid.NewGuid();
            var contact = new Contact
            {
                Id = id,
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Favorite = request.Favorite
            };

            dbContext.Contacts.Add(contact);

            if (request.Topics != null)
            {
                foreach (var topicRequest in request.Topics)
                {
                    Console.WriteLine("topics: " + topicRequest);
                    topicRequest.ContactId = id;
                    contact.ContactTopics.Add(new ContactTopic
                    {
                        ContactId = contact.Id,
                        TopicId = topicRequest.TopicId,
                        InterestLevel = topicRequest.InterestLevel,
                        Comment = topicRequest.Comment
                    });
                }
            }

            
            dbContext.SaveChanges();

            var responseDto = new ContactResponseDTO
            {
                Id = contact.Id,
                Name = contact.Name,
                Email = contact.Email,
                Phone = contact.Phone,
                Favorite = contact.Favorite,
                Topics = contact.ContactTopics.Select(ct => new ContactTopicResponseDTO
                {
                    TopicId = ct.TopicId,
                    TopicName = ct.Topic?.Name, // Asegúrate de incluir .Include(ct => ct.Topic)
                    InterestLevel = ct.InterestLevel,
                    Comment = ct.Comment
                }).ToList()
            };

            return Ok(responseDto); // Devuelve el DTO, no la entidad            
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteContact(Guid id)
        {
            var contact = dbContext.Contacts.Find(id);

            if(contact is not null)
            {
                dbContext.Contacts.Remove(contact);
                dbContext.SaveChanges();
            }
            return Ok();
        }
    }
}
