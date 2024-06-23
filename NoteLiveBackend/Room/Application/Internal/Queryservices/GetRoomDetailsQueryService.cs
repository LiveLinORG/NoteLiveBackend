using NoteLiveBackend.IAM.Domain.Repositories;
using NoteLiveBackend.Room.Application.Internal.CommandServices;
using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Room.Domain.Model.Queries;
using NoteLiveBackend.Room.Interfaces.REST.Transform;

namespace NoteLiveBackend.Room.Application.Internal.Queryservices;

public class GetRoomDetailsQueryService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IChatRepository _chatRepository;
        private readonly IPDFRepository _pdfRepository;
        private readonly IUserRepository _userRepository; 
        public GetRoomDetailsQueryService(
            IRoomRepository roomRepository, 
            IQuestionRepository questionRepository, 
            IChatRepository chatRepository, 
            IPDFRepository pdfRepository, 
            IUserRepository userRepository)
        {
            _roomRepository = roomRepository;
            _questionRepository = questionRepository;
            _chatRepository = chatRepository;
            _pdfRepository = pdfRepository;
            _userRepository = userRepository; // Inyectar el repositorio de usuarios
        }

        public async Task<RoomDetailsDto> HandleAsync(GetRoomDetailsQuery query)
        {
            var room = await _roomRepository.GetById(query.RoomId);
            var questions = (await _questionRepository.GetByRoomId(query.RoomId))
                .Select(async q => new QuestionDto(q.Id, q.Text, q.Likes, await GetUserNameAsync(q.UserId))).ToList();
            var chat = await _chatRepository.GetByRoomId(query.RoomId);
            var chatMessages = (await Task.WhenAll(chat.Messages.Select(async m => 
                new ChatMessageDto(m.Content, await GetUserNameAsync(m.UserId))))).ToList();
            var pdf = await _pdfRepository.GetByRoomId(query.RoomId);

            var pdfDto = new PDFDto(pdf.Id, pdf.Content);

            return new RoomDetailsDto(
                room.Id, 
                room.Name, 
                await GetProfessorNameAsync(room.ProfessorId), 
                (await Task.WhenAll(questions)).ToList(), // Convertir el array en una lista
                chatMessages, 
                pdfDto
            );
        }

        private async Task<string> GetUserNameAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return user?.Name ?? "Unknown"; // Devuelve el nombre del usuario o "Unknown" si el usuario no se encuentra
        }

        private async Task<string> GetProfessorNameAsync(Guid professorId)
        {
            var professor = await _userRepository.GetByIdAsync(professorId);
            return professor?.Name ?? "Unknown"; // Devuelve el nombre del profesor o "Unknown" si el profesor no se encuentra
        }
        
        public async Task<RoomDetailsDto> Handle(GetRoomDetailsQuery query)
        {
            return await HandleAsync(query);
        }
    }