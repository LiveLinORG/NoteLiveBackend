namespace NoteLiveBackend.Users.Domain.Model.Commands;

public record CreateProfesorCommand(string Name,long CodigoProfesor,string Correo);