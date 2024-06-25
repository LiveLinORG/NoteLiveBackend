using NoteLiveBackend.Room.Interfaces.REST.Resources;

namespace NoteLiveBackend.Room.Interfaces.REST.Transform;

public static class RoomResourceFromEntityAssembler
{
    public static RoomResource ToResourceFromEntity(Domain.Model.Entities.Room room)
    {
        return new RoomResource(
            room.Id,
            room.Name,
            room.ProfessorId,
            room.Questions,
            room.Creador,
            room.UserIds,
            room.PDF,
            room.Chat);
    }
}