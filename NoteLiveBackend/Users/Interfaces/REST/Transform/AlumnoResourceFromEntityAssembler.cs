using NoteLiveBackend.Users.Domain.Model.Aggregates;
using NoteLiveBackend.Users.Infraestructure.Repositories;
using NoteLiveBackend.Users.Interfaces.REST.Resource;

namespace NoteLiveBackend.Users.Interfaces.REST.Transform;

public static class AlumnoResourceFromEntityAssembler
{
    public static AlumnoResource toResourceFromEntity(Alumno entity) => new
        AlumnoResource(entity.Id, entity.Name, entity.CodigoAlumno, entity.Correo);
}