namespace NoteLiveBackend.Room.Domain.Exceptions;

public class RoomNotFoundException : Exception
{
    public RoomNotFoundException() : base("Room not found") { }
}
