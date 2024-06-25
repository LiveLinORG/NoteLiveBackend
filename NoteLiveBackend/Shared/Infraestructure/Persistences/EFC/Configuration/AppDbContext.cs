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
    builder.Entity<PDF>().Property(p => p.RoomId).IsRequired();
    builder.Entity<PDF>().HasOne(p => p.Room)
                         .WithOne(r => r.PDF)
                         .HasForeignKey<PDF>(p => p.RoomId)
                         .HasConstraintName("FK_PDF_Room");

    // Configuración de la entidad Chat
    builder.Entity<Chat>().ToTable("Chats");
    builder.Entity<Chat>().HasKey(c => c.Id);
    builder.Entity<Chat>().Property(c => c.Id).IsRequired();
    builder.Entity<Chat>().Property(c => c.RoomId).IsRequired();
    builder.Entity<Chat>().HasOne(c => c.Room)
                          .WithOne(r => r.Chat)
                          .HasForeignKey<Chat>(c => c.RoomId)
                          .HasConstraintName("FK_Chat_Room");

    // Configuración de la entidad Room
    builder.Entity<Room.Domain.Model.Entities.Room>().ToTable("Rooms");
    builder.Entity<Room.Domain.Model.Entities.Room>().HasKey(r => r.Id);
    builder.Entity<Room.Domain.Model.Entities.Room>().Property(r => r.Id).IsRequired();
    builder.Entity<Room.Domain.Model.Entities.Room>().Property(r => r.Name).IsRequired();
    builder.Entity<Room.Domain.Model.Entities.Room>().Property(r => r.ProfessorId).IsRequired();


    // Usar convención de nombres en snake_case
    builder.UseSnakeCaseNamingConvention();
}
    }
}
