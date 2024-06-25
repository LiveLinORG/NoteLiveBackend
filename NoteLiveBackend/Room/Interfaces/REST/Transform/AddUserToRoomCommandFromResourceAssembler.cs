using NoteLiveBackend.Room.Domain.Model.Commands;
using NoteLiveBackend.Room.Interfaces.REST.Resources;

namespace NoteLiveBackend.Room.Interfaces.REST.Transform;

public static class AddUserToRoomCommandFromResourceAssembler
{
    public static AddUserToRoomCommand ToCommandFromResource(AddUserToRoomResource addUserToRoomResource, Guid userId)
    {
        return new AddUserToRoomCommand(addUserToRoomResource.RoomId, userId);
    }
}