namespace NoteLiveBackend.Users.Domain.Model.Commands;

public record CreateAlumnoCommand(string Name, string LastName, string Correo,string Password);