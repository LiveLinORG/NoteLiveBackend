using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Configuration.Extensions;
using NoteLiveBackend.IAM.Domain.Model.Aggregates;
using User = NoteLiveBackend.IAM.Domain.Model.Aggregates.User;

namespace NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Configuration
{

    public class AppDbContext : DbContext
    {
        public DbSet<Room.Domain.Model.Entities.Room> Rooms { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<PDF> PDFs { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<User> Usuarios { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
            builder.EnableSensitiveDataLogging();
            builder.AddCreatedUpdatedInterceptor();
            base.OnConfiguring(builder);
        }

protected override void OnModelCreating(ModelBuilder builder)
{
    base.OnModelCreating(builder);

// Configuración de la entidad User
builder.Entity<User>().ToTable("Users");
builder.Entity<User>().HasKey(u => u.Id);
builder.Entity<User>().Property(u => u.Id).IsRequired();
builder.Entity<User>().Property(u => u.Username).IsRequired();
builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
builder.Entity<User>().Property(u => u.Role).IsRequired();
builder.Entity<User>().Property(u => u.FirstName).IsRequired();
builder.Entity<User>().Property(u => u.LastName).IsRequired();
builder.Entity<User>().Property(u => u.Email).IsRequired();

// Configuración de la entidad Question
builder.Entity<Question>().ToTable("Questions");
builder.Entity<Question>().HasKey(q => q.Id);
builder.Entity<Question>().Property(q => q.Id).IsRequired();
builder.Entity<Question>().Property(q => q.Text).IsRequired();
builder.Entity<Question>().Property(q => q.Likes).IsRequired();
builder.Entity<Question>().Property(q => q.UserId).IsRequired();
builder.Entity<Question>().HasOne(q => q.User)
                           .WithMany()
                           .HasForeignKey(q => q.UserId)
                           .HasConstraintName("FK_Question_User");

// Configuración de la entidad PDF
builder.Entity<PDF>().ToTable("PDFs");
builder.Entity<PDF>().HasKey(p => p.Id);
builder.Entity<PDF>().Property(p => p.Id).IsRequired();
builder.Entity<PDF>().Property(p => p.Content).IsRequired();

// Configuración de la entidad Chat
builder.Entity<Chat>().ToTable("Chats");
builder.Entity<Chat>().HasKey(c => c.Id);
builder.Entity<Chat>().Property(c => c.Id).IsRequired();

    // Configuración de la entidad Room
    builder.Entity<Room.Domain.Model.Entities.Room>(room =>
    {
        room.ToTable("Rooms");
        room.HasKey(r => r.Id);
        room.Property(r => r.Id).IsRequired();
        room.Property(r => r.Name).IsRequired();
        room.Property(r => r.CreadorId).IsRequired();

        // Relación con PDF
        room.Property(r => r.PdfId).IsRequired(false);
        room.HasOne(r => r.PDF)
            .WithOne()
            .HasForeignKey<Room.Domain.Model.Entities.Room>(r => r.PdfId)
            .HasConstraintName("FK_Room_PDF");


        // Relación con Creador (User)
        room.HasOne(r => r.Creador)
            .WithMany()
            .HasForeignKey(r => r.CreadorId)
            .HasConstraintName("FK_Room_Creator");


        // Relación muchos a muchos con Users
        room.HasMany(r => r.Users)
            .WithMany(u => u.Rooms)
            .UsingEntity<Dictionary<string, object>>(
                "RoomUsers",
                j => j
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_RoomUser_User"),
                j => j
                    .HasOne<Room.Domain.Model.Entities.Room>()
                    .WithMany()
                    .HasForeignKey("RoomId")
                    .HasConstraintName("FK_RoomUser_Room"),
                j =>
                {
                    j.HasKey("RoomId", "UserId");
                    j.ToTable("RoomUsers");
                    j.HasIndex("RoomId", "UserId").HasDatabaseName("IX_RoomUser_Composite");
                }
            );
    });

    // Usar convención de nombres en snake_case
    builder.UseSnakeCaseNamingConvention();
}
    }
}
