// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrbitalWitnessAPI.Context;

#nullable disable

namespace OrbitalWitnessAPI.Migrations
{
    [DbContext(typeof(OrbitalWitnessContext))]
    [Migration("20220424125528_EntryDateTypeChange2")]
    partial class EntryDateTypeChange2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("OrbitalWitnessAPI.Domain.Note", b =>
                {
                    b.Property<int>("NoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NoteId"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ParsedDataId")
                        .HasColumnType("int");

                    b.HasKey("NoteId");

                    b.HasIndex("ParsedDataId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("OrbitalWitnessAPI.Domain.ParsedSchedule", b =>
                {
                    b.Property<int>("ParsedDataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ParsedDataId"), 1L, 1);

                    b.Property<string>("DateOfLeaseAndTerm")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EntryNumber")
                        .HasColumnType("int");

                    b.Property<string>("LesseesTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PropertyDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RawData")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegistrationDateAndPlanRef")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ParsedDataId");

                    b.ToTable("ParsedSchedules");
                });

            modelBuilder.Entity("OrbitalWitnessAPI.Domain.Note", b =>
                {
                    b.HasOne("OrbitalWitnessAPI.Domain.ParsedSchedule", "ParsedData")
                        .WithMany("Notes")
                        .HasForeignKey("ParsedDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParsedData");
                });

            modelBuilder.Entity("OrbitalWitnessAPI.Domain.ParsedSchedule", b =>
                {
                    b.Navigation("Notes");
                });
#pragma warning restore 612, 618
        }
    }
}
