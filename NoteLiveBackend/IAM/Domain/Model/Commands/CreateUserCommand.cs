namespace NoteLiveBackend.IAM.Domain.Model.Commands;

public record CreateUserCommand(string Name, string LastName, string Correo, string Password, long? CodigoProfesor = null);
