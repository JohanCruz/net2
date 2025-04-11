namespace Contactly.Models.Dtos
{
    public class ContactResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Favorite { get; set; }
        public List<ContactTopicResponseDTO> Topics { get; set; } = new();
    }
}
