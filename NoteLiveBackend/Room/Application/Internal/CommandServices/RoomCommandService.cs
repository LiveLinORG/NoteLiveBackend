using NoteLiveBackend.IAM.Domain.Model.Aggregates;
using NoteLiveBackend.IAM.Domain.Repositories;
using NoteLiveBackend.Room.Application.Internal.Queryservices;
using NoteLiveBackend.Room.Domain.Model.Commands;
using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Room.Domain.Model.Queries;
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
        var room = await _roomRepository.GetRoomWithUsersAsync(command.RoomId);
        if (room == null)
        {
            // Manejar caso donde la sala no existe
            return null;
        }

        var user = await _userRepository.GetByIdAsync(command.UserId);
        if (user == null)
        {
            // Manejar caso donde el usuario no existe
            return null;
        }

        if (!room.Users.Any(u => u.Id == command.UserId))
        {
            room.Users.Add(user);
            await _roomRepository.SaveAsync();
        }

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
            throw new Exception("Room not found");

        if (room.PDF == null)
        {
            var pdf = new PDF(command.Content);
            await _pdfRepository.AddAsync(pdf);
            room.PdfId = pdf.Id;
            room.PDF = pdf;
        }
        else
        {
            room.PDF.Content = command.Content;
            await _pdfRepository.UpdateAsync(room.PDF);
        }

        await _roomRepository.UpdateAsync(room);
        await unitOfWork.CompleteAsync();
        return true;
    }



}