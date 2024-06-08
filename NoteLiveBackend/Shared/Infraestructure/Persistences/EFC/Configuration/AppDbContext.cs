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

        public DbSet<User> Users { get; set; }
        public DbSet<RoomUser> RoomUsers { get; set; }

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
            
            builder.Entity<Profesor>().ToTable("Profesor");
            builder.Entity<Profesor>().HasKey(a => a.Id);
            builder.Entity<Profesor>().Property(a => a.Id).IsRequired();
            builder.Entity<Profesor>().Property(a => a.Name).IsRequired();
            builder.Entity<Profesor>().Property(a => a.CodigoProfesor).IsRequired();
            builder.Entity<Profesor>().Property(a => a.Correo).IsRequired();

            
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
            
            // Configuración de la entidad User
            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(u => u.Id);
            builder.Entity<User>().Property(u => u.Id).IsRequired();
            builder.Entity<User>().Property(u => u.Name).IsRequired();
            
            // Configuración de la entidad RoomUser
            builder.Entity<RoomUser>().ToTable("RoomUsers");
            builder.Entity<RoomUser>().HasKey(ru => new { ru.RoomId, ru.UserId });
            builder.Entity<RoomUser>().HasOne(ru => ru.Room)
                .WithMany(r => r.RoomUsers)
                .HasForeignKey(ru => ru.RoomId);
            builder.Entity<RoomUser>().HasOne(ru => ru.User)
                .WithMany()
                .HasForeignKey(ru => ru.UserId);
            
            builder.UseSnakeCaseNamingConvention();
        }
    }
}
