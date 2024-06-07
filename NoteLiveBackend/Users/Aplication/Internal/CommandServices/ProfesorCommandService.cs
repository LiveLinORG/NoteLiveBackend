using NoteLiveBackend.Shared.Domain.Repositories;
using NoteLiveBackend.Users.Domain.Model.Aggregates;
using NoteLiveBackend.Users.Domain.Model.Commands;
using NoteLiveBackend.Users.Domain.Repositories;
using NoteLiveBackend.Users.Domain.Services;

namespace NoteLiveBackend.Users.Aplication.Internal.CommandServices;

public class ProfesorCommandService(IProfesorRepository profesorRepository,
    IUnitOfWork unitOfWork) : IProfesorCommandService
{
    public async Task<Profesor> Handle(CreateProfesorCommand command)
    {
        var profesor = await profesorRepository.FindByCodigoProfesorAsync(command.CodigoProfesor);
        if (profesor != null)
            throw new
                Exception("Professor with Code Professor already exists");
        profesor = new Profesor(command);
        await profesorRepository.AddAsync(profesor);
        await unitOfWork.CompleteAsync();
        return profesor; 
    }
    
    
}