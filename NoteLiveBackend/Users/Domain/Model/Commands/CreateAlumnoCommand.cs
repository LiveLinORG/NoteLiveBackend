namespace NoteLiveBackend.Users.Domain.Model.Commands;

public record CreateAlumnoCommand(int id,string Name, string LastName, string Correo, string Password);