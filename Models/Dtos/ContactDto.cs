namespace Contactly.Models.Dtos
{
    public class ContactDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Favorite { get; set; }
        public List<ContactTopicDto> Topics { get; set; } = new();
    }
}
