namespace Contactly.Models.Dtos
{
    public class ContactTopicResponseDTO
    {
        public Guid TopicId { get; set; }
        public string TopicName { get; set; } // Opcional
        public int InterestLevel { get; set; }
        public string Comment { get; set; }
    }
}
