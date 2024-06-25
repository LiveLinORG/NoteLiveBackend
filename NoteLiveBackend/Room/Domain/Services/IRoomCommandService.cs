﻿using NoteLiveBackend.Room.Domain.Model.Commands;
namespace NoteLiveBackend.Room.Domain.Services;

public interface IRoomCommandService
{

    Task<Model.Entities.Room?> Handle(CreateRoomCommand command);
    
    Task<bool> Handle(CheckIfActivatedCommand command);

    //falta incorporar
    Task<Model.Entities.Room?> Handle(AddUserToRoomCommand command);
    //falta incorporar
    Task<Model.Entities.Room?> Handle(EndRoomCommand command);


}
