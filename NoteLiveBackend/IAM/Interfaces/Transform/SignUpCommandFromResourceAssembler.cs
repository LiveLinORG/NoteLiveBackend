using NoteLiveBackend.IAM.Domain.Model.Commands;

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