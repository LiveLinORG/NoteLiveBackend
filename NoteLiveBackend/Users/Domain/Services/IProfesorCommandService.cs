using NoteLiveBackend.Users.Domain.Model.Aggregates;
using NoteLiveBackend.Users.Domain.Model.Commands;

namespace NoteLiveBackend.Users.Domain.Services;

public interface IProfesorCommandService
{
    Task<Profesor> Handle(CreateProfesorCommand command);

}