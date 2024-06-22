using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoteLiveBackend.IAM.Domain.Model.Aggregates;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Configuration;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }

    public async Task<User?> FindByUsernameAsync(string username)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(user => user.Username.Equals(username));
    }

    public bool ExistsByUsername(string username)
    {
        return Context.Set<User>().Any(user => user.Username.Equals(username));
    }

    public async Task<User?> FindByCorreoAlumnoAsync(string correoAlumno)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(u => u.Correo == correoAlumno && u.CodigoProfesor == null);
    }

    public async Task<User?> FindByCodigoProfesorAsync(long codigoProfesor)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(u => u.CodigoProfesor == codigoProfesor);
    }

    public async Task<IEnumerable<User>> FindByNameAsync(string name)
    {
        return await Context.Set<User>().Where(u => u.Name == name).ToListAsync();
    }


    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await Context.Set<User>().ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid userId)
    {
        return await Context.Set<User>().FindAsync(userId);
    }
    
    public async Task<User?> FindByNameAndCorreoAsync(string name, string correo)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(u => u.Name == name && u.Correo == correo);
    }



}