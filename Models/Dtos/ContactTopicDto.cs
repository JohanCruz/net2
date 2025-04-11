namespace Contactly.Models.Dtos
{
    public class ContactTopicDto
    {
        public Guid TopicId { get; set; }
        public string TopicName { get; set; } // Nombre del tema (opcional)
        public int InterestLevel { get; set; }
        public string Comment { get; set; }
    }
}
