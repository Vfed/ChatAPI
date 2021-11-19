﻿// <auto-generated />
using System;
using ChatAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ChatAPI.Migrations
{
    [DbContext(typeof(DbService))]
    partial class DbServiceModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ChatAPI.Data.Models.AdminUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AdminUsers");
                });

            modelBuilder.Entity("ChatAPI.Data.Models.Chat", b =>
                {
                    b.Property<Guid>("ChatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ChatName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastData")
                        .HasColumnType("datetime2");

                    b.HasKey("ChatId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("ChatAPI.Data.Models.ChatUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastActionTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ChatUsers");
                });

            modelBuilder.Entity("ChatAPI.Data.Models.ChatsList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ChatId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ChatUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Current")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("ChatUserId");

                    b.ToTable("ChatsList");
                });

            modelBuilder.Entity("ChatAPI.Data.Models.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ChatId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CurrentTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Massege")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("ChatAPI.Data.Models.ChatsList", b =>
                {
                    b.HasOne("ChatAPI.Data.Models.Chat", "Chat")
                        .WithMany("ChatsList")
                        .HasForeignKey("ChatId");

                    b.HasOne("ChatAPI.Data.Models.ChatUser", "ChatUser")
                        .WithMany("ChatLists")
                        .HasForeignKey("ChatUserId");
                });

            modelBuilder.Entity("ChatAPI.Data.Models.Message", b =>
                {
                    b.HasOne("ChatAPI.Data.Models.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId");
                });
#pragma warning restore 612, 618
        }
    }
}
