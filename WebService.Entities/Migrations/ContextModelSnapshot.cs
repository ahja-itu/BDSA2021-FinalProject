﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebService.Entities;

#nullable disable

namespace WebService.Entities.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebService.Entities.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("WebService.Entities.Level", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("MaterialId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("WebService.Entities.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("LanguageId")
                        .HasColumnType("integer");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("character varying(400)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("WebService.Entities.Media", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("MaterialId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Medias");
                });

            modelBuilder.Entity("WebService.Entities.ProgrammingLanguage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("MaterialId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ProgrammingLanguages");
                });

            modelBuilder.Entity("WebService.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("WebService.Entities.Level", b =>
                {
                    b.HasOne("WebService.Entities.Material", null)
                        .WithMany("Levels")
                        .HasForeignKey("MaterialId");
                });

            modelBuilder.Entity("WebService.Entities.Material", b =>
                {
                    b.HasOne("WebService.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("WebService.Entities.Author", "Authors", b1 =>
                        {
                            b1.Property<int>("MaterialId")
                                .HasColumnType("integer");

                            b1.Property<string>("FirstName")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<string>("SurName")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<int>("Id")
                                .HasColumnType("integer");

                            b1.HasKey("MaterialId", "FirstName", "SurName");

                            b1.ToTable("Author");

                            b1.WithOwner()
                                .HasForeignKey("MaterialId");
                        });

                    b.OwnsMany("WebService.Entities.Rating", "Ratings", b1 =>
                        {
                            b1.Property<int>("MaterialId")
                                .HasColumnType("integer");

                            b1.Property<string>("Reviewer")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<int>("Id")
                                .HasColumnType("integer");

                            b1.Property<DateTime>("TimeStamp")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<int>("Value")
                                .HasColumnType("integer");

                            b1.HasKey("MaterialId", "Reviewer");

                            b1.ToTable("Rating");

                            b1.WithOwner()
                                .HasForeignKey("MaterialId");
                        });

                    b.OwnsMany("WebService.Entities.WeightedTag", "WeightedTags", b1 =>
                        {
                            b1.Property<int>("MaterialId")
                                .HasColumnType("integer");

                            b1.Property<string>("Name")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<int>("Id")
                                .HasColumnType("integer");

                            b1.Property<int>("Weight")
                                .HasColumnType("integer");

                            b1.HasKey("MaterialId", "Name");

                            b1.ToTable("WeightedTag");

                            b1.WithOwner()
                                .HasForeignKey("MaterialId");
                        });

                    b.Navigation("Authors");

                    b.OwnsMany("WebService.Entities.Author", "Authors", b1 =>
                        {
                            b1.Property<int>("MaterialId")
                                .HasColumnType("integer");

                            b1.Property<string>("FirstName")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<string>("SurName")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<int>("Id")
                                .HasColumnType("integer");

                            b1.HasKey("MaterialId", "FirstName", "SurName");

                            b1.ToTable("Author");

                            b1.WithOwner()
                                .HasForeignKey("MaterialId");
                        });

                    b.OwnsMany("WebService.Entities.Rating", "Ratings", b1 =>
                        {
                            b1.Property<int>("MaterialId")
                                .HasColumnType("integer");

                            b1.Property<string>("Reviewer")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<int>("Id")
                                .HasColumnType("integer");

                            b1.Property<DateTime>("TimeStamp")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<int>("Value")
                                .HasColumnType("integer");

                            b1.HasKey("MaterialId", "Reviewer");

                            b1.ToTable("Rating");

                            b1.WithOwner()
                                .HasForeignKey("MaterialId");
                        });

                    b.OwnsMany("WebService.Entities.WeightedTag", "WeightedTags", b1 =>
                        {
                            b1.Property<int>("MaterialId")
                                .HasColumnType("integer");

                            b1.Property<string>("Name")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<int>("Id")
                                .HasColumnType("integer");

                            b1.Property<int>("Weight")
                                .HasColumnType("integer");

                            b1.HasKey("MaterialId", "Name");

                            b1.ToTable("WeightedTag");

                            b1.WithOwner()
                                .HasForeignKey("MaterialId");
                        });

                    b.Navigation("Authors");

                    b.Navigation("Language");

                    b.Navigation("Ratings");

                    b.Navigation("WeightedTags");
                });

            modelBuilder.Entity("WebService.Entities.Media", b =>
                {
                    b.HasOne("WebService.Entities.Material", null)
                        .WithMany("Medias")
                        .HasForeignKey("MaterialId");
                });

            modelBuilder.Entity("WebService.Entities.ProgrammingLanguage", b =>
                {
                    b.HasOne("WebService.Entities.Material", null)
                        .WithMany("ProgrammingLanguages")
                        .HasForeignKey("MaterialId");
                });

            modelBuilder.Entity("WebService.Entities.Material", b =>
                {
                    b.Navigation("Levels");

                    b.Navigation("Medias");

                    b.Navigation("ProgrammingLanguages");
                });
#pragma warning restore 612, 618
        }
    }
}
