using Contactly.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Contactly.Data
{
    public class ContactlyDbContext : DbContext
    {
        //public ContactlyDbContext(DbContextOptions options) : base(options)
        //{
        //}
        public ContactlyDbContext(DbContextOptions<ContactlyDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Topic> Topics { get; set; } // Nuevo DbSet para Topic
        public DbSet<ContactTopic> ContactTopics { get; set; } // Entidad intermedia

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactTopic>()
                .HasKey(ct => new { ct.ContactId, ct.TopicId }); // Clave compuesta

            // Relación con Contact (Configuración de cascada)
            modelBuilder.Entity<ContactTopic>()
                .HasOne(ct => ct.Contact)
                .WithMany(c => c.ContactTopics)
                .HasForeignKey(ct => ct.ContactId)
                .OnDelete(DeleteBehavior.Cascade); 

            // Relación con Topic
            modelBuilder.Entity<ContactTopic>()
                .HasOne(ct => ct.Topic)
                .WithMany(t => t.ContactTopics)
                .HasForeignKey(ct => ct.TopicId);
        }
    }
}
