using NoteLiveBackend.Shared.Domain.Repositories;
using NoteLiveBackend.Users.Domain.Model.Aggregates;
using NoteLiveBackend.Users.Domain.Model.Commands;
using NoteLiveBackend.Users.Domain.Repositories;
using NoteLiveBackend.Users.Domain.Services;

namespace NoteLiveBackend.Users.Aplication.Internal.CommandServices;

public class AlumnoCommandService(IAlumnoRepository alumnoRepository,
    IUnitOfWork unitOfWork) : IAlumnoCommandService
{
    public async Task<Alumno> Handle(CreateAlumnoCommand command)
    {
        var alumno = await alumnoRepository.FindByIdAsync(command.id);
        if (alumno != null)
            throw new
                Exception("Alumn with Code Alumn already exists");
        alumno = new Alumno(command);
        await alumnoRepository.AddAsync(alumno);
        await unitOfWork.CompleteAsync();
        return alumno; 
    }
    
    
}