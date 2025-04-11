using System.ComponentModel.DataAnnotations;

namespace Contactly.Models.Domain
{
    public class Topic
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        // Colección para la relación muchos a muchos (a través de ContactTopic)
        public ICollection<ContactTopic> ContactTopics { get; set; } = new List<ContactTopic>();
    }

}
