using NoteLiveBackend.Users.Domain.Model.Commands;
using NoteLiveBackend.Users.Interfaces.REST.Resource;

namespace NoteLiveBackend.Users.Interfaces.REST.Transform;

public class CreateProfesorCommandFromResourceAssembler
{
    public static CreateProfesorCommand ToCommandFromResource(CreateProfesorResource resource) =>
        new CreateProfesorCommand(resource.name, resource.codigoProfesor, resource.email);

}