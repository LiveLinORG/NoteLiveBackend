using NoteLiveBackend.Users.Domain.Model.Commands;

namespace NoteLiveBackend.Users.Domain.Model.Aggregates;

public class Alumno
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string LastNames { get; private set; }
    public string Correo { get; private set; }
    public string Password { get; private set; }
    protected Alumno()
    {
        this.Name = string.Empty;
        this.LastNames = string.Empty;
        this.Correo= string.Empty;
        this.Password = string.Empty;
        
    }

    public Alumno(CreateAlumnoCommand command)
    {
        
        this.Name = command.Name;
        this.LastNames = command.LastName;
        this.Correo = command.Correo;
        this.Password = command.Password;
    }
}