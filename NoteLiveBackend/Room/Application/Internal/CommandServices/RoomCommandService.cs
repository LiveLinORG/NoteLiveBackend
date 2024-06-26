using NoteLiveBackend.IAM.Domain.Model.Aggregates;
using NoteLiveBackend.IAM.Domain.Repositories;
using NoteLiveBackend.Room.Application.Internal.Queryservices;
using NoteLiveBackend.Room.Domain.Model.Commands;
using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Room.Domain.Repositories;
using NoteLiveBackend.Room.Domain.Services;
using NoteLiveBackend.Shared.Domain.Repositories;

namespace NoteLiveBackend.Room.Application.Internal.CommandServices;

public class RoomCommandService(
    IRoomRepository _roomRepository,
    IUserRepository _userRepository,
    IPDFRepository _pdfRepository,
    IChatRepository _chatRepository,
    IQuestionRepository _questionRepository,
    IUnitOfWork unitOfWork
    ) : IRoomCommandService
{
    
    
    //FALTA IMPLEMENTAR---IPDFRepository _pdfRepository
    //IChatRepository _chatRepository;
    //IQuestionRepository _questionRepository;

    public async Task<Domain.Model.Entities.Room?> Handle(CreateRoomCommand command)
    {
        var creador = await _userRepository.FindByIdAsync(command.ProfessorId);
        var room = new Domain.Model.Entities.Room(command.Name, creador);
        await _roomRepository.AddSync(room);
        await unitOfWork.CompleteAsync();
        return room;
    }

    public async Task<Domain.Model.Entities.Room?> Handle(AddUserToRoomCommand command)
    {
        var room = await _roomRepository.FindByIdAsync(command.RoomId);
        if (room == null)
            throw new Exception("Room not found");
        var user = await _userRepository.FindByIdAsync(command.UserId);
        if (user != null) room.AddUser(user); //tal vez de error
        await _roomRepository.UpdateAsync(room);
        return room;
    }

    public async Task<Domain.Model.Entities.Room?> Handle(EndRoomCommand command)
    {
        var room = await _roomRepository.FindByIdAsync(command.RoomId);
        if (room == null)
            throw new Exception("Room not found");

        room.EndRoom();
        await _roomRepository.UpdateAsync(room);
        await unitOfWork.CompleteAsync();
        return room;
    }
    public async Task<bool> Handle(CheckIfActivatedCommand command)
    {
        var room = await _roomRepository.FindByIdAsync(command.RoomId);
        if (room == null || !room.Roomstarted)
            return false;

        var chat = await _chatRepository.GetByRoomId(command.RoomId);
        if (chat == null || !chat.isActivated)
            return false;

        return true;
    }
    
    public async Task<bool> Handle(UploadPDFCommand command)
    {
        var room = await _roomRepository.FindByIdAsync(command.RoomId);
        if (room == null)
            return false;

        byte[] pdf = command.Content;
        room.UploadPDF(pdf);
        await _roomRepository.UpdateAsync(room);
        await unitOfWork.CompleteAsync();

        return true;
    }
}