using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Configuration.Extensions;
using NoteLiveBackend.Users.Domain.Model.Aggregates;

namespace NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Configuration
{
    public class AppDbContext : DbContext
    {
        // DbSet para la entidad Room
        public DbSet<Room.Domain.Model.Entities.Room> Rooms { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<PDF> PDFs { get; set; } 
        public DbSet<Chat> Chats { get; set; } 

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
            builder.Entity<Alumno>().ToTable("Alumno");
            builder.Entity<Alumno>().HasKey(a => a.Id);
            builder.Entity<Alumno>().Property(a => a.Id).IsRequired();
            builder.Entity<Alumno>().Property(a => a.Name).IsRequired();
            builder.Entity<Alumno>().Property(a => a.LastName).IsRequired();
            builder.Entity<Alumno>().Property(a => a.Correo).IsRequired();
            builder.Entity<Alumno>().Property(a => a.Password).IsRequired();
            
            // Configuración de la entidad Question
            builder.Entity<Question>().ToTable("Questions");
            builder.Entity<Question>().HasKey(q => q.Id);
            builder.Entity<Question>().Property(q => q.Id).IsRequired();
            builder.Entity<Question>().Property(q => q.Text).IsRequired();
            builder.Entity<Question>().Property(q => q.Likes).IsRequired();
            builder.Entity<Question>().Property(q => q.UserId).IsRequired();
            builder.Entity<Question>().HasOne(q => q.User);
            
            // Configuración de la entidad PDF
            builder.Entity<PDF>().ToTable("PDFs");
            builder.Entity<PDF>().HasKey(p => p.Id);
            builder.Entity<PDF>().Property(p => p.Id).IsRequired();
            builder.Entity<PDF>().Property(p => p.Content).IsRequired();
            builder.Entity<PDF>().Property(p => p.RoomId).IsRequired(); 
            
            // Configuración de la entidad Chat
            builder.Entity<Chat>().ToTable("Chats");
            builder.Entity<Chat>().HasKey(c => c.Id);
            builder.Entity<Chat>().Property(c => c.Id).IsRequired();
            builder.Entity<Chat>().Property(c => c.RoomId).IsRequired();
            
            
            builder.UseSnakeCaseNamingConvention();
        }
    }
}
