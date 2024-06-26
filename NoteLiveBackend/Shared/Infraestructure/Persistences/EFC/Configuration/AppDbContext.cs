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
    builder.Entity<User>(user =>
    {
        user.ToTable("Users");
        user.HasKey(u => u.Id);
        user.Property(u => u.Id).IsRequired();
        user.Property(u => u.Username).IsRequired().HasMaxLength(20);
        user.Property(u => u.PasswordHash).IsRequired().HasMaxLength(100);
        user.Property(u => u.Role).IsRequired().HasMaxLength(20);
        user.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
        user.Property(u => u.LastName).IsRequired().HasMaxLength(50);
        user.Property(u => u.Email).IsRequired().HasMaxLength(100);
        user.HasIndex(u => u.Username).IsUnique();

        // Un usuario puede hacer muchas preguntas
        user.HasMany(u => u.Questions)
            .WithOne(q => q.User)
            .HasForeignKey(q => q.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_User_Questions");
    });

    // Configuración de la entidad Question
    builder.Entity<Question>(question =>
    {
        question.ToTable("Questions");
        question.HasKey(q => q.Id);
        question.Property(q => q.Id).IsRequired();
        question.Property(q => q.Text).IsRequired();
        question.Property(q => q.Likes).IsRequired();
        question.Property(q => q.UserId).IsRequired();
        question.Property(q => q.RoomId).IsRequired();

        // Una pregunta no puede tener varios usuarios (ya configurado en User)
        question.HasOne(q => q.User)
            .WithMany(u => u.Questions)
            .HasForeignKey(q => q.UserId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Question_User");

        // Una room puede tener varias preguntas
        question.HasOne(q => q.Room)
            .WithMany(r => r.Questions)
            .HasForeignKey(q => q.RoomId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Question_Room");
    });

    // Configuración de la entidad PDF
    builder.Entity<PDF>(pdf =>
    {
        pdf.ToTable("PDFs");
        pdf.HasKey(p => p.Id);
        pdf.Property(p => p.Id).IsRequired();
        pdf.Property(p => p.Content).IsRequired(false);
    });

    // Configuración de la entidad Chat
    builder.Entity<Chat>(chat =>
    {
        chat.ToTable("Chats");
        chat.HasKey(c => c.Id);
        chat.Property(c => c.Id).IsRequired();
    });

    // Configuración de la entidad Room
    builder.Entity<Room.Domain.Model.Entities.Room>(room =>
    {
        room.ToTable("Rooms");
        room.HasKey(r => r.Id);
        room.Property(r => r.Id).IsRequired();
        room.Property(r => r.Name).IsRequired();
        room.Property(r => r.CreadorId).IsRequired();

        // Relación con PDF
        room.Property(r => r.PdfId).IsRequired();
        room.HasOne(r => r.PDF)
            .WithOne()
            .HasForeignKey<Room.Domain.Model.Entities.Room>(r => r.PdfId)
            .HasConstraintName("FK_Room_PDF");

       // Relación con Creador (User)
        room.HasOne(r => r.Creador)
            .WithOne()  
            .HasForeignKey<Room.Domain.Model.Entities.Room>(r => r.CreadorId)  
            .OnDelete(DeleteBehavior.Restrict)      
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
    //builder.UseSnakeCaseNamingConvention();
}
    }
}
