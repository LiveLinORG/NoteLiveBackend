using NoteLiveBackend.Users.Domain.Model.Commands;
using NoteLiveBackend.Users.Interfaces.REST.Resource;

namespace NoteLiveBackend.Users.Interfaces.REST.Transform;

public static class CreateAlumnoCommandFromResourceAssembler
{
    public static CreateAlumnoCommand ToCommandFromResource(CreateAlumnoResource resource) =>
        new CreateAlumnoCommand(resource.name, resource.lastname, resource.email,resource.password);
}