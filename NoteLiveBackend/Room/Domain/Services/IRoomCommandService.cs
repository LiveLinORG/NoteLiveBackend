using NoteLiveBackend.Room.Domain.Model.Commands;
using NoteLiveBackend.Room.Domain.Model.Queries;

namespace NoteLiveBackend.Room.Domain.Services;

public interface IRoomCommandService
{

    Task<Model.Entities.Room?> Handle(CreateRoomCommand command);

    Task<bool> Handle(CheckIfActivatedCommand command);
    Task<bool> Handle(UploadPDFCommand command);

    //falta incorporar
    Task<Model.Entities.Room?> Handle(AddUserToRoomCommand command);
    //falta incorporar
    Task<Model.Entities.Room?> Handle(EndRoomCommand command);


}
