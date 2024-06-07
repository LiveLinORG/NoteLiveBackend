using NoteLiveBackend.Users.Domain.Model.Commands;

namespace NoteLiveBackend.Users.Domain.Model.Aggregates;

public class Alumno
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public long CodigoAlumno { get; private set; }
    public string Correo { get; private set; }

    protected Alumno()
    {
        this.Name = string.Empty;
        this.CodigoAlumno = long.MinValue; 
        this.Correo = string.Empty;
        
    }

    public Alumno(CreateAlumnoCommand command)
    {
        this.Name = command.Name;
        this.CodigoAlumno = command.CodigoAlumno;
        this.Correo = command.Correo;
    }
}