using NoteLiveBackend.Users.Domain.Model.Aggregates;
using NoteLiveBackend.Users.Interfaces.REST.Resource;

namespace NoteLiveBackend.Users.Interfaces.REST.Transform;

public class ProfesorResourceFromEntityAssembler
{
    public static ProfesorResource toResourceFromEntity(Profesor entity) => new
        ProfesorResource(entity.Id, entity.Name, entity.CodigoProfesor, entity.Correo);

}