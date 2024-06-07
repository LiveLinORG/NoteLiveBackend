using Microsoft.EntityFrameworkCore;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Configuration;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Repositories;
using NoteLiveBackend.Users.Domain.Model.Aggregates;
using NoteLiveBackend.Users.Domain.Repositories;

namespace NoteLiveBackend.Users.Infraestructure.Repositories;

public class ProfesorRepository : BaseRepository<Profesor>, IProfesorRepository
{
    public ProfesorRepository(AppDbContext context) : base(context){}

    public async Task<Profesor> FindByCodigoProfesorAsync(long codigoProfesor)
    {
        return await Context.Set<Profesor>().FirstOrDefaultAsync(b => b.CodigoProfesor == codigoProfesor);
    }

    public async Task<IEnumerable<Profesor>> FindByNameProfesorAsync(string name)
    {
        return await Context.Set<Profesor>().Where(a => a.Name == name).ToListAsync();
    }
    
    public async Task<Profesor?> FindByNameAndProfesorCodigoAsync(string name,long codigoProfesor)
    {
        return await Context.Set<Profesor>()
            .FirstOrDefaultAsync(a => a.Name == name && a.CodigoProfesor == codigoProfesor);
    }
}