﻿// <auto-generated />
using System;
using Contactly.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Contactly.Migrations
{
    [DbContext(typeof(ContactlyDbContext))]
    partial class ContactlyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Contactly.Models.Domain.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<bool>("Favorite")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Contactly.Models.Domain.ContactTopic", b =>
                {
                    b.Property<Guid>("ContactId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TopicId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Comment")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<int>("InterestLevel")
                        .HasColumnType("int");

                    b.HasKey("ContactId", "TopicId");

                    b.HasIndex("TopicId");

                    b.ToTable("ContactTopics");
                });

            modelBuilder.Entity("Contactly.Models.Domain.Topic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("Contactly.Models.Domain.ContactTopic", b =>
                {
                    b.HasOne("Contactly.Models.Domain.Contact", "Contact")
                        .WithMany("ContactTopics")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Contactly.Models.Domain.Topic", "Topic")
                        .WithMany("ContactTopics")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contact");

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("Contactly.Models.Domain.Contact", b =>
                {
                    b.Navigation("ContactTopics");
                });

            modelBuilder.Entity("Contactly.Models.Domain.Topic", b =>
                {
                    b.Navigation("ContactTopics");
                });
#pragma warning restore 612, 618
        }
    }
}
