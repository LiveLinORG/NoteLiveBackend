using NoteLiveBackend.Users.Domain.Model.Commands;

namespace NoteLiveBackend.Users.Domain.Model.Aggregates;

public class Profesor
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public long CodigoProfesor { get; private set; }
    public string Correo { get; private set; }

    protected Profesor()
    {
        this.Name = string.Empty;
        this.CodigoProfesor = long.MinValue;
        this.Correo = string.Empty;
    }

    public Profesor(CreateProfesorCommand command)
    {
        this.Name = command.Name;
        this.CodigoProfesor = command.CodigoProfesor;
        this.Correo = command.Correo;
    }
}