namespace NoteLiveBackend.Users.Domain.Model.Commands;

public record CreateAlumnoCommand(string Name, long CodigoAlumno, string Correo);