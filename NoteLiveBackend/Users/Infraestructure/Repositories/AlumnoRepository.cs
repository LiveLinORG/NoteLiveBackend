﻿using Microsoft.EntityFrameworkCore;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Configuration;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Repositories;
using NoteLiveBackend.Users.Domain.Model.Aggregates;
using NoteLiveBackend.Users.Domain.Repositories;

namespace NoteLiveBackend.Users.Infraestructure.Repositories;

public class AlumnoRepository : BaseRepository<Alumno>, IAlumnoRepository
{
    public AlumnoRepository(AppDbContext context) : base(context){}

    public async Task<Alumno?> FindByAlumnoCodigoAsync(long codigoAlumno)
    {
        return await Context.Set<Alumno>().FirstOrDefaultAsync(b => b.CodigoAlumno == codigoAlumno);
    }

    public async Task<IEnumerable<Alumno>> FindByNameAlumnoAsync(string name)
    {
        return await Context.Set<Alumno>().Where(a => a.Name == name).ToListAsync();
    }
    
    public async Task<Alumno?> FindByNameAndCodigoAlumnoAsync(string name,long codigoAlumno)
    {
        return await Context.Set<Alumno>()
            .FirstOrDefaultAsync(a => a.Name == name && a.CodigoAlumno == codigoAlumno);
    }
}