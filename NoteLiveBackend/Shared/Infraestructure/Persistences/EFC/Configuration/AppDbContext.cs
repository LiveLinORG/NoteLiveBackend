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
            builder.Entity<Alumno>().Property(a => a.CodigoAlumno).IsRequired();
            builder.Entity<Alumno>().Property(a => a.Correo).IsRequired();
            builder.UseSnakeCaseNamingConvention();
        }
    }
}