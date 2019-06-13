﻿// <auto-generated />
using ApiCard.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApiCard.Migrations
{
    [DbContext(typeof(CardContext))]
    [Migration("20190612160712_InitialDb")]
    partial class InitialDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApiCard.Models.Card", b =>
                {
                    b.Property<long>("CardNumber");

                    b.Property<int>("CVV");

                    b.Property<string>("DateNowUtc");

                    b.Property<string>("Token");

                    b.HasKey("CardNumber");

                    b.ToTable("Card");
                });
#pragma warning restore 612, 618
        }
    }
}
