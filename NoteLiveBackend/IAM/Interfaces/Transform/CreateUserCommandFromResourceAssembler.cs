using NoteLiveBackend.IAM.Domain.Model.Commands;
using NoteLiveBackend.IAM.Interfaces.Resources;

namespace NoteLiveBackend.IAM.Interfaces.Transform;

public static class CreateUserCommandFromResourceAssembler
{
    public static CreateUserCommand ToCommandFromResource(CreateUserResource resource)
    {
        if (resource.CodigoProfesor.HasValue)
        {
            return new CreateUserCommand(resource.Name, resource.LastName, resource.Correo, resource.Password, resource.CodigoProfesor.Value);
        }
        else
        {
            return new CreateUserCommand(resource.Name, resource.LastName, resource.Correo, resource.Password);
        }
    }
}
