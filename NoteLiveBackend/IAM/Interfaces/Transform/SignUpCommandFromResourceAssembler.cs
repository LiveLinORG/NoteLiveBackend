using NoteLiveBackend.IAM.Domain.Model.Commands;
using NoteLiveBackend.IAM.Interfaces.Resources;

namespace NoteLiveBackend.IAM.Interfaces.Transform;

public class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
  
        return new SignUpCommand(
            resource.Username,
            resource.Password,
            resource.Role,
            resource.Name,
            resource.LastName,
            resource.Correo,
            resource.CodigoProfesor
        );
    }
}