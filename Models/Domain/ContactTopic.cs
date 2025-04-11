using System.ComponentModel.DataAnnotations;

namespace Contactly.Models.Domain
{
    public class ContactTopic
    {
        public Guid ContactId { get; set; } // FK de Contact
        public Guid TopicId { get; set; }   // FK de Topic

        [Range(0, 100, ErrorMessage = "El nivel de interés debe estar entre 0 y 100.")]
        public int InterestLevel { get; set; }

        [MaxLength(500)]
        public string? Comment { get; set; } // Opcional

        // Propiedades de navegación
        public Contact? Contact { get; set; }
        public Topic? Topic { get; set; }
    }

}
