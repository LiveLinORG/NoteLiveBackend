using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Users.Domain.Repositories;

namespace NoteLiveBackend.Users.Infraestructure.Repositories;



using User = NoteLiveBackend.Room.Domain.Model.Entities.User;

public class UserRepository : IUserRepository
{
    private readonly IAlumnoRepository _alumnoRepository;
    private readonly IProfesorRepository _profesorRepository;

    public UserRepository(IAlumnoRepository alumnoRepository, IProfesorRepository profesorRepository)
    {
        _alumnoRepository = alumnoRepository;
        _profesorRepository = profesorRepository;
    }

    public async Task<User?> GetByIdAsync(Guid userId)
    {
        var alumno = await _alumnoRepository.FindByIdAsync(userId);
        if (alumno != null)
        {
            return new User(alumno.Id, alumno.Name);
        }

        var profesor = await _profesorRepository.FindByIdAsync(userId);
        if (profesor != null)
        {
            return new User(profesor.Id, profesor.Name);
        }

        return null;
    }

    public async Task<string?> GetUserNameAsync(Guid userId)
    {
        var user = await GetByIdAsync(userId);
        return user?.Name;
    }
}