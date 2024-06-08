using NoteLiveBackend.Users.Domain.Model.Commands;

namespace NoteLiveBackend.Users.Domain.Model.Aggregates;

public class Alumno
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string LastName { get; private set; }
    public string Correo { get; private set; }
    public string Password { get; private set; }

    protected Alumno()
    {
        this.Name = string.Empty;
        this.LastName = string.Empty; 
        this.Correo = string.Empty;
        this.Password = string.Empty;
    }

    public Alumno(CreateAlumnoCommand command)
    {
        this.Name = command.Name;
        this.LastName = command.LastName;
        this.Correo = command.Correo;
        this.Password = command.Password;
    }
}