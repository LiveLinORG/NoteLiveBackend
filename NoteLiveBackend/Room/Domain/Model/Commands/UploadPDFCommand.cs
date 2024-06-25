namespace NoteLiveBackend.Room.Domain.Model.Commands;

public record UploadPDFCommand(Guid RoomId, byte[] Content);
